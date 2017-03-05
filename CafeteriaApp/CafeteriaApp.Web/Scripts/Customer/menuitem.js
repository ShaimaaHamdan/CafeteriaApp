function CustomerMenuItemViewModel(id) {
    var self = this;
    self.categoryId = ko.observable(id);
    self.menuItemId = ko.observable();
    self.orderId = ko.observable();
    self.menuItems = ko.observableArray();
    self.cafeteriaId = ko.observable();
    self.name = ko.observable();

    //TODO need to get the id from the logged in user
    self.customerId = ko.observable(1);

    self.quantity = ko.observable(2);

    self.orderItems = ko.observableArray();

    (self.init = function () {

        //Get orderitems for current users for last order not checked out.

        $.ajax({
            type: 'Get',
            url: '/api/order/GetbyCustomerId/' + self.customerId(),
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            console.log(data)
            if (data.order != undefined)
            {
                self.orderItems(data.order.OrderItems);
                self.orderId(data.order.Id)
            }
        }).fail(self.showError);

    })();

    self.total = ko.computed(function () {
        var total = 0;
        for (var p = 0; p < self.orderItems().length; ++p) {
            total += self.orderItems()[p].MenuItem.Price;
        }
        return total;
    });

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

    self.getCategory = function () {
        $.ajax({
            type: 'Get',
            url: '/api/Category/' + self.categoryId(),
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            self.cafeteriaId(data.category.CafeteriaId);
            self.name(data.category.Name);
        }).fail(self.showError);
    };

    self.getCategory();

    self.getMenuItemByCategoryId = function () {
        console.log(self.categoryId())
        $.ajax({
            type: 'Get',
            url: '/api/MenuItem/GetByCategory/' + self.categoryId(),
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            console.log(data)
            self.menuItems(data.menuItems)
        }).fail(self.showError);
    };


    self.getMenuItemByCategoryId();


    self.addToCart = function (menuItem) {


        var data = {
            quantity: self.quantity(),
            menuItemid: menuItem.Id,
            orderid: self.orderId(),
            customerid: self.customerId()
        }
        $.ajax({
            type: 'Post',
            url: '/api/OrderItem',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (result) {
            console.log(result)
            self.init();
        }).fail(self.showError);

    }

}
