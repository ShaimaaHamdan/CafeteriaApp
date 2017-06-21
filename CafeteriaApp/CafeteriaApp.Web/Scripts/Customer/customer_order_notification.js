function customer_order_notification_ViewModel() {

    self.currentcustomer = ko.observable();
    self.customer_id = ko.observable(6);
    self.order_id = ko.observable();
    self.ordernotifications = ko.observableArray();

    self.showerror = function (jqXHR) {

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

    self.get_customer_by_id = function () {
        $.ajax({
            type: 'Get',
            url: '/api/Get/' + 6,
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            console.log(1);
            self.customer(data);
        }).fail(self.showerror);
    }
    self.get_customer_by_id();

    self.getnotifications = function () {
        $.ajax({
            type: 'Get',
            url: '/api/OrderNotification/Getbycustomer/' + self.customer_id(),
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            console.log(1);
            self.ordernotifications(data);
        }).fail(self.showerror);
    }
    self.getnotifications();
}