function CafeteriaViewModel() {
    var self = this;
    self.cafeterias = ko.observableArray();
    self.cafeteriaId = ko.observable();
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

    self.getAllCafeterias = function () {
        $.ajax({
            type: 'Get',
            url: '/api/Cafeteria',
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            console.log(data)
            self.cafeterias(data.cafeterias)
        }).fail(self.showError);
    };

    self.getAllCafeterias();


    $('#myModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)[0];
        self.cafeteriaId(button.attributes["cafeteriaid"].value)
        self.name(button.attributes["name"].value)
        console.log(self.name());
        self.getCategoryByCafeteriaId();
    });

    self.getCategoryByCafeteriaId = function () {
        console.log(self.cafeteriaId())
        $.ajax({
            type: 'Get',
            url: '/api/Category/GetByCafetria/' + self.cafeteriaId(),
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            console.log(data);
            self.categories(data.categories);
        }).fail(self.showError);
    };



    self.deleteCafeteria = function () {

        if (self.categories().length== 0) {
            console.log(self.categories().length)
            console.log("id=" + self.cafeteriaId());
            $.ajax({
                type: 'Delete',
                url: '/api/Cafeteria/' + self.cafeteriaId(),
                contentType: 'application/json; charset=utf-8',
                data: { id: self.cafeteriaId() }
            }).done(function (data) {
                console.log(data)
                $('#myModal').modal('hide')
                alertify.success(self.name() + " cafeteria is deleted ");
                self.getAllCafeterias();
            }).fail(self.showError);
        } else {
            alertify.error("Error, You must delete categories of " + self.name() + " cafeteria first!");

        }

    }
}


function CafeteriaEditViewModel(id) {

    var self = this;
    
    self.cafeteriaId = ko.observable(id);
    self.categoryId = ko.observable();
    self.categories = ko.observableArray();
    self.menuItems = ko.observableArray();
    self.name = ko.observable();
    self.imageurl = ko.observable();
    self.chooseimageclicked = ko.observable(0);
    ko.fileBindings.defaultOptions.buttonText = "Choose Image";

    self.fileData = ko.observable({
       // file: ko.observable(), // will be filled with a File object
        // Read the files (all are optional, e.g: if you're certain that it is a text file, use only text:
       // binaryString: ko.observable(), // FileReader.readAsBinaryString(Blob|File) - The result property will contain the file/blob's data as a binary string. Every byte is represented by an integer in the range [0..255].
        //text: ko.observable(), // FileReader.readAsText(Blob|File, opt_encoding) - The result property will contain the file/blob's data as a text string. By default the string is decoded as 'UTF-8'. Use the optional encoding parameter can specify a different format.
        dataURL: ko.observable(), // FileReader.readAsDataURL(Blob|File) - The result property will contain the file/blob's data encoded as a data URL.
        //arrayBuffer: ko.observable(), // FileReader.readAsArrayBuffer(Blob|File) - The result property will contain the file/blob's data as an ArrayBuffer object.

        // a special observable (optional)
        base64String: ko.observable(), // just the base64 string, without mime type or anything else

        // you can have observable arrays for each of the properties above, useful in multiple file upload selection (see Multiple file Uploads section below)
        // in the format of xxxArray: ko.observableArray(),
        /* e.g: */ //fileArray: ko.observableArray(), base64StringArray: ko.observableArray(),
    });

    self.model = ko.validatedObservable({
        name: ko.observable().extend({ required: true, maxLength: 100 }),
        imageurl: ko.observable()
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


    self.getCafeteria = function () {
        $.ajax({
            type: 'Get',
            url: '/api/Cafeteria/' + self.cafeteriaId(),
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            console.log(data.cafeteria);
            self.model().name(data.cafeteria.Name);
            self.model().imageurl(data.cafeteria.ImageUrl);
            console.log(self.model().imageurl());
            self.fileData().dataURL('data:image/gif;base64,' + data.cafeteria.ImageData);
            //self.fileData().base64String(data.cafeteria.ImageData);
        }).fail(self.showError);
    };

    self.getCafeteria();


    self.save = function () {

        if (self.model.isValid()) {
            var data = {
                name: self.model().name(),
                id: self.cafeteriaId(),
                imageData: self.fileData().base64String()
            }

            $.ajax({
                type: 'PUT',
                url: '/api/Cafeteria/' + self.cafeteriaId(),
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(data)
            }).done(function (result) {
                document.location = '/admin/cafeteria/index';                
            }).fail(self.showError);
        } else {
            alertify.error("Error,Some fields are invalid !");
        }

    }

    self.cancel = function () {
        document.location = '/Admin/Cafeteria/Index';
    }


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


    $('#myModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)[0];
        self.categoryId(button.attributes["categoryid"].value);
        self.name(button.attributes["name"].value);
        self.getMenuItemByCategoryId();
    });

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

    self.deleteCategory = function () {


        if (self.menuItems().length == 0) {
            console.log("id=" + self.categoryId());
            $.ajax({
                type: 'Delete',
                url: '/api/Category/' + self.categoryId(),
                contentType: 'application/json; charset=utf-8',
                data: { id: self.categoryId() }
            }).done(function (data) {
                console.log(data);
                $('#myModal').modal('hide');
                alertify.success(self.name() + " category is deleted ");
                self.getCategoryByCafeteriaId();
            }).fail(self.showError);
        } else {
            alertify.error("Error, You must delete menuitems of " + self.name() + " category first!");
        }
    }
    console.log(self.chooseimageclicked());
    $("#file").on('change' , function() {
        self.chooseimageclicked(1);
        console.log(self.chooseimageclicked());
    });
    
    
}

function CafeteriaNewViewModel() {
    var self = this;

    ko.fileBindings.defaultOptions.buttonText = "Choose Image";



    self.model = ko.validatedObservable({
        name: ko.observable().extend({ required: true, maxLength: 100 })
    });


    self.fileData = ko.observable({
       // file: ko.observable(), // will be filled with a File object
        // Read the files (all are optional, e.g: if you're certain that it is a text file, use only text:
       // binaryString: ko.observable(), // FileReader.readAsBinaryString(Blob|File) - The result property will contain the file/blob's data as a binary string. Every byte is represented by an integer in the range [0..255].
        //text: ko.observable(), // FileReader.readAsText(Blob|File, opt_encoding) - The result property will contain the file/blob's data as a text string. By default the string is decoded as 'UTF-8'. Use the optional encoding parameter can specify a different format.
        //dataURL: ko.observable(), // FileReader.readAsDataURL(Blob|File) - The result property will contain the file/blob's data encoded as a data URL.
        //arrayBuffer: ko.observable(), // FileReader.readAsArrayBuffer(Blob|File) - The result property will contain the file/blob's data as an ArrayBuffer object.

        // a special observable (optional)
        base64String: ko.observable(), // just the base64 string, without mime type or anything else

        // you can have observable arrays for each of the properties above, useful in multiple file upload selection (see Multiple file Uploads section below)
        // in the format of xxxArray: ko.observableArray(),
        /* e.g: */ //fileArray: ko.observableArray(), base64StringArray: ko.observableArray(),
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
                imageData: self.fileData().base64String()
            }
            console.log(data);
            $.ajax({
                type: 'Post',
                url: '/api/cafeteria',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(data)
            }).done(function (result) {
                console.log(result);
                document.location = '/admin/cafeteria/index';
            }).fail(self.showError);
        } else {

            alertify.error("Error,Some fields are invalid !");

        }

    }

    self.cancel = function () {
        document.location = '/Admin/Cafeteria/Index';
    }

}