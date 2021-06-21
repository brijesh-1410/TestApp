//import { param } from "jquery";

(function () {
    'use strict';

    angular
        .module('app')
        .service('ContactService', ContactService);

    ContactService.$inject = ['$http'];

    function ContactService($http) {
        var service = {
            getData: getData,
            SaveContactDetails: SaveContactDetails,
            GetContacts: GetContacts,
            GetContactDetails: GetContactDetails,
            DeleteContactDetails: DeleteContactDetails,
            ActiveInactiveContact: ActiveInactiveContact,
            FavouriteContact: FavouriteContact
        };

        
        function GetContacts(params) {
            return $http({
                method: 'GET',
                url: APIUrl + 'Contact/GetContacts?inputParam=' + JSON.stringify(params)
            });
        }

        function SaveContactDetails(contactDetails) {
            return $.ajax({
                type: 'POST',
                url: APIUrl + 'Contact/AddUpdateContact',
                data: contactDetails
            });

            //return $http({
            //    method: 'POST',
            //    url: APIUrl + '/Contact/AddUpdateContact',
            //    data: { contact: JSON.stringify(contactDetails)}
            //});
        }


        function GetContactDetails(id) {
            return $http({
                method: 'GET',
                url: APIUrl + 'Contact/GetContactDetails?contactId=' + id
            });
        }

        function DeleteContactDetails(id) {
            return $http({
                method: 'GET',
                url: APIUrl + 'Contact/DeleteContactDetails?contactId=' + id
            });
        }

        function ActiveInactiveContact(id, isActive) {
            return $http({
                method: 'GET',
                url: APIUrl + 'Contact/ActiveInactiveContact?contactId=' + id + '&IsActive=' + isActive
            });
        }

        function FavouriteContact(id, isFavourite) {
            return $http({
                method: 'GET',
                url: APIUrl + 'Contact/FavouriteContact?contactId=' + id + '&IsFavourite=' + isFavourite
            });
        }


        return service;

        function getData() { }
    }
})();