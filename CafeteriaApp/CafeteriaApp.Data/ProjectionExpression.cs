﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CafeteriaApp.Data
{
    public class ProjectionExpression<TSource>
    {
        private static readonly Dictionary<string, Expression> ExpressionCache = new Dictionary<string, Expression>();

        private readonly IQueryable<TSource> _source;

        public ProjectionExpression(IQueryable<TSource> source)
        {
            _source = source;
        }

        /// <summary>
        /// Specifies the target type
        /// </summary>
        /// <typeparam name="TDest"></typeparam>
        /// <returns></returns>
        public IQueryable<TDest> To<TDest>()
        {
            var queryExpression = GetCachedExpression<TDest>() ?? BuildExpression<TDest>();

            return _source.Select(queryExpression);
        }

        private static Expression<Func<TSource, TDest>> GetCachedExpression<TDest>()
        {
            var key = GetCacheKey<TDest>();

            return ExpressionCache.ContainsKey(key) ? ExpressionCache[key] as Expression<Func<TSource, TDest>> : null;
        }

        private static Expression<Func<TSource, TDest>> BuildExpression<TDest>()
        {
            var sourceProperties = typeof(TSource).GetProperties();
            var destinationProperties = typeof(TDest).GetProperties().Where(dest => dest.CanWrite);
            var parameterExpression = Expression.Parameter(typeof(TSource), "src");

            var bindings = destinationProperties
                                .Select(destinationProperty => BuildBinding(parameterExpression, destinationProperty, sourceProperties))
                                .Where(binding => binding != null);

            var expression = Expression.Lambda<Func<TSource, TDest>>(Expression.MemberInit(Expression.New(typeof(TDest)), bindings), parameterExpression);

            var key = GetCacheKey<TDest>();

            ExpressionCache.Add(key, expression);

            return expression;
        }

        private static MemberAssignment BuildBinding(Expression parameterExpression, MemberInfo destinationProperty, IEnumerable<PropertyInfo> sourceProperties)
        {
            var sections = SplitCamelCase(destinationProperty.Name);

            return ResolveProperty(
                parameterExpression,
                destinationProperty,
                sections[0],
                1,
                sourceProperties,
                sections);
        }

        private static MemberAssignment ResolveProperty(
            Expression parameterExpression,
            MemberInfo destinationProperty,
            string currentName,
            int currentIndex,
            IEnumerable<PropertyInfo> properties,
            string[] sections)
        {
            // Check if any of the current properties match in name
            var property = properties.FirstOrDefault(src => src.Name == currentName);

            // If we're at the end of the sections, attempt to bind, or nothing found
            if (currentIndex == sections.Length)
            {
                return (property != null) ?
                    Expression.Bind(destinationProperty, Expression.Property(parameterExpression, property)) :
                    null;
            }

            // The property exists and there are still sections left - look into the child
            if (property != null)
            {
                // We found a property with currentName, so move to the next section
                // Examine the remaining sections on child properties
                var result = ResolveProperty(
                    Expression.Property(parameterExpression, property),
                    destinationProperty,
                    sections[currentIndex],
                    currentIndex + 1,             // proceed to the next section index
                    property.PropertyType.GetProperties(),
                    sections);

                // If we found a property on the child, return it.  Otherwise continue searching
                if (result != null)
                    return result;
            }

            // currentName doesn't exist, so add the next section and keep searching
            return ResolveProperty(
                    parameterExpression,
                    destinationProperty,
                    currentName + sections[currentIndex],
                    currentIndex + 1,             // proceed to the next section index
                    properties,
                    sections);
        }


        private static string GetCacheKey<TDest>()
        {
            return string.Concat(typeof(TSource).FullName, typeof(TDest).FullName);
        }

        private static string[] SplitCamelCase(string input)
        {
            return Regex.Replace(input, "([A-Z])", " $1", RegexOptions.Compiled).Trim().Split(' ');
        }
    }
}
