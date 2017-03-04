function OrderViewModel() {
    var self = this;
    self.orders = ko.observableArray();
    self.id = ko.observable();
    self.customerid = 1;
    self.paymentdone = ko.observableArray();
    self.deliverytime = ko.observable();
    self.ordertime = ko.observableArray();
    self.paymentmethod = ko.observable();
    self.orderstatus = ko.observable();
    self.deliveryplace = ko.observable();
    self.orderitems = ko.observableArray(new OrderItemViewModel); // observable array of OrderItemViewModel

    self.showError = function (jqXHR) {

        self.result(jqXHR.status + ': ' + jqXHR.statusText);

        var response = jqXHR.responseJSON;
        if (response) {
            if (response.Message) self.errors.push(response.Message);
            if (response.ModelState) {
                var modelState = response.ModelState;
                for (var prop in modelState) {
                    if (modelState.hasOwnProperty(prop)) {
                        var msgArr = modelState[prop]; // expect array here
                        if (msgArr.length) {
                            for (var i = 0; i < msgArr.length; ++i) self.errors.push(msgArr[i]);
                        }
                    }
                }
            }
            if (response.error) self.errors.push(response.error);
            if (response.error_description) self.errors.push(response.error_description);
        }
    }

    self.getorders = function () {
        $.ajax({
            type: 'Get',
            url: '/api/Order',
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            console.log(data)
            self.orders(data.orders)
        }).fail(self.showError);
    };

    self.getorders();


    //    $('#myModal').on('show.bs.modal', function (event) {
    //        var button = $(event.relatedTarget)[0];
    //        self.cafeteriaId(button.attributes["cafeteriaid"].value)
    //        self.name(button.attributes["name"].value)
    //        console.log(self.name());
    //        self.getCategoryByCafeteriaId();
    //    });

    //    self.getCategoryByCafeteriaId = function () {
    //        console.log(self.cafeteriaId())
    //        $.ajax({
    //            type: 'Get',
    //            url: '/api/Category/GetByCafetria/' + self.cafeteriaId(),
    //            contentType: 'application/json; charset=utf-8',
    //        }).done(function (data) {
    //            self.categories(data.categories);
    //        }).fail(self.showError);
    //    };



    //    self.deleteCafeteria = function () {

    //        if (self.categories().length == 0) {
    //            console.log(self.categories().length)
    //            console.log("id=" + self.cafeteriaId());
    //            $.ajax({
    //                type: 'Delete',
    //                url: '/api/Cafeteria/' + self.cafeteriaId(),
    //                contentType: 'application/json; charset=utf-8',
    //                data: { id: self.cafeteriaId() }
    //            }).done(function (data) {
    //                console.log(data)
    //                $('#myModal').modal('hide')
    //                alertify.success(self.name() + " cafeteria is deleted ");
    //                self.getAllCafeterias();
    //            }).fail(self.showError);
    //        } else {
    //            alertify.error("Error, You Must delete categories of " + self.name() + " cafeteria first!");

    //        }

    //    }
    //}


    //function CafeteriaEditViewModel(id) {

    //    var self = this;
    //    self.cafeteriaId = ko.observable(id);
    //    self.categoryId = ko.observable();
    //    self.categories = ko.observableArray();
    //    self.menuItems = ko.observableArray();
    //    self.name = ko.observable();

    //    self.model = ko.validatedObservable({
    //        name: ko.observable().extend({ required: true, maxLength: 100 })
    //    });


    //    self.showError = function (jqXHR) {

    //        self.result(jqXHR.status + ': ' + jqXHR.statusText);

    //        var response = jqXHR.responseJSON;
    //        if (response) {
    //            if (response.Message) self.errors.push(response.Message);
    //            if (response.ModelState) {
    //                var modelState = response.ModelState;
    //                for (var prop in modelState) {
    //                    if (modelState.hasOwnProperty(prop)) {
    //                        var msgArr = modelState[prop]; // expect array here
    //                        if (msgArr.length) {
    //                            for (var i = 0; i < msgArr.length; ++i) self.errors.push(msgArr[i]);
    //                        }
    //                    }
    //                }
    //            }
    //            if (response.error) self.errors.push(response.error);
    //            if (response.error_description) self.errors.push(response.error_description);
    //        }
    //    }


    self.getorder = function () {
        ////self.orderitems().getorderitems();
        ////for(i=0;i<self.orderitems().)
        //self.orderitems().getorderitems();
        ////$.ajax({
        ////    type: 'Get',
        ////    url: '/api/OrderItems/',
        ////    contentType: 'application/json; charset=utf-8',
        ////}).done(function (data) {
        ////    self.model().orderitems(data.order.OrderItems);
        ////}).fail(self.showError);
    };

    self.getorder();


    //    self.save = function () {

    //        if (self.model.isValid()) {
    //            var data = {
    //                name: self.model().name(),
    //                id: self.cafeteriaId()
    //            }

    //            $.ajax({
    //                type: 'PUT',
    //                url: '/api/Cafeteria/' + self.cafeteriaId(),
    //                contentType: 'application/json; charset=utf-8',
    //                data: JSON.stringify(data)
    //            }).done(function (result) {
    //                document.location = '/admin/cafeteria/index';
    //            }).fail(self.showError);
    //        } else {
    //            alertify.error("Error,Some fileds are invalid !");
    //        }

    //    }

    //    self.cancel = function () {
    //        document.location = '/Admin/Cafeteria/Index';
    //    }


    //    self.getCategoryByCafeteriaId = function () {
    //        console.log(self.cafeteriaId())
    //        $.ajax({
    //            type: 'Get',
    //            url: '/api/Category/GetByCafetria/' + self.cafeteriaId(),
    //            contentType: 'application/json; charset=utf-8'
    //        }).done(function (data) {
    //            self.categories(data.categories);
    //        }).fail(self.showError);
    //    };


    //    self.getCategoryByCafeteriaId();


    //    $('#myModal').on('show.bs.modal', function (event) {
    //        var button = $(event.relatedTarget)[0];
    //        self.categoryId(button.attributes["categoryid"].value);
    //        self.name(button.attributes["name"].value);
    //        self.getMenuItemByCategoryId();
    //    });

    //    self.getMenuItemByCategoryId = function () {
    //        console.log(self.categoryId())
    //        $.ajax({
    //            type: 'Get',
    //            url: '/api/MenuItem/GetByCategory/' + self.categoryId(),
    //            contentType: 'application/json; charset=utf-8'
    //        }).done(function (data) {
    //            console.log(data);
    //            self.menuItems(data.menuItems);
    //        }).fail(self.showError);
    //    };

    //    self.deleteCategory = function () {


    //        if (self.menuItems().length == 0) {
    //            console.log("id=" + self.categoryId());
    //            $.ajax({
    //                type: 'Delete',
    //                url: '/api/Category/' + self.categoryId(),
    //                contentType: 'application/json; charset=utf-8',
    //                data: { id: self.categoryId() }
    //            }).done(function (data) {
    //                console.log(data);
    //                $('#myModal').modal('hide');
    //                alertify.success(self.name() + " category is deleted ");
    //                self.getCategoryByCafeteriaId();
    //            }).fail(self.showError);
    //        } else {
    //            alertify.error("Error, You Must delete menuitems of " + self.name() + " category first!");
    //        }
    //    }



    //}
}
    function OrderNewViewModel() {
        var self = this;


        //self.model = ko.validatedObservable({
        //    name: ko.observable().extend({ required: true, maxLength: 100 })
        //});
        self.showError = function (jqXHR) {

            self.result(jqXHR.status + ': ' + jqXHR.statusText);

            var response = jqXHR.responseJSON;
            if (response) {
                if (response.Message) self.errors.push(response.Message);
                if (response.ModelState) {
                    var modelState = response.ModelState;
                    for (var prop in modelState) {
                        if (modelState.hasOwnProperty(prop)) {
                            var msgArr = modelState[prop]; // expect array here
                            if (msgArr.length) {
                                for (var i = 0; i < msgArr.length; ++i) self.errors.push(msgArr[i]);
                            }
                        }
                    }
                }
                if (response.error) self.errors.push(response.error);
                if (response.error_description) self.errors.push(response.error_description);
            }
        }

        self.add = function () {

            if (self.model.isValid()) {
                var data = {
                    //Id: self.model().id(),
                    OrderTime: self.model().ordertime(),
                    OrderStatus: self.model().orderstatus(),
                    DeliveryTime: self.model().deliverytime(),
                    DeliveryPlace: self.model().deliveryplace(),
                    PaymentMethod: self.model().paymentmethod(),
                    PaymentDone: self.model().paymentdone(),
                    CustomerId: self.model().customerid()
                }
                console.log(data);
                $.ajax({
                    type: 'Post',
                    url: '/api/order',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(data)
                }).done(function (result) {
                    console.log(result);
                    document.location = '/Customer/cafeteria/Index';
                }).fail(self.showError);
            } else {

                alertify.error("Error,Some fields are invalid !");

            }

        }

        self.cancel = function () {
            document.location = '/Customer/Cafeteria/Index';
        }
}