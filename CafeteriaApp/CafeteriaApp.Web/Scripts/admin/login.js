function AdminLoginViewModel() {
    var self = this;

    var tokenKey = 'accessToken';
    self.loginEmail = ko.observable();
    self.loginPassword = ko.observable();
    self.remmberMe = ko.observable();
    self.result = ko.observable();
    self.errors = ko.observableArray([]);


    self.login = function () {
        self.result('');
        self.errors.removeAll();

        var data = {
            Username: self.loginEmail(),
            Password: self.loginPassword(),
            RemmberMe: self.remmberMe()
        };

        $.ajax({
            type: 'POST',
            url: '/api/Account/Login',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            self.result("Done!");
            console.log(data);
            sessionStorage.setItem('userName', data.userName);
            sessionStorage.setItem('accessToken', data.access_token);
            sessionStorage.setItem('refreshToken', data.refresh_token);
            var tkn = sessionStorage.getItem(tokenKey);
            $("#tknKey").val(tkn);
            document.location = '/Admin/Cafeteria/Index';
        }).fail(function(error) {
            console.log(error);
        });
    }

    self.registerEmail = ko.observable();
    self.registerPassword = ko.observable();
    self.registerPassword2 = ko.observable();

    self.register = function () {
        self.result('');
        self.errors.removeAll();

        var data = {
            Email: self.registerEmail(),
            Password: self.registerPassword(),
            ConfirmPassword: self.registerPassword2()
        };

        $.ajax({
            type: 'POST',
            url: '/api/Account/Register',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            self.result("Done!");
        }).fail(function (error) {
            console.log(error);
        });
    }
}

