using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestApp.API.Service;

namespace TestApp.Test
{
    [TestFixture]
    public class ContactTest
    {
        public IContactService contactService;
        public ContactTest() {
            contactService = new ContactService();
        }

        [Test]
        public void TestGetContacts()
        {

            var result = contactService.GetContacts(new API.Common.GridParam
            {
               pageIndex = 1,
               pageSize = 5,
               orderBy = "Asc",
               orderByField = "FirstName",
               searchString = ""
            });
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestGetContactDetail()
        {
            int id = 1;
            var result = contactService.GetContactDetails(id);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestSaveContact()
        {
            var result = contactService.AddUpdateContact(new API.Models.Contacts
            {
                FirstName = "Ajay",
                LastName = "Nagar",
                EmailId = "Ajay.nagar@g.com",
                MobileNo = "9856453423",
                IsActive = true,
                IsFavourite = true,
                EntryDate = DateTime.UtcNow
            });
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestIsActiveDeactiveContact()
        {
            int id = 1;
            bool isActive = true;
            var result = contactService.ActiveInactiveContact(id,isActive);
            Assert.IsTrue(result);
        }

        [Test]
        public void TestIsFavouriteContact()
        {
            int id = 1;
            bool isFavourite = true;
            var result = contactService.FavouriteContact(id, isFavourite);
            Assert.IsTrue(result);
        }
    }
}
