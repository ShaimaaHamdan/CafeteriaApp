function orderViewModel() {
    var self = this;
    self.orders = ko.observableArray();
    self.id = ko.observable();
    self.customerid = ko.observable();
    self.paymentdone = ko.observable();
    self.deliverytime = ko.observable();
    self.ordertime = ko.observable();
    self.paymentmethod = ko.observable();
    self.orderstatus = ko.observable();
    self.deliveryplace = ko.observable();
    self.orderitems = ko.observableArray();
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
}