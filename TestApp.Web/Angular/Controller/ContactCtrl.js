﻿(function () {
    'use strict';

    angular
        .module('app')
        .controller('ContactCtrl', ContactCtrl);

    ContactCtrl.$inject = ['$scope', 'ContactService'];

    function ContactCtrl($scope, ContactService) {
        $scope.title = 'ContactCtrl';

        $scope.DefaultContactDetails = {
            ContactId: 0,
            FirstName: "",
            LastName: "",
            EmailId: "",
            MobileNo: "",
            CountryId: null,
            StateId: null,
            CityId: null,
            Address: null,
            IsActive: true,
            IsFavourite: false
        };

        $scope.Param = {
            PageIndex: 1,
            PageSize: 5,
            SearchString: '',
            OrderByField: 'FirstName',
            OrderBy: 'Asc'
        }

        $scope.maxsize = 10;
        $scope.totalcount = 0;
        $scope.reverseSort = false;
        //$scope.regEx = "/^[0-9]*$/";

        //Contact Details
        $scope.ContactDetails = angular.copy($scope.DefaultContactDetails);

        $scope.regEx = new RegExp("^(()?\\d{3}())?(-|\\s)?\\d{3}(-|\\s)?\\d{4}$");
        //Save Contact Detials
        $scope.SaveContactDetails = function () {
            if (!$scope.regEx.test($scope.ContactDetails.MobileNo)) {
                toastr.error("Invalid mobile number");
                return;
            }
            ContactService.SaveContactDetails($scope.ContactDetails).then(function (result) {
                debugger;
                if (angular.isUndefined(result) || result == null)
                    toastr.error("Something Went Wrong.");

                if (result.MessageType == MessageTypes.Success) {
                    toastr.success(result.Message);
                    $scope.ClearForm();
                }
                else if (result.MessageType == MessageTypes.Warning) {
                    toastr.warning(result.Message);
                }
                else {
                    toastr.error(result.Message);
                }
            });
        }

        $scope.GetContacts = function () {

            ContactService.GetContacts($scope.Param).then(function (result) {
                debugger;
                if (angular.isUndefined(result) || result == null) {
                    toastr.error("Something Went Wrong.");
                }
                else if (angular.isUndefined(result.data) || result.data.MessageType != MessageTypes.Success) {
                    toastr.error(result.data.Message);
                }
                else {
                    $scope.ContactData = result.data.Data;
                    $scope.totalcount = result.data.TotalCount;
                    $scope.numPages = Math.ceil($scope.totalcount / $scope.Param.PageSize);
                }
            });
        }

        $scope.GetContactDetails = function (id) {

            ContactService.GetContactDetails(id).then(function (result) {
                debugger;
                if (angular.isUndefined(result) || result == null) {
                    toastr.error("Something Went Wrong.");
                }
                else if (result.data.MessageType == MessageTypes.Warning) {
                    toastr.warning(result.data.Message);
                }
                else if (result.data.MessageType == MessageTypes.Fail) {
                    toastr.error(result.data.Message);
                }
                else {
                    var contactDetail = result.data.Data;
                    $scope.ContactDetails = angular.copy(contactDetail);
                }
            });
        }

        $scope.DeleteContactDetails = function (id) {
            debugger;
            if (!(id > 0)) {
                toastr.error("No Contact Selected");
            }

            ContactService.DeleteContactDetails(id).then(function (result) {
                debugger;
                if (angular.isUndefined(result) || result == null) {
                    toastr.error("Something Went Wrong");
                }

                if (result.data.MessageType == MessageTypes.Success) {
                    toastr.success(result.data.Message);
                    $scope.GetContacts();
                }
                else if (result.data.MessageType == MessageTypes.Warning) {
                    toastr.warning(result.data.Message);
                }
                else {
                    toastr.error(result.data.Message);
                }
            });
        }

        $scope.FavouriteContact = function (id, isFavourite) {
            debugger;
            if (!(id > 0)) {
                toastr.error("No Contact Selected");
            }

            ContactService.FavouriteContact(id, isFavourite).then(function (result) {
                debugger;
                if (angular.isUndefined(result) || result == null) {
                    toastr.error("Something Went Wrong");
                }

                if (result.data.MessageType == MessageTypes.Success) {
                    toastr.success(result.data.Message);
                    $scope.GetContacts();
                }
                else if (result.data.MessageType == MessageTypes.Warning) {
                    toastr.warning(result.data.Message);
                }
                else {
                    toastr.error(result.data.Message);
                }
            });
        }

        $scope.ActiveInactiveContact = function (id, isActive) {
            debugger;
            if (!(id > 0)) {
                toastr.error("No Contact Selected");
            }

            ContactService.ActiveInactiveContact(id, isActive).then(function (result) {
                debugger;
                if (angular.isUndefined(result) || result == null) {
                    toastr.error("Something Went Wrong");
                }

                if (result.data.MessageType == MessageTypes.Success) {
                    toastr.success(result.data.Message);
                    $scope.GetContacts();
                }
                else if (result.data.MessageType == MessageTypes.Warning) {
                    toastr.warning(result.data.Message);
                }
                else {
                    toastr.error(result.data.Message);
                }
            });
        }

        //Clear Contact Form
        $scope.ClearForm = function () {
            $scope.ContactDetails = angular.copy($scope.DefaultContactDetails);
            $scope.GetContacts();
        }

        $scope.pagechanged = function () {
            $scope.GetContacts();
        }

        $scope.changePageSize = function () {
            $scope.Param.PageIndex = 1;
            $scope.GetContacts();
        }

        $scope.EditContact = function (id) {
            $scope.GetContactDetails(id);
        }

        $scope.SortBy = function (sortByField) {
            $scope.Param.OrderByField = sortByField;
            $scope.reverseSort = !$scope.reverseSort;
            $scope.Param.OrderBy = ($scope.reverseSort) ? 'ASC' : 'DESC' ;
            $scope.pagechanged();
        }

        //Init
        $scope.Init = function () {
            $scope.GetContacts();
        }
    }
})();