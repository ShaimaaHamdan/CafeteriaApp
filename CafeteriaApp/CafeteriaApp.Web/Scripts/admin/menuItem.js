function MenuItemViewModel() {
    var self = this;
    self.menuItems = ko.observableArray();
    self.menuItemId = ko.observable();

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

    self.getAllMenuItems = function () {
        $.ajax({
            type: 'Get',
            url: '/api/MenuItem',
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            console.log(data)
            self.menuItems(data)
        }).fail(self.showError);
    };

    self.getAllMenuItems();


   

}

function MenuItemEditViewModel(id) {

    var self = this;
    self.menuItemId = ko.observable(id);    
    self.categoryId = ko.observable();
    self.model = ko.validatedObservable({
        name: ko.observable().extend({ required: true, maxLength: 100 }),
        price: ko.observable().extend({ required: true,pattern:'^[+-]?[0-9]{1,3}(?:[0-9]*(?:[.,][0-9]{2})?|(?:,[0-9]{3})*(?:\.[0-9]{2})?|(?:\.[0-9]{3})*(?:,[0-9]{2})?)$'}),
        type: ko.observable().extend({ required: true, maxLength: 100 }),
        description: ko.observable().extend({ required: true, maxLength: 500 })
    });
    
    self.result = ko.observable();
    self.errors = ko.observableArray([]);

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

    self.getMenuItemById = function () {
        console.log(self.menuItemId())
        $.ajax({
            type: 'Get',
            url: '/api/MenuItem/' + self.menuItemId(),
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            console.log(data)
            self.model().description(data.Description)
            self.model().name(data.Name)
            self.model().type(data.Type)
            self.model().price(data.Price)
            self.categoryId(data.CategoryId)
        }).fail(self.showError);
    };
    self.getMenuItemById();

    self.save = function () {
        if (self.model.isValid() ) {
            var data = {
                id: self.menuItemId(),
                name: self.model().name(),
                price: self.model().price(),
                description: self.model().description(),
                type: self.model().type()
            }
            $.ajax({
                type: 'PUT',
                url: '/api/menuitem',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(data)
            }).done(function (result) {
                console.log(result)
                document.location = '/admin/category/edit/' + self.categoryId();
            }).fail(self.showError);
        } else {
            alertify.error("Error,Some fileds are invalid !");
        }

    }

    self.cancel = function () {
        document.location = '/admin/category/edit/'+self.categoryId();
    }



}

function MenuItemNewViewModel(categoryId) {
    var self = this;
    self.description = ko.observable();
    self.name = ko.observable();
    self.type = ko.observable();
    self.price = ko.observable();
    self.categoryId = ko.observable(categoryId);

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

    self.save = function () {
        var data = {
            categoryId: self.categoryId(),
            name: self.name(),
            price: self.price(),
            description: self.description(),
            type: self.type()
        }
        console.log(data);
        $.ajax({
            type: 'Post',
            url: '/api/menuitem',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (result) {
            console.log(result);
            document.location = '/admin/category/edit/' + self.categoryId();
        }).fail(self.showError);

    }

    self.cancel = function () {
        document.location = '/admin/category/edit/' + self.categoryId();
    }
}

