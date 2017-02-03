function CategoryViewModel() {
    var self = this;
    self.categories = ko.observableArray();
    self.categoryId = ko.observable();

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

    self.getAllCategories = function () {
        $.ajax({
            type: 'Get',
            url: '/api/Category',
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            console.log(data)
            self.categories(data)
        }).fail(self.showError);
    };

    self.getAllCategories();


    $('#myModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)[0];
        self.categoryId(button.attributes["categoryid"].value)
    });


    self.deleteCategory = function () {
        console.log("id=" + self.categoryId());
        $.ajax({
            type: 'Delete',
            url: '/api/Category/' + self.categoryId(),
            contentType: 'application/json; charset=utf-8',
            data: { id: self.categoryId() }
        }).done(function (data) {
            console.log(data)
            $('#myModal').modal('hide')
            self.getAllCategories();
        }).fail(self.showError);

    }
}

function CategoryEditViewModel(id) {

    var self = this;
    self.categoryId = ko.observable(id);
    self.menuItemId = ko.observable();
    self.menuItems = ko.observableArray();
    self.name = ko.observable();
    self.cafeteriaId = ko.observable();

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
            self.cafeteriaId(data.CafeteriaId);
            self.name(data.Name);
        }).fail(self.showError);
    };

    self.getCategory();

    self.save = function () {

        var data = {

            name: self.name(),
            id: self.categoryId()

        }
        $.ajax({
            type: 'PUT',
            url: '/api/Category/' + self.categoryId(),
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (result) {
            console.log(result)
            // document.location = '/admin/cafeteria/index';
        }).fail(self.showError);

    }

    self.cancel = function () {
        document.location = '/Admin/Cafeteria/edit/'+ self.cafeteriaId();
    }
    

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

    $('#myModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)[0];
        self.menuItemId(button.attributes["menuItemid"].value)
    });


    self.deleteMenuItem = function () {
        console.log("id=" + self.menuItemId());
        $.ajax({
            type: 'Delete',
            url: '/api/MenuItem/' + self.menuItemId(),
            contentType: 'application/json; charset=utf-8',
            //data:{id:self.menuItemId()}
        }).done(function (data) {
            console.log(data)
            $('#myModal').modal('hide')
            self.getMenuItemByCategoryId();
        }).fail(self.showError);

    }


}

function CategoryNewViewModel(cafetriaId) {
    var self = this;
    self.name = ko.observable();
    self.cafeteriaId = ko.observable(cafetriaId);

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
            cafeteriaId: self.cafeteriaId(),
            name: self.name()
        }
        console.log(data);
        $.ajax({
            type: 'Post',
            url: '/api/category',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (result) {
            console.log(result);
            document.location = '/admin/cafeteria/edit/' + self.cafeteriaId();
        }).fail(self.showError);

    }

    self.cancel = function () {
        document.location = '/Admin/Cafeteria/edit/' + self.cafeteriaId();
    }


}




