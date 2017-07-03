function orderViewModel() {
    var self = this;
    self.finishclicked = ko.observable(0);
    self.orders = ko.observableArray();
    self.id = ko.observable();
    self.customerid = ko.observable();
    self.paymentdone = ko.observable();
    self.deliverytime = ko.observable();
    self.ordertime = ko.observable();
    self.paymentmethod = ko.observable();
    self.orderstatus = ko.observable();
    self.deliveryplace = ko.observable();
    self.finishedorderId = ko.observable();
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
        self.finishedorderId(button.attributes["finishedorderId"].value);
    });
    self.getAllOrders = function () {
        $.ajax({
            type: 'Get',
            url: '/api/Order',
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            console.log(data)
            self.orders(data.orders)
        }).fail(self.showError);
    };
   
    self.getAllOrders();

    self.inprogress = function (order) {
        var data = {
            id: order.Id,
            customerid: order.CustomerId,
            paymentmethod: order.PaymentMethod,
            paymentdone: order.PaymentDone,
            orderstatus: "inprogress",
            ordertime: order.OrderTime,
            deliveryplace: order.DeliveryPlace,
            deliverytime: order.DeliveryTime
        }
        $.ajax({
            type: 'PUT',
            url: '/api/Order/' + order.Id,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (result) {
            console.log(result)
            self.getAllOrders();
        }).fail(self.showError);
    }
    self.waiting = function (order) {
        var data = {
            id: order.Id,
            customerid: order.customerid,
            paymentmethod: order.PaymentMethod,
            paymentdone: order.PaymentDone,
            orderstatus: "waiting",
            ordertime: order.OrderTime,
            deliveryplace: order.DeliveryPlace,
            deliverytime: order.DeliveryTime
        }
        $.ajax({
            type: 'PUT',
            url: '/api/Order/' + order.Id,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (result) {
            console.log(result);
            self.getAllOrders();
        }).fail(self.showError);
    }

    self.finish = function (order) {
        var data = {
            id: order.Id,
            customerid: order.customerid,
            paymentmethod: order.PaymentMethod,
            paymentdone: order.PaymentDone,
            orderstatus: "completed",
            ordertime: order.OrderTime,
            deliveryplace: order.DeliveryPlace,
            deliverytime: order.DeliveryTime
        }
        $.ajax({
            type: 'PUT',
            url: '/api/Order/' + order.Id,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            console.log(data);
            $('#myModal').modal('hide')
            self.getAllOrders();
            self.finishclicked(1);
        }).fail(self.showError);


        data = {
            data: "Order "+order.Id+"is finished",
            customerid: order.customerid,
            orderid: order.Id
        }
        $.ajax({
            type: 'POST',
            url: '/api/OrderNotification',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            console.log(1);
        }).fail(self.showError);
    }
   

}