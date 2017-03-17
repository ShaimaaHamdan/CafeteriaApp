function AdditionViewModel() {
    var self = this;
    self.additions = ko.observableArray();
    self.additionId = ko.observable();
    
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

    self.getAllAdditions = function () {
        $.ajax({
            type: 'Get',
            url: '/api/Addition',
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            console.log(data)
            self.additions(data.additions)
        }).fail(self.showError);
    };

    self.getAllAdditions();


    $('#myModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)[0];
        self.additionId(button.attributes["additionid"].value)
        self.name(button.attributes["name"].value)
        console.log(self.name());
        //self.getCategoryByCafeteriaId();
    });

    //self.getCategoryByCafeteriaId = function () {
    //    console.log(self.cafeteriaId())
    //    $.ajax({
    //        type: 'Get',
    //        url: '/api/Category/GetByCafetria/' + self.cafeteriaId(),
    //        contentType: 'application/json; charset=utf-8',
    //    }).done(function (data) {
    //        self.categories(data.categories);
    //    }).fail(self.showError);
    //};



    self.deleteAddition = function () {

       
            $.ajax({
                type: 'Delete',
                url: '/api/Addition/' + self.additionId(),
                contentType: 'application/json; charset=utf-8',
                data: { id: self.additionId() }
            }).done(function (data) {
                console.log(data)
                $('#myModal').modal('hide')
                alertify.success(self.name() + " addition is deleted ");
                self.getAllAdditions();
            }).fail(self.showError);
        } 

    
}

function AdditionEditViewModel(id) {

    var self = this;
    self.additionId = ko.observable(id);
    self.additions = ko.observableArray();
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
    self.getAddition = function () {
        $.ajax({
            type: 'Get',
            url: '/api/Addition/' + self.additionId(),
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            self.model().name(data.addition.Name);
        }).fail(self.showError);
    };

    self.getAddition();


    self.save = function () {

        if (self.model.isValid()) {
            var data = {
                name: self.model().name(),
                id: self.additionId(),
            }

            $.ajax({
                type: 'PUT',
                url: '/api/Addition/' + self.additionId(),
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(data)
            }).done(function (result) {
                document.location = '/admin/Addition/index';
            }).fail(self.showError);
        } else {
            alertify.error("Error,Some fileds are invalid !");
        }

    }

    self.cancel = function () {
        document.location = '/Admin/Addition/Index';
    }


}

function AdditionNewViewModel() {
    var self = this;
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

    self.save = function () {


        if (self.model.isValid()) {
            var data = {
                name: self.model().name(),               
            }
            console.log(data);
            $.ajax({
                type: 'Post',
                url: '/api/addition',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(data)
            }).done(function (result) {
                console.log(result);
                document.location = '/admin/addition/index';
            }).fail(self.showError);
        } else {

            alertify.error("Error,Some fileds are invalid !");

        }

    }

    self.cancel = function () {
        document.location = '/Admin/addition/Index';
    }

}