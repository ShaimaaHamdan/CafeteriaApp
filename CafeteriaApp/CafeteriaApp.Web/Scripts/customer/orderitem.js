function OrderItemViewModel() {
    var self = this;
    self.id = ko.observable();
    //self.orderitems = ko.observableArray();
    self.menuitemid = ko.observable();
    self.orderid = ko.observable();
    self.quantity = ko.observable();




    self.getorderitem = function () {

        $.ajax({
            type: 'Get',
            url: '/api/OrderItem/'+self.id(),
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            self.orderitem(data.orderitem)
        }).fail(self.showError);
    };

    self.getorderitems();
}