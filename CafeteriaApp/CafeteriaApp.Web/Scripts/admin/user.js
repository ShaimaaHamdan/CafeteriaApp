function userViewModel() {
    var self = this;
    self.users = ko.observableArray();
    self.userId = ko.observable();
    self.name = ko.observable();
    self.roles = ko.observableArray();
    self.selectedRoles = ko.observableArray();


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

    self.getAllusers = function () {
        $.ajax({
            type: 'Get',
            url: '/api/user',
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            console.log(data.users);
            self.users(data.users);
        }).fail(self.showError);
    };

    self.getAllusers();

    self.getAllRoles = function () {
        $.ajax({
            type: 'Get',
            url: '/api/user/Getallroles',
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            console.log(data)
            self.roles(data.roles);
        }).fail(function () {
            self.showError
        });
    };
    self.getAllRoles();


    self.assignRoles = function (user) {
        console.log(self.selectedRoles());
        var data = {
            id: user.Id,
            Roles: self.selectedRoles()
        }

        $.ajax({
            type: 'Put',
            url: '/api/user/assignRoles/' + user.Id,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            alertify.success("Roles are assigned successfully");
        }).fail(function () {
            self.showError
        });
    };
   


    $('#myModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)[0];
        self.userId(button.attributes["userid"].value)
        self.name(button.attributes["User.UserName"].value)
        console.log(self.name());      
    });

   



    //self.deleteuser = function () {

    //        $.ajax({
    //            type: 'Delete',
    //            url: '/api/Customer/' + self.userId(),
    //            contentType: 'application/json; charset=utf-8',
    //            data: { id: self.userId() }
    //        }).done(function (data) {
    //            console.log(data)
    //            $('#myModal').modal('hide')
    //            alertify.success(self.name() + " User is deleted ");
    //            self.getAllusers();
    //        }).fail(self.showError);
        

    //}
}


function userEditViewModel(id) {

    var self = this;
    self.userId = ko.observable(id);

    self.password = ko.observable(null),
    self.confirmPassword = ko.observable(null),
    //self.selectedRole = ko.observable();
    //self.selectedRole = ko.observable();
    //self.imageurl = ko.observable();
    //self.chooseimageclicked = ko.observable(0);
    //ko.fileBindings.defaultOptions.buttonText = "Choose Image";

    //self.fileData = ko.observable({
    //    // file: ko.observable(), // will be filled with a File object
    //    // Read the files (all are optional, e.g: if you're certain that it is a text file, use only text:
    //    // binaryString: ko.observable(), // FileReader.readAsBinaryString(Blob|File) - The result property will contain the file/blob's data as a binary string. Every byte is represented by an integer in the range [0..255].
    //    //text: ko.observable(), // FileReader.readAsText(Blob|File, opt_encoding) - The result property will contain the file/blob's data as a text string. By default the string is decoded as 'UTF-8'. Use the optional encoding parameter can specify a different format.
    //    dataURL: ko.observable(), // FileReader.readAsDataURL(Blob|File) - The result property will contain the file/blob's data encoded as a data URL.
    //    //arrayBuffer: ko.observable(), // FileReader.readAsArrayBuffer(Blob|File) - The result property will contain the file/blob's data as an ArrayBuffer object.

    //    // a special observable (optional)
    //    base64String: ko.observable(), // just the base64 string, without mime type or anything else

    //    // you can have observable arrays for each of the properties above, useful in multiple file upload selection (see Multiple file Uploads section below)
    //    // in the format of xxxArray: ko.observableArray(),
    //    /* e.g: */ //fileArray: ko.observableArray(), base64StringArray: ko.observableArray(),
    //});

    self.model = ko.observable({
        username: ko.observable(),//.extend({ required: true, maxLength: 100 }),
        firstname: ko.observable(),//.extend({ required: true, maxLength: 100 }),
        lastname: ko.observable(),//.extend({ required: true, maxLength: 100 }),
        //roles: ko.observableArray(),
        //selectedRoles: ko.observableArray(),
        //userName: ko.observable().extend({ required: true, maxLength: 100 }),
        email: ko.observable().extend({ required: true, pattern: '^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$' }),
        phoneNumber: ko.observable(),//.extend({ required: true, pattern: '^01[0-2]{1}[0-9]{8}' }),
        //credit: ko.observable().extend({ required: true, pattern: '^[+-]?[0-9]{1,3}(?:[0-9]*(?:[.,][0-9]{2})?|(?:,[0-9]{3})*(?:\.[0-9]{2})?|(?:\.[0-9]{3})*(?:,[0-9]{2})?)$' }),
        //limitedcredit: ko.observable().extend({ required: true, pattern: '^[+-]?[0-9]{1,3}(?:[0-9]*(?:[.,][0-9]{2})?|(?:,[0-9]{3})*(?:\.[0-9]{2})?|(?:\.[0-9]{3})*(?:,[0-9]{2})?)$' }),
        
        passwordHash: ko.observable(),
        securityStamp: ko.observable()
        //roles: ko.observableArray(["Admin", "Employee", "Customer"]),
        //selectedRole: ko.observable()
        //imageurl: ko.observable()
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


    self.getuser = function () {
        $.ajax({
            type: 'Get',
            url: '/api/user/' + self.userId(),
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            //var data = result.customer;
            console.log(data);
            self.userId(data.user.Id);
            self.model().username(data.user.UserName);
            self.model().firstname(data.user.FirstName);
            self.model().lastname(data.user.LastName);
            self.model().email(data.user.Email);
            //console.log(data.user.Email);
            //self.model().password(data.user.Password);
            //console.log(data.user.Password);
            self.model().passwordHash(data.user.PasswordHash);
            self.model().securityStamp(data.user.SecurityStamp);
            self.model().phoneNumber(data.user.PhoneNumber);
            //self.model().credit(data.Credit);
            //self.model().limitedcredit(data.LimitedCredit);
            //self.model().selectedRole(data.User.Roles);
            //self.model().imageurl(data.cafeteria.ImageUrl);
            //console.log(self);
            //console.log(self.model().imageurl());
            //self.fileData().dataURL('data:image/gif;base64,' + data.cafeteria.ImageData);
            //self.fileData().base64String(data.cafeteria.ImageData);
        }).fail(self.showError);
    };

    self.getuser();

    //self.getAllRoles = function () {
    //    $.ajax({
    //        type: 'Get',
    //        url: '/api/user/Getallroles',
    //        contentType: 'application/json; charset=utf-8',
    //    }).done(function (data) {
    //        self.roles(data);
    //        console.log(self.roles());
    //    }).fail(self.showError());
    //}
    

    self.save = function () {

        
        
        var data1 = {
            id: self.userId(),
                
            firstName: self.model().firstname(),
            lastName: self.model().lastname(),
            email: self.model().email(),
            userName: self.model().username(),
            ////Roles: self.model().selectedRoles(),
                        
            ////Password: self.model().password(),
            //PasswordHash: self.model().passwordHash(),
            phoneNumber: self.model().phoneNumber(),
            //SecurityStamp: self.model().securityStamp()
        }
        var data2 = {
            id: self.userId(),
            Email: self.model().email(),
            Password: self.password(),
            }
        console.log(self.model().email());

        if (self.model().email() != null) {
            $.ajax({
                type: 'PUT',
                url: '/login/updateEmail/',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(data1)
            }).done(function () {
                console.log("done")
            }).fail(function () { });
        }
            //if (self.model().username() != null || self.model().firstname() != null || self.model().lastname() != null || self.model().phoneNumber() != null) {
            //    $.ajax({
            //        type: 'PUT',
            //        url: '/api/user/' + self.userId(),
            //        contentType: 'application/json; charset=utf-8',
            //        data: JSON.stringify(data1)
            //    }).done(function (result) {
            //        //console.log("Done !")
            //        document.location = '/admin/user/index';
            //    }).fail(self.showError);
            //}
            //if ((self.password() != null && self.confirmPassword()==self.password()) || self.model().email!=null ) {
            //    $.ajax({
            //        type: 'PUT',
            //        url: '/api/login/editEmailAndPassword/'+self.userId(),
            //        contentType: 'application/json; charset=utf-8',
            //        data: JSON.stringify(data2)
            //    }).done(function (result) {
            //        //console.log("Done !")
            //        document.location = '/admin/user/index';
            //    }).fail(self.showError);
            //}
      
        
    }

    

    self.cancel = function () {
        document.location = '/Admin/User/Index';
    }


    //console.log(self.chooseimageclicked());
    //$("#file").on('change', function () {
    //    self.chooseimageclicked(1);
    //    console.log(self.chooseimageclicked());
    //});


}

//function userNewViewModel() {
//    var self = this;

//    ko.fileBindings.defaultOptions.buttonText = "Choose Image";



//    self.model = ko.validatedObservable({
//        username: ko.observable(),//.extend({ required: true, maxLength: 100 }),
//        firstname: ko.observable(),//.extend({ required: true, maxLength: 100 }),
//        lastname: ko.observable(),//.extend({ required: true, maxLength: 100 }),
//        //userName: ko.observable().extend({ required: true, maxLength: 100 }),
//        //email: ko.observable().extend({ required: true, pattern: '^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$' }),
//        phonenumber: ko.observable(),//.extend({ required: true, pattern: '^01[0-2]{1}[0-9]{8}' }),
//        //credit: ko.observable().extend({ required: true, pattern: '^[+-]?[0-9]{1,3}(?:[0-9]*(?:[.,][0-9]{2})?|(?:,[0-9]{3})*(?:\.[0-9]{2})?|(?:\.[0-9]{3})*(?:,[0-9]{2})?)$' }),
//        //limitedcredit: ko.observable().extend({ required: true, pattern: '^[+-]?[0-9]{1,3}(?:[0-9]*(?:[.,][0-9]{2})?|(?:,[0-9]{3})*(?:\.[0-9]{2})?|(?:\.[0-9]{3})*(?:,[0-9]{2})?)$' }),
//        //password: ko.observable(),
//        //passwordconfirm: ko.observable(),
//        //roles: ko.observableArray(["Admin", "Employee", "Customer"]),
//        //selectedRole: ko.observable()
//        //imageurl: ko.observable()
//    });


//    //self.fileData = ko.observable({
//    //    // file: ko.observable(), // will be filled with a File object
//    //    // Read the files (all are optional, e.g: if you're certain that it is a text file, use only text:
//    //    // binaryString: ko.observable(), // FileReader.readAsBinaryString(Blob|File) - The result property will contain the file/blob's data as a binary string. Every byte is represented by an integer in the range [0..255].
//    //    //text: ko.observable(), // FileReader.readAsText(Blob|File, opt_encoding) - The result property will contain the file/blob's data as a text string. By default the string is decoded as 'UTF-8'. Use the optional encoding parameter can specify a different format.
//    //    //dataURL: ko.observable(), // FileReader.readAsDataURL(Blob|File) - The result property will contain the file/blob's data encoded as a data URL.
//    //    //arrayBuffer: ko.observable(), // FileReader.readAsArrayBuffer(Blob|File) - The result property will contain the file/blob's data as an ArrayBuffer object.

//    //    // a special observable (optional)
//    //    base64String: ko.observable(), // just the base64 string, without mime type or anything else

//    //    // you can have observable arrays for each of the properties above, useful in multiple file upload selection (see Multiple file Uploads section below)
//    //    // in the format of xxxArray: ko.observableArray(),
//    //    /* e.g: */ //fileArray: ko.observableArray(), base64StringArray: ko.observableArray(),
//    //});


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

//    self.save = function () {


//        //if (self.model.isValid()) {
//        //    var data = {
//        //        //id: self.customerId(),
//        //        //user: {
//        //            userName: self.model().username(),
//        //            firstName: self.model().firstname(),
//        //            lastName: self.model().lastname(),
//        //            email: self.model().email(),                    
//        //            password: self.model().password(),
//        //            passwordconfirm: self.model().passwordconfirm(),
//        //            phoneNumber: self.model().phonenumber(),
//        //            //imageData: self.fileData().base64String()
                
//        //        //credit: self.model().credit(),
//        //        //limitedcredit: self.model().limitedcredit()
//        //    }
//        //    console.log(data);
//        console.log(1);
//            $.ajax({
//                type: 'Post',
//                url: 'localhost:39654/Login/Register',
//                contentType: 'application/json; charset=utf-8',
//                data: JSON.stringify(data)
//            }).done(function (result) {
//                console.log(result);
//                //document.location = '/admin/user/index';
//            }).fail(self.showError);
//        //} else {
//            console.log(1);

//        //    alertify.error("Error,Some fields are invalid !");

//        //}

//    }

//    self.cancel = function () {
//        document.location = '/Admin/User/Index';
//    }

//}