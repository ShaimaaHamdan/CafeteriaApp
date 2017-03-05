function CustomerMenuItemViewModel(id) {
    var self = this;
    self.categoryId = ko.observable(id);
    self.menuItemId = ko.observable();
    self.menuItems = ko.observableArray();
    self.cafeteriaId = ko.observable();
    self.name = ko.observable();
    self.model = ko.validatedObservable({
        name: ko.observable().extend({ required: true, maxLength: 100 })
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
            self.model().name(data.category.Name);
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

    //$('#myModal').on('show.bs.modal', function (event) {
    //    var button = $(event.relatedTarget)[0];
    //    self.menuItemId(button.attributes["menuItemid"].value)
    //    self.name(button.attributes["name"].value)
    //});


}
