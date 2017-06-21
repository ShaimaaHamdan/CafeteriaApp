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
            console.log(data);
            self.categories(data.categories);
        }).fail(self.showError);
    };

    self.getAllCategories();


    $('#myModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)[0];
        self.categoryId(button.attributes["categoryid"].value);

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
    self.cafeteriaId = ko.observable();
    self.name = ko.observable();
    self.imageurl = ko.observable();
    
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

    self.getCategory = function () {
        $.ajax({
            type: 'Get',
            url: '/api/Category/' + self.categoryId(),
            contentType: 'application/json; charset=utf-8',
        }).done(function (data) {
            self.cafeteriaId(data.category.CafeteriaId);
            self.model().name(data.category.Name);
            self.model().imageurl(data.category.ImageUrl);
            self.fileData().dataURL('data:image/gif;base64,' + data.category.ImageData);
            self.fileData().base64String(data.category.ImageData);
        }).fail(self.showError);
    };

    self.getCategory();

    self.save = function () {
        if (self.model.isValid()) {
            var data = {

                name: self.model().name(),
                id: self.categoryId(),
                imageData: self.fileData().base64String()

            }
            $.ajax({
                type: 'PUT',
                url: '/api/Category/' + self.categoryId(),
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(data)
            }).done(function (result) {
                console.log(result)
                document.location = '/Admin/Cafeteria/edit/' + self.cafeteriaId();
            }).fail(self.showError);
        } else {
            alertify.error("Error,Some fileds are invalid !");

        }
    }

        self.cancel = function () {
            document.location = '/Admin/Cafeteria/edit/' + self.cafeteriaId();
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
            self.name(button.attributes["name"].value)
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
                alertify.success(self.name() + " menuitem is deleted ");
                self.getMenuItemByCategoryId();
            }).fail(self.showError);

        }


    }

function CategoryNewViewModel(cafetriaId) {
        var self = this;
        ko.fileBindings.defaultOptions.buttonText = "Choose Image";

        self.cafeteriaId = ko.observable(cafetriaId);
        self.model = ko.validatedObservable({
            name: ko.observable().extend({ required: true, maxLength: 100 })
        });
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

                    name: self.model().name(),
                    cafeteriaId: self.cafeteriaId(),
                    imageData: self.fileData().base64String()

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
            } else {
                alertify.error("Error,Some fileds are invalid !");
            }

        }

        self.cancel = function () {
            document.location = '/Admin/Cafeteria/edit/' + self.cafeteriaId();
        }

    }

