﻿function CustomerMenuItemViewModel(id) {
    var self = this;
    self.categoryId = ko.observable(id);
    self.customerId = ko.observable(7);
    self.menuItemId = ko.observable();
    self.orderId = ko.observable();
    self.showfavorite = ko.observable();
    self.currentorder = ko.observable();
    self.currentorderstatus = ko.observable();
    self.makeorderclicked = ko.observable();
    self.editorderclicked = ko.observable();
    self.deleteallclicked = ko.observable(0);
    self.paymentmethod = ko.observableArray(["credit", "cash"]);
    self.chosenpaymentmethod = ko.observable();
    self.deliveryplace = ko.observable();
    self.menuItems = ko.observableArray();
    //self.menuItem = ko.observable();
    self.currentmenuitemId = ko.observable();
    self.viewdetailsclicked = ko.observable();
    self.favoriteItems = ko.observableArray();
    //--------------------------------------------
    self.commentId = ko.observable();
    self.comments = ko.observableArray();
    self.comment_data = ko.observable();
    self.commentedit_data = ko.observable();
    self.viewcommentsclicked = ko.observable();
    self.editcommentclicked = ko.observable(0);
    //--------------------------------------------
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
        //file: ko.observable(), // will be filled with a File object
        // Read the files (all are optional, e.g: if you're certain that it is a text file, use only text:
        //binaryString: ko.observable(), // FileReader.readAsBinaryString(Blob|File) - The result property will contain the file/blob's data as a binary string. Every byte is represented by an integer in the range [0..255].
       // text: ko.observable(), // FileReader.readAsText(Blob|File, opt_encoding) - The result property will contain the file/blob's data as a text string. By default the string is decoded as 'UTF-8'. Use the optional encoding parameter can specify a different format.
       // dataURL: ko.observable(), // FileReader.readAsDataURL(Blob|File) - The result property will contain the file/blob's data encoded as a data URL.
       // arrayBuffer: ko.observable(), // FileReader.readAsArrayBuffer(Blob|File) - The result property will contain the file/blob's data as an ArrayBuffer object.

        // a special observable (optional)
        base64String: ko.observable(), // just the base64 string, without mime type or anything else

        // you can have observable arrays for each of the properties above, useful in multiple file upload selection (see Multiple file Uploads section below)
        // in the format of xxxArray: ko.observableArray(),
        /* e.g: */// fileArray: ko.observableArray(), base64StringArray: ko.observableArray()
    });
    //TODO need to get the id from the logged in user
    //self.customerId = ko.observable(7);
    
    self.quantity = ko.observable(1);
    self.comments(null);
    self.orderItems = ko.observableArray();
    self.orderitemtodelete_id = ko.observable();
    self.orderitemtodelete_name = ko.observable();
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

    $('#myModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)[0];
        self.orderitemtodelete_id(button.attributes["orderitemtodelete_id"].value)
        self.orderitemtodelete_name(button.attributes["orderitemtodelete_name"].value)
    });

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

    self.getCustomerById = function () {
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
            //self.fileData().dataURL('data:image/gif;base64,' + data.User.ImageData);
            //self.fileData().base64String(data.User.ImageData);
        }).fail(self.showError);
    };

    self.getCustomerById();

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
                    self.currentorder(data.order);
                    self.currentorderstatus(data.order.OrderStatus);
                    self.deliveryplace(data.order.DeliveryPlace);
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
        }
    })();

    self.viewdetails = function (menuitem) {
        self.viewdetailsclicked(1);
        self.viewcommentsclicked(0);
        if (self.currentmenuitemId() != menuitem.Id) {
            self.currentmenuitemId(menuitem.Id);
        }
    };
    self.hidedetails = function (menuitem) {
        self.viewdetailsclicked(0);
        self.currentmenuitemId(-1);
    };
    // orders

    (self.initialorder = function () {
        if (self.makeorderclicked() == 1) {
            $.ajax({
                type: 'Get',
                url: '/api/order/Getlastorder/' + self.customerId(),
                contentType: 'application/json; charset=utf-8'
            }).done(function (data) {
                self.editorder(data.order);
                self.editorderclicked(1);
            }).fail(self.showError);
        }
        else if (self.editorderclicked() == 1) {            
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
            }).fail(self.showError);
        }
    })();
   
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

    self.enterdeliveryplaceandpaymentmethod = function () {
        var data = {
            Id: self.orderId(),
            paymentmethod: self.chosenpaymentmethod(),
            paymentdone: self.currentorder().PaymentDone,
            deliveryplace: self.deliveryplace(),
            deliverytime: self.currentorder().DeliveryTime,
            ordertime: self.currentorder().OrderTime,
            orderstatus: self.currentorder().OrderStatus
        }
        $.ajax({
            type: 'Put',
            url: '/api/Order/' + self.orderId(),
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (result) {
            self.init();
            self.initialorder();
            alertify.success('Delivery Place and Payment Method are Updated');
        }).fail(self.showError);
    }

    self.hide = function (order) {
        if (self.orderId() == order.Id) {
            self.makeorderclicked(0);
            self.editorderclicked(0);
        }
    }

    // order items

    self.addToCart = function (menuItem) {
        var x=self.orderItems().filter(e=>  e.MenuItemId == menuItem.Id)[0];
        if (x!=null) {
            self.addanother(x);
        }
        else {
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
        
    }

    self.addanother = function (orderitem) {
        var data = {
            id: orderitem.Id,  // id of orderitem to be passed in the put request
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
                    self.init();
                    self.initialorder();
                }).fail(self.showError);
            }
        }
        else {
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
        }
        else {
            if (self.currentorder().OrderStatus == "inprogress") {
                alertify.error('Sorry,Your order is in progress.You cannot delete');
            }
            if (self.currentorder().OrderStatus == "completed") {
                alertify.error('Sorry,Your order is already fnished.You cannot delete');
            }
        }
    }

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
            }).fail(self.showError);
        } else {
            alertify.error("Error,");
        }
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

    }

    self.cancel = function () {
        document.location = '/Customer/Cafeteria/Index';
    }

    self.total = ko.computed(function () {
        var total = 0;
        for (var p = 0; p < self.orderItems().length; ++p) {
            total += (self.orderItems()[p].MenuItem.Price) * (self.orderItems()[p].Quantity);
        }
        return total;
    });

    // comments   
    
    self.addcomment = function (menuitem) {
        if (self.comment_data() == null) {
            alertify.error("Please Enter Comment");
        }
        else {
            var data = {
                data: self.comment_data(),
                menuItemid: menuitem.Id,
                customerid: self.customerId()
            }
            $.ajax({
                type: 'Post',
                url: '/api/Comment',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(data)
            }).done(function (result) {
                alertify.success("Your comment is added");
                self.init();
                self.getCommentByMenuItemId(menuitem);
                self.comment_data(null);
            }).fail(self.showError);
        }

    }  

    self.getCommentByMenuItemId = function (menuItem) {
        if (menuItem.Comments.length!=0) {
            $.ajax({
                type: 'Get',
                url: '/api/Comment/GetByMenuItem/' + menuItem.Id,
                contentType: 'application/json; charset=utf-8'
            }).done(function (data) {
               // self.menuItem(menuItem);
                self.menuItemId(menuItem.Id);
                self.comments(data.comments)  ///!!!
                self.init();
                self.viewcommentsclicked(1);
            }).fail(self.showError);

        }
    }  
  
    self.editcomment = function (comment) {
        var data = {
            id: comment.Id,
            data: self.commentedit_data(),
            menuitemid: comment.MenuItemId,
            customerid: self.customerId(),
        }
        $.ajax({
            type: 'Put',
            url: '/api/comment/' + comment.Id,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function () {
            self.editcommentclicked(0);
            self.getCommentByMenuItemId(self.menuItem());
            self.commentedit_data(null);
        }).fail(self.showError);
    }

    //-------------------------------------------------------------------
    self.deletecomment = function (comment) {
        $.ajax({
            type: 'Delete',
            url: '/api/Comment/' + comment.Id,
            contentType: 'application/json; charset=utf-8',
            data: { id: comment.Id }
        }).done(function (data) {
            self.getCommentByMenuItemId(self.menuItem());
            alertify.success("Your Comment is deleted");
        }).fail(self.showError);
    }
    //----------------------------------------------------------------

    self.hidecomments = function (menuitem) {
        if (self.menuItemId() == menuitem.Id) {
            self.comments(null);
        }
        self.viewcommentsclicked(0);
    }
    self.showcommenteditbox = function (comment) {
        self.commentId(comment.Id);
        self.editcommentclicked(1);
    }
    self.hidecommenteditbox = function (comment) {
        self.editcommentclicked(0);
    }
    
    // favorite
    
    self.addfavorite = function (menuitem) {
        var x = self.favoriteItems().filter(e=>  e.MenuItemId == menuitem.Id);
        if (x.length != 0) {
            alertify.error("This Item is already in your Favorite Items");
        }
        else {
            var data = {
                menuitemid: menuitem.Id,
                customerid: self.customerId()
            }
            $.ajax({
                type: 'Post',
                url: '/api/FavoriteItem',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(data)
            }).done(function () {
                alertify.success("MenuItem is added to your Favorite Items");
                self.getfavorite();
            }).fail(self.showError);
        }
    }
    self.getfavorite = function () {
            self.showfavorite(1);
            $.ajax({
                type: 'Get',
                url: '/api/FavoriteItem/GetbyCustomerId/' + self.customerId(),
                contentType: 'application/json; charset=utf-8'
            }).done(function (data) {
                self.favoriteItems(data.favoriteitems);
            }).fail(self.showError);
    }

    self.deletefavorite = function (favoriteitem) {
        $.ajax({
            type: 'Delete',
            url: '/api/FavoriteItem/' + favoriteitem.Id,
            contentType: 'application/json; charset=utf-8',
            data: { id: favoriteitem.Id }
        }).done(function () {
            self.getfavorite();
        }).fail(self.showError);
    }

    self.hidefavorite = function () {
        self.showfavorite(0);
    }

}