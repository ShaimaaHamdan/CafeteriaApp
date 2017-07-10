function favoriteViewModel(userId) {

    var self = this;
    self.userId = ko.observable(userId);
    self.favoriteItems = ko.observableArray();
    self.favoriteItemsLength = ko.observable();
    self.showfavorite = ko.observable();
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
    self.getFavorite = function () {
        if (self.userId() != undefined && self.userId() != '') {
            $.ajax({
                type: 'Get',
                url: '/api/FavoriteItem/getByUserId/' + self.userId(),
                contentType: 'application/json; charset=utf-8'
            }).done(function (data) {
                self.favoriteItems(data.favoriteitems);
                console.log(self.favoriteItems());
                self.favoriteItemsLength(self.favoriteItems().length);
                if (self.favoriteItemsLength() != 0) {
                    self.showfavorite(1);
                }
            }).fail(self.showError);
        } else {
            document.location = '/login/login';
        }
    }

   
    self.getFavorite();


    
    self.deletefavorite = function (favoriteitem) {
        $.ajax({
            type: 'Delete',
            url: '/api/FavoriteItem/' + favoriteitem.Id,
            contentType: 'application/json; charset=utf-8',
            data: { id: favoriteitem.Id }
        }).done(function () {
            self.favoriteItemsLength(self.favoriteItemsLength() - 1);
            self.getFavorite();
            if (self.favoriteItemsLength() == 0) {
                self.showfavorite(0);
            }
        }).fail(self.showError);
    }

}