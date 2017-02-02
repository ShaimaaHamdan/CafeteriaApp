﻿function CafeteriaViewModel() {
    var self = this;
    self.cafeterias = ko.observableArray();
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

    self.getAllCafeterias = function () {
        $.ajax({
            type: 'Get',
            url: '/api/Cafeteria',
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            console.log(data)
            self.cafeterias(data)
        }).fail(self.showError);
    };

    self.getAllCafeterias();


    $('#myModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)[0];
        self.cafeteriaId(button.attributes["cafeteriaid"].value)
    });


    self.deleteCafeteria = function () {
        console.log("id=" + self.cafeteriaId());
        $.ajax({
            type: 'Delete',
            url: '/api/Cafeteria/' + self.cafeteriaId(),
            contentType: 'application/json; charset=utf-8',
            data:{id:self.cafeteriaId()}
        }).done(function (data) {
            console.log(data)
            $('#myModal').modal('hide')
            self.getAllCafeterias();
        }).fail(self.showError);

    }
}


function CafeteriaEditViewModel(id) {

    var self = this;
    self.cafeteriaId = ko.observable(id);
    self.categoryId = ko.observable();
    self.categories = ko.observableArray();
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

    self.getCategoryByCafeteriaId = function () {
        console.log(self.cafeteriaId())
        $.ajax({
            type: 'Get',
            url: '/api/Category/GetByCafetria/' + self.cafeteriaId(),
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            console.log(data)
            self.categories(data)        
        }).fail(self.showError);
    };

    
    self.getCategoryByCafeteriaId();

   
   //add

    


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
            self.getCategoryByCafeteriaId();
        }).fail(self.showError);

    }

    
        
}
