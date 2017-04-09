function AddNewChildRestrictViewModel(childId) {
    var self = this;
    self.childId = ko.observable(childId);
    self.cafeterias = ko.observableArray();
    self.categories = ko.observableArray();
    self.menuItems = ko.observableArray();
    self.selectedCafetria = ko.observable();
    self.selectedcategory = ko.observable();
    self.selectedMenuItems = ko.observableArray();


    (self.getAllCafeterias = function() {
        $.ajax({
            type: 'Get',
            url: '/api/Cafeteria',
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            console.log(data)
            self.cafeterias(data.cafeterias)
        }).fail(self.showError);
    })();

    self.getAllCategories = function(cafId) {
        
        $.ajax({
            type: 'Get',
            url: '/api/Category/GetByCafetria/' + cafId,
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            self.categories(data.categories);
        }).fail(self.showError);
    };

    self.getAllMenuItems = function (catId) {

        $.ajax({
            type: 'Get',
            url: '/api/MenuItem/GetByCategory/' + catId,
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            console.log(data);
            self.menuItems(data.menuItems);
            if(data.menuItems.length > 0) {
                 self.selectedMenuItems.push(data.menuItems[0])
            }
        }).fail(self.showError);
    }


    self.selectedCafetria.subscribe(function (newTCaf) {
        console.log(newTCaf);
        self.getAllCategories(newTCaf[0].Id);

    });

    self.selectedcategory.subscribe(function (newCat) {
        console.log(newCat);
        self.getAllMenuItems(newCat[0].Id);

    });

    self.selectedMenuItems.subscribe(function (newmenuItem) {
        console.log(newmenuItem);

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
    self.save = function () {
        console.log(self.selectedMenuItems())
        var data = {
            id: self.childId(),
            restricts: self.selectedMenuItems(),
        }

        $.ajax({
            type: 'Post',
            url: '/api/customer/AddDependentRestricts',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (result) {
            console.log(result);
            document.location = '/customer/profile/editChild/'+ self.childId();
        }).fail(self.showError);
    }
}
