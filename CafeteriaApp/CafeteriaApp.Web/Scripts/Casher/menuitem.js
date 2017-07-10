function CasherMenuItemViewModel(categoryId,userId) {
    var self = this;
    self.categoryId = ko.observable(categoryId);
    self.cafeteriaId = ko.observable();
    self.name = ko.observable();
    self.userId = ko.observable(userId);
    self.customerId = ko.observable();
    self.menuItemId = ko.observable();
    self.menuItem = ko.observable();
    self.orderId = ko.observable();
    self.currentorder = ko.observable();
    self.currentorderstatus = ko.observable();
    self.makeorderclicked = ko.observable();
    self.editorderclicked = ko.observable();
    self.deleteallclicked = ko.observable(0);
    self.paymentmethod = ko.observableArray(["credit", "cash"]);
    self.chosenpaymentmethod = ko.observable();
    self.deliveryMethod = ko.observableArray(["Delivery", "Take Away"]);
    self.chosenDeliveryMethod = ko.observable();
    self.deliveryFee = ko.observable(10);
    self.deliveryplace = ko.observable();
    self.menuItems = ko.observableArray();
    self.menuItem = ko.observable();
    self.currentmenuitemId = ko.observable();
    self.viewdetailsclicked = ko.observable();
    self.orderItems = ko.observableArray();
    self.casherorders = ko.observableArray();








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
    self.getCasherOrders = function () {
        $.ajax({
            type: 'Get',
            url: '/api/order/GetAllbyCustomerId/' + self.customerId(),
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            self.casherorders(data.orders);
        }).fail(self.showError);
    };

    self.getCustomerById = function () {
        if (self.userId() != undefined && self.userId() != '') {
            //console.log(self.userId());
            $.ajax({
                type: 'Get',
                url: '/api/Customer/' + self.userId(),
                contentType: 'application/json; charset=utf-8'
            }).done(function (result) {
                var data = result.customer;
                self.customerId(data.Id);
                self.getCasherOrders();
                console.log(self.customerId());
                //self.init();
            }).fail(self.showError);
        }
    };

    self.getCustomerById();

    self.getCategory = function () {
        if (self.categoryId() != undefined && self.categoryId() != '') {
            $.ajax({
                type: 'Get',
                url: '/api/Category/' + self.categoryId(),
                contentType: 'application/json; charset=utf-8',
            }).done(function (data) {
                self.cafeteriaId(data.category.CafeteriaId);
                self.name(data.category.Name);
            }).fail(self.showError);
        }
    };

    self.getCategory();

    (self.getMenuItemByCategoryId = function () {
        if (self.categoryId() != undefined && self.categoryId() != '') {
            $.ajax({
                type: 'Get',
                url: '/api/MenuItem/GetByCategory/' + self.categoryId(),
                contentType: 'application/json; charset=utf-8',
            }).done(function (data) {
                //console.log(data)
                self.menuItems(data.menuItems)
            }).fail(self.showError);
        }
    })();


    //self.init = function () {
    //    //Get orderitems for current users for last order not checked out.
    //    /* if (self.customerId()!=7) {*/ // if it's logged in user not from outside
    //    console.log(self.customerId());
    //    $.ajax({
    //        type: 'Get',
    //        url: '/api/order/GetbyCustomerId/' + self.customerId(),
    //        contentType: 'application/json; charset=utf-8'
    //    }).done(function (data) {
    //        if (data.order != undefined) {
    //            self.orderItems(data.order.OrderItems);
    //            self.orderId(data.order.Id);
    //            self.currentorder(data.order);
    //            self.currentorderstatus(data.order.OrderStatus);
    //            //self.comments()
    //            self.deliveryplace(data.order.DeliveryPlace);
    //            console.log(self.orderItems());
    //        }
    //    }).fail(self.showError);
    //}

    //self.init();

    self.viewdetails = function (menuitem) {
        self.viewdetailsclicked(1);
        if (self.currentmenuitemId() != menuitem.Id) {
            self.currentmenuitemId(menuitem.Id);
            self.menuItem(menuitem);
        }
    };
    self.hidedetails = function (menuitem) {
        self.viewdetailsclicked(0);
        self.currentmenuitemId(-1);
    };

    self.initialorder = function () {
        if (self.makeorderclicked() == 1) {
            $.ajax({
                type: 'Get',
                url: '/api/order/Getlastorder/' + self.customerId(),
                contentType: 'application/json; charset=utf-8'
            }).done(function (data) {
                self.editorder(data.order);
                self.editorderclicked(1);
            }).fail(self.showError);
        } else if (self.editorderclicked() == 1) {
            $.ajax({
                type: 'Get',
                url: '/api/order/' + self.orderId(),
                contentType: 'application/json; charset=utf-8'
            }).done(function (data) {
                self.orderId(data.order.Id);
                self.orderItems(data.order.OrderItems);
                self.currentorder(data.order);
                self.deliveryplace(data.order.DeliveryPlace);
                self.chosenpaymentmethod(data.order.PaymentMethod);
                console.log(self.orderItems);
            }).fail(self.showError);
        }
    };

    self.MakeOrder = function () {
        self.makeorderclicked(1);
        var data = {
            OrderTime: new Date(),
            OrderStatus: "waiting",
            DeliveryTime: new Date(),
            DeliveryPlace: "No",
            PaymentMethod: "cash",
            PaymentDone: false,
            CustomerId: self.customerId()
        }
        $.ajax({
            type: 'Post',
            url: '/api/Order',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (result) {
            self.initialorder();
            self.init();
        }).fail(self.showError);
    }

    self.editorder = function (order) {
        self.makeorderclicked(0);
        self.editorderclicked(1);
        self.orderId(order.Id);
        self.orderItems(order.OrderItems);
        self.currentorder(order);
        self.deliveryplace(order.DeliveryPlace);
        self.chosenpaymentmethod(order.PaymentMethod);
    }

    self.deleteorder = function (order) {
        $.ajax({
            type: 'Delete',
            url: '/api/OrderItem/DeleteAll/' + order.Id,
            contentType: 'application/json; charset=utf-8',
            data: { id: order.Id }
        }).done(function (data) {
            self.init();
        }).fail(self.showError)
        $.ajax({
            type: 'Delete',
            url: '/api/Order/' + order.Id,
            contentType: 'application/json; charset=utf-8',
            data: { id: order.Id }
        }).done(function (data) {
            self.init();
        }).fail(self.showError)
    }


    self.hide = function (order) {
        if (self.orderId() == order.Id) {
            self.makeorderclicked(0);
            self.editorderclicked(0);
        }
    }

    // order items

    self.addToCart = function (menuItem) {
        if (self.userId() != undefined && self.userId() != '') {
            var x = self.orderItems().filter(e => e.MenuItemId == menuItem.Id)[0];
            if (x != null) {
                self.addanother(x);
            } else {
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
                    self.init();
                    self.initialorder();
                }).fail(self.showError);
            }
        } else {
            document.location = '/login/login';
        }
    }

    self.addanother = function (orderitem) {
        var data = {
            id: orderitem.Id, // id of orderitem to be passed in the put request
            quantity: orderitem.Quantity + 1, // increasing the quantity
            menuItemid: self.menuItemId,
            orderid: self.orderId,
            customerid: self.customerId
        }
        $.ajax({
            type: 'Put',
            url: '/api/OrderItem/' + orderitem.Id,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (result) {
            self.init();
            self.initialorder();
        }).fail(self.showError);
    };

    self.deleteitem = function (orderitem) {
        if ((self.currentorder().OrderStatus != 'inprogress') && (self.currentorder().OrderStatus != 'completed')) {
            if (orderitem.Quantity == 1) {
                $.ajax({
                    type: 'Delete',
                    url: '/api/OrderItem/' + orderitem.Id,
                    contentType: 'application/json; charset=utf-8',
                    data: { id: orderitem.Id }
                }).done(function (data) {
                    self.init();
                    self.initialorder();
                }).fail(self.showError)
            } else {
                var data = {
                    id: orderitem.Id, // id of orderitem to be passed in the put request
                    quantity: orderitem.Quantity - 1,
                    menuItemid: self.menuItemId,
                    orderid: self.orderId,
                    customerid: self.customerId
                }
                $.ajax({
                    type: 'PUT',
                    url: '/api/OrderItem/' + orderitem.Id,
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(data)
                }).done(function (result) {
                    self.init();
                    self.initialorder();
                }).fail(self.showError);
            }
        } else {
            if (self.currentorder().OrderStatus == "inprogress") {
                alertify.error('Sorry, Your order is in progress.You cannot delete')
            }
            if (self.currentorder().OrderStatus == "completed") {
                alertify.error('Sorry, Your order is already finished.You cannot delete')
            }
        }
    }

    self.deleteall = function (orderitem) {
        self.orderitemtodelete_id(orderitem.Id);
        self.orderitemtodelete_name(orderitem.MenuItem.Name);
        if (self.currentorder().OrderStatus != "inprogress" && self.currentorder().OrderStatus != "completed") {
            $.ajax({
                type: 'Delete',
                url: '/api/OrderItem/' + self.orderitemtodelete_id(),
                contentType: 'application/json; charset=utf-8',
                data: { id: self.orderitemtodelete_id() }
            }).done(function (data) {
                $('#myModal').modal('hide')
                self.init();
                self.initialorder();
                console.log(1);
            }).fail(self.showError)
        } else {
            if (self.currentorder().OrderStatus == "inprogress") {
                alertify.error('Sorry,Your order is in progress.You cannot delete');
            }
            if (self.currentorder().OrderStatus == "completed") {
                alertify.error('Sorry,Your order is already fnished.You cannot delete');
            }
        }
    }

    self.total = ko.computed(function () {
        var total = 0;
        if (self.chosenDeliveryMethod() == "Take Away") {

            for (var p = 0; p < self.orderItems().length; ++p) {
                total += (self.orderItems()[p].MenuItem.Price) * (self.orderItems()[p].Quantity);
            }

        } else {
            total = self.deliveryFee();
            for (var p = 0; p < self.orderItems().length; ++p) {
                total += (self.orderItems()[p].MenuItem.Price) * (self.orderItems()[p].Quantity);
            }
        }
        return total;
    });
    self.enterdeliveryplaceandpaymentmethod = function () {

    }

    self.checkOut = function () {
        if (self.userId() != undefined && self.userId() != '') {
            console.log(self.total());
            console.log(self.model().limitedCredit());

            if (self.total() < self.model().limitedCredit()) {

                var result = self.model().limitedCredit() - self.total();
                self.model().limitedCredit(result);
                console.log(result);
                var data = {
                    customer: { limitedCredit: result },
                    Id: self.orderId(),
                    paymentmethod: self.chosenpaymentmethod(),
                    paymentdone: self.currentorder().PaymentDone,
                    deliveryplace: self.deliveryplace(),
                    deliverytime: new Date(),
                    ordertime: new Date(),
                    orderstatus: "waiting"
                }
                $.ajax({
                    type: 'Put',
                    url: '/api/Order/' + self.orderId(),
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(data)
                }).done(function (result) {
                    //self.init();
                    //self.initialorder();
                    document.location = '/Customer/Order/OrderStatus';
                    alertify.success('Delivery Place and Payment Method are Updated');
                }).fail(self.showError);
            } else {
                document.location = '/login/login';
            }
        }
    }


    //self.getCustomerById = function () {
    //    if (self.userId() != undefined && self.userId() != '') {
    //        //console.log(self.userId());
    //        $.ajax({
    //            type: 'Get',
    //            url: '/api/Customer/' + self.userId(),
    //            contentType: 'application/json; charset=utf-8'
    //        }).done(function (result) {
    //            var data = result.customer;
    //            //console.log(data);
    //            self.customerId(data.Id);
    //            self.model().limitedCredit(data.LimitedCredit);
    //            self.model().firstName(data.User.FirstName);
    //            self.model().lastName(data.User.LastName);
    //            self.model().email(data.User.Email);
    //            self.model().userName(data.User.UserName);
    //            self.model().password(data.User.PasswordHash);
    //            self.model().phoneNumber(data.User.PhoneNumber);
    //            console.log(self.customerId());
    //            self.init();
    //        }).fail(self.showError);
    //    }
    //};
    //self.getCustomerById();

    //self.getCasherOrders = function () {
    //    $.ajax({
    //                type: 'Get',
    //                url: '/api/order/GetAllbyCustomerId/'+ self.customerId(),
    //                contentType: 'application/json; charset=utf-8'
    //            }).done(function (data) {
    //                self.casherorders(data.orders);
    //            }).fail(self.showError);
    //}
    //self.getCasherOrders();

    



    self.cancel = function () {
        document.location = '/Customer/Cafeteria/Index';
    }

}