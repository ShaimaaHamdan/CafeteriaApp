function ProfileEditeViewModel() {
    var self = this;
    self.customerId = ko.observable(23);
    self.childeren = ko.observableArray();
    self.childIdToDelete = ko.observable();
    self.chosenschoolyear = ko.observable();
    self.model = ko.validatedObservable({
        firstName: ko.observable().extend({ required: true, maxLength: 100 }),
        lastName: ko.observable().extend({ required: true, maxLength: 100 }),
        userName: ko.observable().extend({ required: true, maxLength: 100 }),
        email: ko.observable().extend({ required: true, pattern: '^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$' }),
        phoneNumber: ko.observable().extend({ required: true, pattern: '^01[0-2]{1}[0-9]{8}' }),
        credit: ko.observable().extend({ required: true, pattern: '^[+-]?[0-9]{1,3}(?:[0-9]*(?:[.,][0-9]{2})?|(?:,[0-9]{3})*(?:\.[0-9]{2})?|(?:\.[0-9]{3})*(?:,[0-9]{2})?)$' }),
        password:ko.observable()
    });
    ko.fileBindings.defaultOptions.buttonText = "Choose Image";
    self.fileData = ko.observable({
        file: ko.observable(), // will be filled with a File object
        // Read the files (all are optional, e.g: if you're certain that it is a text file, use only text:
        binaryString: ko.observable(), // FileReader.readAsBinaryString(Blob|File) - The result property will contain the file/blob's data as a binary string. Every byte is represented by an integer in the range [0..255].
        text: ko.observable(), // FileReader.readAsText(Blob|File, opt_encoding) - The result property will contain the file/blob's data as a text string. By default the string is decoded as 'UTF-8'. Use the optional encoding parameter can specify a different format.
        dataURL: ko.observable(), // FileReader.readAsDataURL(Blob|File) - The result property will contain the file/blob's data encoded as a data URL.
        arrayBuffer: ko.observable(), // FileReader.readAsArrayBuffer(Blob|File) - The result property will contain the file/blob's data as an ArrayBuffer object.

        // a special observable (optional)
        base64String: ko.observable(), // just the base64 string, without mime type or anything else

        // you can have observable arrays for each of the properties above, useful in multiple file upload selection (see Multiple file Uploads section below)
        // in the format of xxxArray: ko.observableArray(),
        /* e.g: */ fileArray: ko.observableArray(), base64StringArray: ko.observableArray()
    });
    self.result = ko.observable();
    
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

    self.getCustomerById = function () {
        console.log(self.customerId());
        $.ajax({
            type: 'Get',
            url: '/api/Customer/' + self.customerId(),
            contentType: 'application/json; charset=utf-8'
        }).done(function (result) {
            var data = result.customer;
            console.log(data);
            self.model().firstName(data.User.FirstName);
            self.model().lastName(data.User.LastName);
            self.model().email(data.User.Email);
            self.model().userName(data.User.UserName);
            self.model().password(data.User.PasswordHash);
            self.model().phoneNumber(data.User.PhoneNumber);
            self.model().credit(data.LimitedCredit);
           
            self.fileData().dataURL('data:image/gif;base64,' + data.User.ImageData);
            self.fileData().base64String(data.User.ImageData);
           }).fail(self.showError);
    };
    self.getCustomerById();

    self.save = function () {
        if (self.model.isValid()) {
            var data = {
                id: self.customerId(),
                user: {
                    firstName: self.model().firstName(),
                    lastName: self.model().lastName(),
                    email: self.model().email(),
                    userName: self.model().userName(),
                    password: self.model().password(),
                    phoneNumber: self.model().phoneNumber(),
                    imageData: self.fileData().base64String()
                },
                credit:self.model().credit()
            }
            
            $.ajax({
                type: 'PUT',
                url: '/api/customer',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(data)
            }).done(function (result) {
                console.log(result);
                alertify.success("Changes to your profile updated successfully !!");

                document.location = '/Customer/cafeteria/index';
                console.log(password);
            }).fail(self.showError);
        } else {
            alertify.error("Error,Some fields are invalid !");
        }

    }

    self.cancel = function () {
        document.location = '/Customer/cafeteria/index/';
    }

    self.getChildByCustomerId = function () {
        console.log(self.customerId()),
        $.ajax({
            type: 'Get',
            url: '/api/Customer/GetDependentByCustomer/' + self.customerId(),
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            console.log(data);
            self.childeren(data.dependents);
        }).fail(self.showError);
    };
    self.getChildByCustomerId();

  

    $('#mydeleteModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)[0];
        self.childIdToDelete(button.attributes["childid"].value);
        
    });

    self.deleteCustomerChild = function () {
        var model = {childId: self.childIdToDelete(), customerId: self.customerId()};
        $.ajax({
            type: 'Delete',
            url: '/api/Customer/DeleteCustomerDependent/',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(model)
        }).done(function (data) {
            console.log(data)
            $('#mydeleteModal').modal('hide')
            alertify.success("Child is deleted");
            self.getChildByCustomerId();
        }).fail(self.showError);
    }
}

function ChildNewViewModel() {
    var self = this;
    self.schoolYear = ko.observableArray(["First", "Second","Third"]);
    self.chosenschoolyear = ko.observable();
    self.customerId = ko.observable(6);
   // self.schoolYear = ko.observable();
    self.model = ko.validatedObservable({
        name: ko.observable().extend({ required: true, maxLength: 100 }),
        age: ko.observable().extend({required: true, pattern: '^(1[0-8]|[1-9])$' }),
        dependentCredit: ko.observable().extend({ required: true, pattern: '^[+-]?[0-9]{1,3}(?:[0-9]*(?:[.,][0-9]{2})?|(?:,[0-9]{3})*(?:\.[0-9]{2})?|(?:\.[0-9]{3})*(?:,[0-9]{2})?)$' }),
        dayLimit: ko.observable().extend({ required: true, pattern: '^[+-]?[0-9]{1,3}(?:[0-9]*(?:[.,][0-9]{2})?|(?:,[0-9]{3})*(?:\.[0-9]{2})?|(?:\.[0-9]{3})*(?:,[0-9]{2})?)$' })
    });
    ko.fileBindings.defaultOptions.buttonText = "Choose Image";
    self.fileData = ko.observable({
        file: ko.observable(), // will be filled with a File object
        // Read the files (all are optional, e.g: if you're certain that it is a text file, use only text:
        binaryString: ko.observable(), // FileReader.readAsBinaryString(Blob|File) - The result property will contain the file/blob's data as a binary string. Every byte is represented by an integer in the range [0..255].
        text: ko.observable(), // FileReader.readAsText(Blob|File, opt_encoding) - The result property will contain the file/blob's data as a text string. By default the string is decoded as 'UTF-8'. Use the optional encoding parameter can specify a different format.
        dataURL: ko.observable(), // FileReader.readAsDataURL(Blob|File) - The result property will contain the file/blob's data encoded as a data URL.
        arrayBuffer: ko.observable(), // FileReader.readAsArrayBuffer(Blob|File) - The result property will contain the file/blob's data as an ArrayBuffer object.

        // a special observable (optional)
        base64String: ko.observable(), // just the base64 string, without mime type or anything else

        // you can have observable arrays for each of the properties above, useful in multiple file upload selection (see Multiple file Uploads section below)
        // in the format of xxxArray: ko.observableArray(),
        /* e.g: */ fileArray: ko.observableArray(), base64StringArray: ko.observableArray(),
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
                customerId: self.customerId(),
                name: self.model().name(),
                age: self.model().age(),
                dependentCredit: self.model().dependentCredit(),
                dayLimit: self.model().dayLimit(),
                imageData: self.fileData().base64String(),
                schoolYear: self.chosenschoolyear()
            }

            console.log(data);
            $.ajax({
                type: 'Post',
                url: '/api/customer/addDependents',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(data)
            }).done(function (result) {
                console.log(result);
                document.location = '/customer/profile/index/';
            }).fail(self.showError);
        } else {
            alertify.error("Error,Some fields are invalid !");
        }
    }
}

function ChildEditViewModel(id) {
    self.result = ko.observable();
    self.customerId = ko.observable(6);
    self.childId = ko.observable(id);
    self.restrictionId = ko.observable();
    self.restrictions = ko.observableArray();
    // self.schoolYear = ko.observable();
    self.schoolYear = ko.observableArray(["First", "Second", "Third"]);
    self.chosenschoolyear = ko.observable();
    self.model = ko.validatedObservable({
        name: ko.observable().extend({ required: true, maxLength: 100 }),
        age: ko.observable().extend({ required: true, pattern: '^(1[0-8]|[1-9])$' }),
        dependentCredit: ko.observable().extend({ required: true, pattern: '^[+-]?[0-9]{1,3}(?:[0-9]*(?:[.,][0-9]{2})?|(?:,[0-9]{3})*(?:\.[0-9]{2})?|(?:\.[0-9]{3})*(?:,[0-9]{2})?)$' }),
        dayLimit: ko.observable().extend({ required: true, pattern: '^[+-]?[0-9]{1,3}(?:[0-9]*(?:[.,][0-9]{2})?|(?:,[0-9]{3})*(?:\.[0-9]{2})?|(?:\.[0-9]{3})*(?:,[0-9]{2})?)$' }),
        //schoolYear: ko.observable()

    });
    ko.fileBindings.defaultOptions.buttonText = "Choose Image";
    self.fileData = ko.observable({
        file: ko.observable(), // will be filled with a File object
        // Read the files (all are optional, e.g: if you're certain that it is a text file, use only text:
        binaryString: ko.observable(), // FileReader.readAsBinaryString(Blob|File) - The result property will contain the file/blob's data as a binary string. Every byte is represented by an integer in the range [0..255].
        text: ko.observable(), // FileReader.readAsText(Blob|File, opt_encoding) - The result property will contain the file/blob's data as a text string. By default the string is decoded as 'UTF-8'. Use the optional encoding parameter can specify a different format.
        dataURL: ko.observable(), // FileReader.readAsDataURL(Blob|File) - The result property will contain the file/blob's data encoded as a data URL.
        arrayBuffer: ko.observable(), // FileReader.readAsArrayBuffer(Blob|File) - The result property will contain the file/blob's data as an ArrayBuffer object.

        // a special observable (optional)
        base64String: ko.observable(), // just the base64 string, without mime type or anything else

        // you can have observable arrays for each of the properties above, useful in multiple file upload selection (see Multiple file Uploads section below)
        // in the format of xxxArray: ko.observableArray(),
        /* e.g: */ fileArray: ko.observableArray(), base64StringArray: ko.observableArray(),
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
    //Get Child by id
    self.getChildById = function () {
        console.log(self.childId());
        $.ajax({
            type: 'Get',
            url: '/api/Customer/GetDependentById/' + self.childId(),
            contentType: 'application/json; charset=utf-8'
        }).done(function (result) {
            var data = result.dependent;
            console.log(data);
            self.model().name(data.Name);
            self.model().age(data.Age);
            self.model().dependentCredit(data.DependentCredit);
            self.model().dayLimit(data.DayLimit);
            self.fileData().dataURL('data:image/gif;base64,' + data.ImageData);
            self.fileData().base64String(data.ImageData);
            self.restrictions(data.Restricts);
            self.chosenschoolyear(data.SchoolYear)
        }).fail(self.showError);
    };
    self.getChildById();

    self.save = function () {
        //var chosyear = "";
        //if (self.schoolYear().length > 0) {
        //     self.chosenschoolyear(self.schoolYear()[0]);
        //} else
        //    self.chosenschoolyear("First");

        if (self.model.isValid()) {
            var data = {
                name: self.model().name(),
                id: self.childId(),
                imageData: self.fileData().base64String(),
                age: self.model().age(),
                schoolYear:self.chosenschoolyear(),
                dependentCredit: self.model().dependentCredit(),
                dayLimit: self.model().dayLimit()
            }

            $.ajax({
                type: 'PUT',
                url: '/api/customer/editDependent',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(data)
            }).done(function (result) {

                alertify.success("Changes to your Child Info updated successfully !!");
                document.location = '/customer/profile/index';
            }).fail(self.showError);
        } else {
            alertify.error("Error,Some fields are invalid !");
        }

    }

    $('#mydeleteModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)[0];
        self.restrictionId(button.attributes["restrictionId"].value);

    });

    self.deleteRestriction = function () {
        console.log("id=" + self.restrictionId());
        $.ajax({
            type: 'Delete',
            url: '/api/Customer/DeleteDependentRestricts/' + self.restrictionId(),
            contentType: 'application/json; charset=utf-8'
            
        }).done(function (data) {
            console.log(data);
            $('#myModal').modal('hide');
            alertify.success( " restriction is deleted ");
            self.getChildById();

        }).fail(self.showError);

    }
    //Save and redirect to profile page

    //Get All Restrict and bind it to grid

    
}