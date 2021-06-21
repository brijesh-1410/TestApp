(function () {
    'use strict';

    angular
        .module('app')
        .service('UserService', UserService);

    UserService.$inject = ['$http'];

    function UserService($http) {

        debugger
        var service = {
            getData: getData,
            SaveUserDetails: SaveUserDetails,
            GetUsers: GetUsers
        };
        var result;
        function SaveUserDetails(userDetails) {

            return $.ajax({
                type: 'POST',
                url: 'https://localhost:44377/User/AddUpdateUser',
                data: userDetails,
                //'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8',
                'Content-Type': 'application/json; charset=UTF-8',
                dataType: 'json',
                success: function (data) { console.log(data) }
            });
          
        }

        function GetUsers() {
            debugger
            return $http({
                 method: 'GET',
                 url: 'https://localhost:44377/User/GetUsers',
                 //headers: {
                 //    'Content-Type': 'application/json',
                 //    'Access-Control-Allow-Origin': '*'
                 //},
            });
        }
        

        return service;

        function getData() { }
    }
})();