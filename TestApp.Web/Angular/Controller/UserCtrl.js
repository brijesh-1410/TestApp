(function () {
    'use strict';

    angular
        .module('app')
        .controller('UserCtrl', UserCtrl);

    UserCtrl.$inject = ['$scope', 'UserService','$http'];

    function UserCtrl($scope, UserService, $http) {
        $scope.title = 'UserCtrl';

        $scope.DefaultUserDetails = {
            UserId: 0,
            FirstName: "",
            LastName: "",
            UserName: "",
            EmailId: "",
            Password: "",
            MobileNo: "",
            EntryDate: null,
            CountryId: null,
            StateId: null,
            CityId: null,
            IsActive: true
        };

        //User Details
        $scope.UserDetails = angular.copy($scope.DefaultUserDetails);

        //Save User Detials
        $scope.SaveUserDetails = function () {
            console.log($scope.UserDetails);
            UserService.SaveUserDetails($scope.UserDetails).then(function (result) {
                debugger;
                if (angular.isUndefined(result) || result == null)
                    toastr.error("Something Went Wrong.");

                if (result.MessageType == MessageTypes.Success)
                    toastr.success(result.Message);
                else if (result.MessageType == MessageTypes.Warning)
                    toastr.warning(result.Message);
                else
                    toastr.error(result.Message);
            });
        }

        $scope.GetUsers = function () {
            UserService.GetUsers().then(function (result) {
                if (angular.isUndefined(result) || result == null)
                    toastr.error("Something Went Wrong.");
                //debugger;
                //alert(result);
            });
        }

        //Clear User Form
        $scope.ClearForm = function () {
            $scope.UserDetails = angular.copy($scope.DefaultUserDetails);
        }

        //Init
        $scope.Init = function () {
            toastr.success("hi");
            $scope.GetUsers();  
        }
    }
})();
