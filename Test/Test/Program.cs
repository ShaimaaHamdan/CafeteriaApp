using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {


        }

        public int solution(int[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            int matchedIndex = -1;

            long smallIndex = 0;
            long largeIndex = 0;
            foreach (int i in A)
                largeIndex += i;

            for (int i = 0; i < A.Length; i++)
            {
                largeIndex -= A[i];
                if (largeIndex == smallIndex)
                {
                    matchedIndex = i;
                    break;
                }
                else
                    smallIndex += A[i];
            }

            return matchedIndex;

        }
    }
}
