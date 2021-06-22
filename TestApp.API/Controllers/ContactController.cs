using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestApp.Resource;
using TestApp.API.Common;
using TestApp.API.Service;
using TestApp.API.Models;
using System.Web.Http.Cors;
using System.Web.Http;
using Newtonsoft.Json;
using TestApp.API.Models.InputModels;

namespace TestApp.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ContactController : Controller
    {
        private readonly IContactService contactService;
        public ContactController()
        {
            this.contactService = new ContactService();
        }

        public ContactController(ContactService _contactService)
        {
            contactService = _contactService;
        }

        [System.Web.Http.HttpGet]
        public ActionResult GetContacts(string inputParam)
        {
            try
            {
                GridParam param = Newtonsoft.Json.JsonConvert.DeserializeObject<GridParam>(inputParam);
                List<Models.USP_GetContacts_Result> contacts = contactService.GetContacts(param);
                int? totalCount = contactService.GetTotalContacts();
                if (contacts != null && totalCount != null)
                {
                    return Json(new Responce() { Message = "Contact Details fetched successfully", Data = contacts, MessageType = MessageType.Success , TotalCount = (int)totalCount}, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new Responce() { Message = "Issue while fetching Contact Details", Data = null, MessageType = MessageType.Fail, TotalCount = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new Responce() { Message = "Issue while fetching Contact Details", Data = null, MessageType = MessageType.Fail, TotalCount = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Http.HttpPost]
        public ActionResult AddUpdateContact(ContactInput contact)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    string inputString = Newtonsoft.Json.JsonConvert.SerializeObject(contact);
                    Contacts contactData = Newtonsoft.Json.JsonConvert.DeserializeObject<Contacts>(inputString);

                    int? contactId = contactService.AddUpdateContact(contactData);

                    string existMessage = string.Empty;

                    switch (contactId)
                    {
                        case -1:
                            existMessage = AppResources.ContactExistMessage;
                            break;
                        case -2:
                            existMessage = AppResources.EmailExistMessage;
                            break;
                        case -3:
                            existMessage = AppResources.MobileNoExistMessage;
                            break;
                    }

                    if (existMessage != string.Empty)
                    {
                        return Json(new Responce() { Message = existMessage, Data = null, MessageType = MessageType.Warning });
                    }
                    else if (contactId != null)
                    {
                        return Json(new Responce() { Message = AppResources.ContactSaveSuccessMessage, Data = contactId, MessageType = MessageType.Success });
                    }
                    else
                    {
                        return Json(new Responce() { Message = AppResources.ContactSaveIssueMessage, Data = null, MessageType = MessageType.Fail });
                    }
                }
                catch (Exception)
                {
                    return Json(new Responce() { Message = AppResources.ContactSaveIssueMessage, Data = null, MessageType = MessageType.Fail });
                }
            }
            else
            {
                return Json(new Responce() { Message = AppResources.InvalidContactDetailMessage, Data = null, MessageType = MessageType.Fail });
            }
        }

        [System.Web.Http.HttpGet]
        public ActionResult ActiveInactiveContact(int contactId, bool IsActive)
        {
            if (contactId > 0)
            {
                bool success = contactService.ActiveInactiveContact(contactId, IsActive);
                if (success)
                {
                    return Json(new Responce() { Message = string.Format("{0} {1} {2}",AppResources.Contact, (IsActive ? AppResources.Activated : AppResources.DeActivated), AppResources.Successfully), Data = success, MessageType = MessageType.Success }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new Responce() { Message = AppResources.ActivationIssueMessage, Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new Responce() { Message = AppResources.InvalidContactMessage, Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Http.HttpGet]
        public ActionResult GetContactDetails(int contactId)
        {
            if (contactId > 0)
            {
                var contact = contactService.GetContactDetails(contactId);
                if (contact != null)
                {
                    return Json(new Responce() { Message = AppResources.ContactListFetchSuccessMessage, Data = contact, MessageType = MessageType.Success }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new Responce() { Message = AppResources.ContactListFetchIssueMessage, Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new Responce() { Message = AppResources.InvalidContactSelected, Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Http.HttpGet]
        public ActionResult DeleteContactDetails(int contactId)
        {
            if (contactId > 0)
            {
                var contact = contactService.DeleteContactDetails(contactId);
                if (contact != null)
                {
                    return Json(new Responce() { Message = AppResources.ContactDeleteSuccessMessage, Data = contact, MessageType = MessageType.Success }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new Responce() { Message = AppResources.ContactDeleteFailMessage, Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new Responce() { Message = AppResources.InvalidContactSelected, Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Http.HttpGet]
        public ActionResult FavouriteContact(int contactId, bool IsFavourite)
        {
            if (contactId > 0)
            {
                bool success = contactService.FavouriteContact(contactId, IsFavourite);
                if (success)
                {
                    return Json(new Responce() { Message = string.Format("{0} {1} {2} {3}", AppResources.Contact, (IsFavourite ? AppResources.AddedIn : AppResources.RemovedFrom), AppResources.FavouriteList, AppResources.Successfully), Data = success, MessageType = MessageType.Success },JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new Responce() { Message = AppResources.ContactActionIssueMessage, Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new Responce() { Message = AppResources.ContactActionInvalidMessage, Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
            }
        }
    }

}