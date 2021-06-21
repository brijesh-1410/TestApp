using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApp.API.Common;
using TestApp.API.Models;

namespace TestApp.API.Service
{
    public interface IContactService
    {
        List<Models.USP_GetContacts_Result> GetContacts(GridParam param);
        int? AddUpdateContact(Contacts contact);

        bool ActiveInactiveContact(int contactId, bool isActive);

        object GetContactDetails(int contactId);

        int? DeleteContactDetails(int contactId);
        bool FavouriteContact(int contactId, bool isFavourite);

        int? GetTotalContacts();
    }
    public class ContactService : IContactService
    {
        AppDBEntities _entities = new AppDBEntities();

        public List<Models.USP_GetContacts_Result> GetContacts(GridParam param)
        {
            try
            {
                //List<Models.Contacts> contacts = _entities.Contacts.ToList();
                List<USP_GetContacts_Result> contacts = _entities.USP_GetContacts(
                    param.pageIndex
                    ,param.pageSize
                    ,param.searchString
                    ,param.orderByField
                    ,param.orderBy).ToList();
                return contacts;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public int? AddUpdateContact(Contacts contact)
        {
            try
            {
                int contactId = 0;

                //check Name already used
                if (_entities.Contacts.Where(x => x.FirstName == contact.FirstName && x.LastName == contact.LastName && x.ContactId != contact.ContactId).Any())
                {
                    contactId = -1;
                    return contactId;
                }

                //check EmailId already used
                if (_entities.Contacts.Where(x => x.EmailId == contact.EmailId && x.ContactId != contact.ContactId).Any())
                {
                    contactId = -2;
                    return contactId;
                }

                //check MobileNo already used
                if (_entities.Contacts.Where(x => x.MobileNo == contact.MobileNo && x.ContactId != contact.ContactId).Any())
                {
                    contactId = -3;
                    return contactId;
                }

                if (contact.ContactId == 0)
                {
                    contact.EntryDate = DateTime.Now;
                    _entities.Contacts.Add(contact);
                    contactId = contact.ContactId;
                }
                else
                {
                    Contacts existingContact = _entities.Contacts.Where(x => x.ContactId == contact.ContactId).FirstOrDefault();
                    if (existingContact != null)
                    {
                        existingContact.FirstName = contact.FirstName;
                        existingContact.LastName = contact.LastName;
                        existingContact.EmailId = contact.EmailId;
                        existingContact.MobileNo = contact.MobileNo;
                        existingContact.CountryId = contact.CountryId;
                        existingContact.StateId = contact.StateId;
                        existingContact.CityId = contact.CityId;
                        existingContact.Address = contact.Address;
                        existingContact.IsActive = contact.IsActive;
                        existingContact.IsFavourite = contact.IsFavourite;
                        existingContact.UpdateDate = contact.UpdateDate;
                    }
                    contactId = existingContact.ContactId;
                }
                _entities.SaveChanges();
                return contactId;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool ActiveInactiveContact(int contactId, bool isActive)
        {
            try
            {
                Contacts contact = _entities.Contacts.Where(x => x.ContactId == contactId).FirstOrDefault();
                if (contact != null)
                {
                    contact.IsActive = isActive;
                    _entities.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public object GetContactDetails(int contactId)
        {
            try
            {
               return  _entities.Contacts.Where(x => x.ContactId == contactId).Select(x=> new {
                    x.ContactId,
                    x.FirstName,
                    x.LastName,
                    x.EmailId,
                    x.MobileNo,
                    x.CountryId,
                    x.StateId,
                    x.CityId,
                    x.Address,
                    x.IsActive,
                    x.IsFavourite
                }
                ).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int? DeleteContactDetails(int contactId)
        {
            try
            {
                Contacts deletedContact = _entities.Contacts.Where(x => x.ContactId == contactId).FirstOrDefault();
                if (deletedContact != null)
                {
                    _entities.Contacts.Remove(deletedContact);
                    _entities.SaveChanges();
                    return contactId;
                }
                else {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool FavouriteContact(int contactId, bool isFavourite)
        {
            try
            {
                Contacts contact = _entities.Contacts.Where(x => x.ContactId == contactId).FirstOrDefault();
                if (contact != null)
                {
                    contact.IsFavourite = isFavourite;
                    _entities.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int? GetTotalContacts() {
            try
            {
                return _entities.Contacts.Count();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}