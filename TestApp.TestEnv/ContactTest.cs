using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.API.Service;
using Moq;
using TestApp.API.Models;
using System.Data.Entity;
using TestApp.API.Controllers;
using TestApp.API.Common;

namespace TestApp.TestEnv
{
    [TestFixture]
    public class ContactTest
    {
        public IContactService contactService;
        public ContactTest()
        {
            contactService = new ContactService();
        }

        [Test]
        public void TestSaveContact()
        {
            var mockSet = new Mock<DbSet<Contacts>>();

            var mockContext = new Mock<AppDBEntities>();
            mockContext.Setup(m => m.Contacts).Returns(mockSet.Object);

            var service = new Mock<IContactService>();
            Contacts con = new Contacts() { FirstName = "Ankit", LastName = "Patel", MobileNo = "4545454545", EmailId = "df.df@f.com", EntryDate = DateTime.Now, IsActive = true, IsFavourite = true };
            service.Setup(m => m.AddUpdateContact(con)).Returns(1);

            var result = service.Object.AddUpdateContact(con);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestGetContactDetail()
        {
            int id = 1;
            var service = new Mock<IContactService>();
            Contacts con = new Contacts() { FirstName = "Ankit", LastName = "Patel", MobileNo = "4545454545", EmailId = "df.df@f.com", EntryDate = DateTime.Now, IsActive = true, IsFavourite = true };
            service.Setup(m => m.GetContactDetails(id)).Returns(con);

            var result = service.Object.GetContactDetails(id);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestIsActiveDeactiveContact()
        {
            int id = 1;
            var service = new Mock<IContactService>();
            bool op = true;
            service.Setup(m => m.ActiveInactiveContact(id, true)).Returns(op);

            var result = service.Object.ActiveInactiveContact(id, true);
            Assert.IsTrue(result);
        }

        [Test]
        public void TestIsFavouriteContact()
        {
            int id = 1;
            var service = new Mock<IContactService>();
            bool op = true;
            service.Setup(m => m.FavouriteContact(id, true)).Returns(op);

            var result = service.Object.FavouriteContact(id, true);
            Assert.IsTrue(result);
        }
    }
}
