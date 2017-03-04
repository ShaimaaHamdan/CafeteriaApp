function CustomerCategoryViewModel(id) {
    var self = this;
    self.cafeteriaId = ko.observable(id);
    self.categoryId = ko.observable();
    self.categories = ko.observableArray();
    self.menuItems = ko.observableArray();
    self.name = ko.observable();


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
    self.getCafeteria = function () {
        $.ajax({
            type: 'Get',
            url: '/api/Cafeteria/' + self.cafeteriaId(),
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            self.model().name(data.cafeteria.Name);
        }).fail(self.showError);
    };

    self.getCafeteria();


    self.getCategoryByCafeteriaId = function () {
        console.log(self.cafeteriaId())
        $.ajax({
            type: 'Get',
            url: '/api/Category/GetByCafetria/' + self.cafeteriaId(),
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            self.categories(data.categories);
        }).fail(self.showError);
    };


    self.getCategoryByCafeteriaId();


    self.getMenuItemByCategoryId = function () {
        console.log(self.categoryId())
        $.ajax({
            type: 'Get',
            url: '/api/MenuItem/GetByCategory/' + self.categoryId(),
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            console.log(data);
            self.menuItems(data.menuItems);
        }).fail(self.showError);
    };


    //self.getAllCategories = function () {
    //    $.ajax({
    //        type: 'Get',
    //        url: '/api/Category',
    //        contentType: 'application/json; charset=utf-8',
    //    }).done(function (data) {
    //        console.log(data)
    //        self.categories(data.categories)
    //    }).fail(self.showError);
    //};

    //self.getAllCategories();


    $('#myModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)[0];
        self.categoryId(button.attributes["categoryid"].value)

    });

}