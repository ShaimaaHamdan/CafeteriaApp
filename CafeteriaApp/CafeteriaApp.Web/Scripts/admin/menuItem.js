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
            console.log(data)
            self.menuItems(data)
        }).fail(self.showError);
    };

    self.getAllMenuItems();


    $('#myModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)[0];
        self.menuItemId(button.attributes["menuItemid"].value)
    });


    self.deleteMenuItem = function()
    {
        console.log("id=" + self.menuItemId());
        $.ajax({
            type: 'Delete',
            url: '/api/MenuItem/'+self.menuItemId(),
            contentType: 'application/json; charset=utf-8',
            //data:{id:self.menuItemId()}
        }).done(function (data) {
            console.log(data)
            $('#myModal').modal('hide')
            self.getAllMenuItems();
        }).fail(self.showError);
   
    }

}


