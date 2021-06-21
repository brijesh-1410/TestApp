using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestApp.API.Common;
using TestApp.API.Service;
using TestApp.API.Models;
using System.Web.Http.Cors;
using System.Web.Http;
using Newtonsoft.Json;

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
                            existMessage = "Contact Already Exist";
                            break;
                        case -2:
                            existMessage = "EmailId Already Exist";
                            break;
                        case -3:
                            existMessage = "MobileNo Already Exist";
                            break;
                    }

                    if (existMessage != string.Empty)
                    {
                        return Json(new Responce() { Message = existMessage, Data = null, MessageType = MessageType.Warning });
                    }
                    else if (contactId != null)
                    {
                        return Json(new Responce() { Message = "Contact Details saved successfully", Data = contactId, MessageType = MessageType.Success });
                    }
                    else
                    {
                        return Json(new Responce() { Message = "Issue while Adding Contact Details", Data = null, MessageType = MessageType.Fail });
                    }
                }
                catch (Exception)
                {
                    return Json(new Responce() { Message = "Issue while Adding Contact Details", Data = null, MessageType = MessageType.Fail });
                }
            }
            else
            {
                return Json(new Responce() { Message = "Invalid Contact Details", Data = null, MessageType = MessageType.Fail });
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
                    return Json(new Responce() { Message = "Contact " + (IsActive ? "Activated": "De-activated")  + " Successfully.", Data = success, MessageType = MessageType.Success }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new Responce() { Message = "Issue while Activating/De-Activating Contact.", Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new Responce() { Message = "Invalid Contact for Activation/De-activation.", Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
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
                    return Json(new Responce() { Message = "Contact details fetched successfully.", Data = contact, MessageType = MessageType.Success }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new Responce() { Message = "Issue while fetching contact details.", Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new Responce() { Message = "Invalid Contact selected.", Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
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
                    return Json(new Responce() { Message = "Contact deleted successfully.", Data = contact, MessageType = MessageType.Success }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new Responce() { Message = "Issue while deleting contact details.", Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new Responce() { Message = "Invalid Contact selected.", Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
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
                    return Json(new Responce() { Message = "Contact " + (IsFavourite ? "Added In" : "Removed From") + "Favourite List Successfully.", Data = success, MessageType = MessageType.Success },JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new Responce() { Message = "Issue while taking action on Contact.", Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new Responce() { Message = "Invalid Contact for Action.", Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
            }
        }
    }

    public class ContactInput
    {
        public int contactId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobileNo { get; set; }
        public string emailId { get; set; }
        public Nullable<System.DateTime> entryDate { get; set; }
        public Nullable<int> countryId { get; set; }
        public Nullable<int> stateId { get; set; }
        public Nullable<int> cityId { get; set; }
        public string address { get; set; }
        public bool isActive { get; set; }
        public Nullable<bool> IsFavourite { get; set; }
    }

}