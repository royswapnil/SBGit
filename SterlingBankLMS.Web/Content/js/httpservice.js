angular.module('requestSvc', []).service('httpRequestSvc', ['$http', '$q', function (http, q) {
    this.get = function (url, data) {
        return q(function (resolve, reject) {
            http.get(url, { params: data}).then(function (response) {
                resolve(response.data);
            }, function (err) {
                reject(err);
            });
        });
    };

    this.post = function (url, data) {
        return q(function (resolve, reject) {
            http.post(url, data).then(function (response) {
                resolve(response.data);
            }, function (err) {
                reject(err);
            });
        });
    };
}]);