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
            console.log(data.menuItems)
            self.menuItems(data.menuItems)
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
        }).done(function (result) {
            var data = result.menuitem;
            console.log(data);
            self.model().description(data.Description);
            self.model().name(data.Name);
            self.model().type(data.Type);
            self.model().price(data.Price);
            self.categoryId(data.CategoryId);
            self.fileData().dataURL('data:image/gif;base64,' + data.ImageData);
            self.fileData().base64String(data.ImageData);
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
                type: self.model().type(),
                imageData: self.fileData().base64String()
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
    ko.fileBindings.defaultOptions.buttonText = "Choose Image";

    self.model = ko.validatedObservable({
        name: ko.observable().extend({ required: true, maxLength: 100 }),
        price: ko.observable().extend({ required: true, pattern: '^[+-]?[0-9]{1,3}(?:[0-9]*(?:[.,][0-9]{2})?|(?:,[0-9]{3})*(?:\.[0-9]{2})?|(?:\.[0-9]{3})*(?:,[0-9]{2})?)$' }),
        type: ko.observable().extend({ required: true, maxLength: 100 }),
        description: ko.observable().extend({ required: true, maxLength: 500 })
    });
    self.categoryId = ko.observable(categoryId);
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
                categoryId: self.categoryId(),
                name: self.model().name(),
                price: self.model().price(),
                description: self.model().description(),
                type: self.model().type(),
                imageData: self.fileData().base64String()
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
        } else {
            alertify.error("Error,Some fileds are invalid !");
        }
    }

    self.cancel = function () {
        document.location = '/admin/category/edit/' + self.categoryId();
    }
}

