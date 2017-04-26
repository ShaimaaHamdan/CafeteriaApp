﻿function CustomerMenuItemViewModel(id) {
    var self = this;
    self.categoryId = ko.observable(id);
    self.customerId = ko.observable(7);
    self.menuItemId = ko.observable();
    self.orderId = ko.observable();
    self.makeorderclicked = ko.observable();
    self.editorderclicked = ko.observable();
    self.menuItems = ko.observableArray();
    self.cafeteriaId = ko.observable();
    self.name = ko.observable();
    self.model = ko.validatedObservable({
        firstName: ko.observable().extend({ required: true, maxLength: 100 }),
        lastName: ko.observable().extend({ required: true, maxLength: 100 }),
        userName: ko.observable().extend({ required: true, maxLength: 100 }),
        email: ko.observable().extend({ required: true, pattern: '^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$' }),
        phoneNumber: ko.observable().extend({ required: true, pattern: '^01[0-2]{1}[0-9]{8}' }),
        limitedCredit: ko.observable().extend({ required: true, pattern: '^[+-]?[0-9]{1,3}(?:[0-9]*(?:[.,][0-9]{2})?|(?:,[0-9]{3})*(?:\.[0-9]{2})?|(?:\.[0-9]{3})*(?:,[0-9]{2})?)$' }),
        password: ko.observable()
    });
    ko.fileBindings.defaultOptions.buttonText = "Choose Image";
    self.fileData = ko.observable({
        file: ko.observable(), // will be filled with a File object
        // Read the files (all are optional, e.g: if you're certain that it is a text file, use only text:
        binaryString: ko.observable(), // FileReader.readAsBinaryString(Blob|File) - The result property will contain the file/blob's data as a binary string. Every byte is represented by an integer in the range [0..255].
        text: ko.observable(), // FileReader.readAsText(Blob|File, opt_encoding) - The result property will contain the file/blob's data as a text string. By default the string is decoded as 'UTF-8'. Use the optional encoding parameter can specify a different format.
        dataURL: ko.observable(), // FileReader.readAsDataURL(Blob|File) - The result property will contain the file/blob's data encoded as a data URL.
        arrayBuffer: ko.observable(), // FileReader.readAsArrayBuffer(Blob|File) - The result property will contain the file/blob's data as an ArrayBuffer object.

        // a special observable (optional)
        base64String: ko.observable(), // just the base64 string, without mime type or anything else

        // you can have observable arrays for each of the properties above, useful in multiple file upload selection (see Multiple file Uploads section below)
        // in the format of xxxArray: ko.observableArray(),
        /* e.g: */ fileArray: ko.observableArray(), base64StringArray: ko.observableArray()
    });
    //TODO need to get the id from the logged in user
    //self.customerId = ko.observable(7);
    
    self.quantity = ko.observable(1);

    self.orderItems = ko.observableArray();
    self.casherorders = ko.observableArray();
    (self.init = function () {
        //Get orderitems for current users for last order not checked out.
        if (self.customerId()!=7) { // if it's logged in user not from outside
            $.ajax({
                type: 'Get',
                url: '/api/order/GetbyCustomerId/' + self.customerId(),
                contentType: 'application/json; charset=utf-8'
            }).done(function (data) {  
                if (data.order != undefined) {
                    self.orderItems(data.order.OrderItems);
                    self.orderId(data.order.Id);
                }
            }).fail(self.showError);
        }
        else {
            $.ajax({
                type: 'Get',
                url: '/api/order/GetAllbyCustomerId/'+self.customerId(),
                contentType: 'application/json; charset=utf-8'
            }).done(function (data) {
                self.casherorders(data.orders);
            }).fail(self.showError);
            if (self.makeorderclicked() == 1) {
                $.ajax({
                    type: 'Get',
                    url: '/api/order/Getlastorder/' + self.customerId(),
                    contentType: 'application/json; charset=utf-8'
                }).done(function (data) {
                    self.orderId(data.order.Id);
                    self.orderItems(data.order.OrderItems);
                    console.log(self.orderId());
                }).fail(self.showError);
            }
            if (self.editorderclicked() == 1) {
                console.log(self.orderId());
                $.ajax({
                    type: 'Get',
                    url: '/api/order/' + self.orderId(),
                    contentType: 'application/json; charset=utf-8'
                }).done(function (data) {
                    self.orderItems(data.order.OrderItems);
                }).fail(self.showError);
            }
             
        }
    })();
    self.deleteorder = function (order) {
        self.makeorderclicked(0);
        self.editorderclicked(0);
        if (order.OrderItems != null) {
            for (var i = 0; i < order.OrderItems.length ; i++) {
                $.ajax({
                    type: 'Delete',
                    url: '/api/OrderItem/' + order.OrderItems[i].Id,
                    contentType: 'application/json; charset=utf-8',
                    data: { id: order.Id }
                }).done(function (data) {
                    self.init();
                }).fail(self.showError)
            }
        }
        $.ajax({
            type: 'Delete',
            url: '/api/Order/' + order.Id,
            contentType: 'application/json; charset=utf-8',
            data: { id: order.Id }
        }).done(function (data) {
            console.log(data);
            self.init();
        }).fail(self.showError)
    }
    self.hide = function (order) {
        if (self.orderId() == order.Id) {
            self.makeorderclicked(0);
            self.orderId(0);
            self.editorderclicked(0);
            self.init();
        }
    }
    self.editorder = function (order) {
        self.makeorderclicked(0);
        self.editorderclicked(1);
        self.orderId(order.Id);
        self.init();
    }
    self.MakeOrder = function () {
        self.makeorderclicked(1);
        //self.editorderclicked(1);
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
                console.log(result);
                self.init();
            }).fail(self.showError);
        }
    self.total = ko.computed(function () {
        var total = 0;
        if (self.orderItems() != null) {
            for (var p = 0; p < self.orderItems().length; ++p) {
                total += (self.orderItems()[p].MenuItem.Price) * (self.orderItems()[p].Quantity);
            }
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
        console.log(self.orderId());
        var x = 0;
        var z;
        if (self.orderItems() != undefined) {
            for (var i = 0; i < self.orderItems().length; ++i) {
                if (self.orderItems()[i].MenuItem.Id == menuItem.Id) {
                    x = 1;
                    z = self.orderItems()[i];
                    break;
                }
            }
        }
        if (x == 1) {
            self.addanother(z)
        }
        else {
            console.log(self.orderId());
            var data = {
                quantity: self.quantity(),
                menuItemid: menuItem.Id,
                orderid: self.orderId(),
                customerid: self.customerId()
            }
            console.log(self.orderId());
            $.ajax({
                type: 'Post',
                url: '/api/OrderItem',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(data)
            }).done(function (result) {
                self.init();
            }).fail(self.showError);
        }
        
    }
    self.addanother = function (orderitem) {
        console.log(orderitem.Id);
        console.log(orderitem.OrderId);
        var data = {
            id: orderitem.Id,  // id of orderitem to be passed in the put request
            quantity: orderitem.Quantity+1, // increasing the quantity
            menuItemid: self.menuItemId,
            orderid: self.orderId,
            customerid: self.customerId
        }
        console.log(self.orderItems());
        $.ajax({
            type: 'Put',
            url: '/api/OrderItem/'+orderitem.Id,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (result) {
            console.log(result);
            self.init();
        }).fail(self.showError);
        
    };
    self.deleteitem = function (orderitem) {
        $.ajax({
            type: 'Get',
            url: '/api/Order/' + orderitem.OrderId,
            contentType: 'application/json; charset=utf-8',
        }).done(function (result) {
            console.log(result)
            if ((result.order.OrderStatus != 'inprogress') && (result.order.OrderStatus != 'completed')) {
                if (orderitem.Quantity == 1) {
                    console.log("id=" + orderitem.Id);
                    $.ajax({
                        type: 'Delete',
                        url: '/api/OrderItem/' + orderitem.Id,
                        contentType: 'application/json; charset=utf-8',
                        data: { id: orderitem.Id }
                    }).done(function (data) {
                        console.log(data);
                    }).fail(self.showError)
                }
                else {
                    var data = {
                        id: orderitem.Id,  // id of orderitem to be passed in the put request
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
                        console.log(result);
                        self.init();
                    }).fail(self.showError);
                }
            }
            else {
                if (result.order.OrderStatus == "inprogress") {
                    alertify.error('Sorry, Your order is in progress.You cannot delete')
                }
                if (result.order.OrderStatus == "completed") {
                    alertify.error('Sorry, Your order is already finished.You cannot delete')
                }
            }
        }).fail(self.showError);
    }
    self.deleteall = function (orderitem) {
        $.ajax({
            type: 'Get',
            url: '/api/Order/' + orderitem.OrderId,
            contentType: 'application/json; charset=utf-8'
        }).done(function (result) {
            if (result.order.OrderStatus != "inprogress" && result.order.OrderStatus != "completed") {
                $.ajax({
                    type: 'Delete',
                    url: '/api/OrderItem/' + orderitem.Id,
                    contentType: 'application/json; charset=utf-8',
                    data: { id: orderitem.Id }
                }).done(function (data) {
                    console.log(data);
                    self.init();
                }).fail(self.showError)
            }
            else {
                if (result.order.OrderStatus=="inprogress")
                {
                    alertify.error('Sorry,Your order is in progress.You cannot delete');
                }
                if (result.order.OrderStatus=="completed")
                {
                    alertify.error('Sorry,Your order is already fnished.You cannot delete');
                }
            }
            }).fail(showError)
        
    }
    //self.enterdeliveryplace = function () {
    //    var data={
    //        id: self.orderId(),
    //        deliveryplace: self.order.deliveryplace,
    //        deliverytime: self.order.deliverytime,
    //        orderstatus: self.order.orderstatus,
    //        paymentmethod: self.order.paymentmethod,
    //        paymentdone: self.order.paymentdone
    //    }
    //    $.ajax({
    //        type: 'PUT',
    //        url: '/api/Order/' + self.orderId(),
    //        contentType: 'application/json; charset=utf-8',
    //        data: JSON.stringify(data)
    //    }).done(function (result) {
    //        console.log(result)
    //        document.location = '/customer/category/show/' + self.categoryId();
    //    }).fail(self.showError);

        
    //}
    ////self.enterpaymentmethod = function () {

    ////}
   

    self.getCustomerById = function () {
        console.log(self.customerId());
        $.ajax({
            type: 'Get',
            url: '/api/Customer/' + self.customerId(),
            contentType: 'application/json; charset=utf-8'
        }).done(function (result) {
            var data = result.customer;
            console.log(data);
            self.model().limitedCredit(data.LimitedCredit);
            self.model().firstName(data.User.FirstName);
            self.model().lastName(data.User.LastName);
            self.model().email(data.User.Email);
            self.model().userName(data.User.UserName);
            self.model().password(data.User.PasswordHash);
            self.model().phoneNumber(data.User.PhoneNumber);
            self.fileData().dataURL('data:image/gif;base64,' + data.User.ImageData);
            self.fileData().base64String(data.User.ImageData);
        }).fail(self.showError);
    };
    self.getCustomerById();
    //self.checkOut = function () {
    //    console.log(self.credit());
    //    console.log(self.total());
    //    if (self.total() > self.credit()) {
    //        alertify.error("Error,");
    //        } else {
    //        alertify.success("Done");
    //        var result = self.credit() - self.total();
    //        self.credit(result);
    //        console.log(result);
    //    }
    //};

    self.checkOut = function () {
        if (self.total() < self.model().limitedCredit()) {
           
            var result = self.model().limitedCredit() - self.total();
            self.model().limitedCredit(result);
            console.log(result);
            var data = {
                id: self.customerId(),
                user: {
                    firstName: self.model().firstName(),
                    lastName: self.model().lastName(),
                    email: self.model().email(),
                    userName: self.model().userName(),
                    password: self.model().password(),
                    phoneNumber: self.model().phoneNumber(),
                    imageData: self.fileData().base64String()
                },
                limitedCredit: result
            }
            $.ajax({
                type: 'PUT',
                url: '/api/customer',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(data)
            }).done(function (result) {
                console.log(result);
                alertify.success("Done");
                document.location = '/customer/category/show/' + self.categoryId();
                }).fail(self.showError);
        } else {
            alertify.error("Error,");
        }
    }
    self.cancel = function () {
        document.location = '/Customer/Cafeteria/Index';
    }

}
