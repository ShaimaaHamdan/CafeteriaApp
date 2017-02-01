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
            data:{id:self.categoryId()}
        }).done(function (data) {
            console.log(data)
            $('#myModal').modal('hide')
            self.getAllCategories();
        }).fail(self.showError);

    }
}


//function MenuItemEditViewModel(id) {

//    var self = this;
//    self.menuItemId = ko.observable(id);
//    self.description = ko.observable();
//    self.name = ko.observable();
//    self.type = ko.observable();
//    self.price = ko.observable();

//    self.showError = function (jqXHR) {

//        self.result(jqXHR.status + ': ' + jqXHR.statusText);

//        var response = jqXHR.responseJSON;
//        if (response) {
//            if (response.Message) self.errors.push(response.Message);
//            if (response.ModelState) {
//                var modelState = response.ModelState;
//                for (var prop in modelState) {
//                    if (modelState.hasOwnProperty(prop)) {
//                        var msgArr = modelState[prop]; // expect array here
//                        if (msgArr.length) {
//                            for (var i = 0; i < msgArr.length; ++i) self.errors.push(msgArr[i]);
//                        }
//                    }
//                }
//            }
//            if (response.error) self.errors.push(response.error);
//            if (response.error_description) self.errors.push(response.error_description);
//        }
//    }

//    self.getMenuItemById = function () {
//        console.log(self.menuItemId())
//        $.ajax({
//            type: 'Get',
//            url: '/api/MenuItem/' + self.menuItemId(),
//            contentType: 'application/json; charset=utf-8',
//        }).done(function (data) {
//            console.log(data)
//            self.description(data.Description)
//            self.name(data.Name)
//            self.type(data.Type)
//            self.price(data.Price)
//        }).fail(self.showError);
//    };

    
//    self.getMenuItemById();

//    self.save = function () {
//        var data = {
//            id: self.menuItemId(),
//            name: self.name(),
//            price: self.price(),
//            description: self.description(),
//            type: self.type()
//        }
//        $.ajax({
//            type: 'PUT',
//            url: '/api/menuitem',
//            contentType: 'application/json; charset=utf-8',
//            data: JSON.stringify(data)
//        }).done(function (result) {
//            console.log(result)
//            document.location = '/admin/menuitem/index';
//        }).fail(self.showError);

//    }



//}


