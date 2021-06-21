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
    //[EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController() {

            this.userService = new UserService();
        }

        [System.Web.Http.HttpGet]
        public ActionResult GetUsers()
        {
            try
            {
                List<Models.Users> users = userService.GetUsers();
                if (users != null)
                {
                    return Json(new Responce() { Message = "User Details fetched successfully", Data = users, MessageType = MessageType.Success }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new Responce() { Message = "Issue while fetching User Details",  Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new Responce() { Message = "Issue while fetching User Details", Data = null, MessageType = MessageType.Fail }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Http.HttpPost]
        public ActionResult AddUpdateUser(UserInput user) {

            if (ModelState.IsValid)
            {
                try 
                {
                    string inputString = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                    Users userData = Newtonsoft.Json.JsonConvert.DeserializeObject<Users>(inputString);

                    int? userId = userService.AddUpdateUser(userData);

                    string existMessage = string.Empty;

                    switch (userId) {
                        case -1 :
                            existMessage = "User Already Exist";
                            break;
                        case -2:
                            existMessage = "EmailId Already Exist";
                            break;
                        case -3:
                            existMessage = "MobileNo Already Exist";
                            break;
                    }

                    if (existMessage != string.Empty) {
                        return Json(new Responce() { Message = existMessage, Data = null, MessageType = MessageType.Warning });
                    }
                    else if (userId != null)
                    {
                        return Json(new Responce() { Message = "User Details saved successfully", Data = userId, MessageType = MessageType.Success });
                    }
                    else
                    {
                        return Json(new Responce() { Message = "Issue while Adding User Details", Data = null, MessageType = MessageType.Fail });
                    }
                }
                catch (Exception) {
                    return Json(new Responce() { Message = "Issue while Adding User Details", Data = null, MessageType = MessageType.Fail });
                }
            }
            else {
                return Json(new Responce() { Message = "Invalid User Details", Data = null, MessageType = MessageType.Fail });
            }
        }

        [System.Web.Http.HttpGet]
        public ActionResult ActiveInactiveUser(int userId, bool IsActive) {
            if (userId > 0) {
                bool success = userService.ActiveInactiveUser(userId,IsActive);
                if (success)
                {
                    return Json(new Responce() { Message = "User Activated or De-activated Successfully.", Data = success, MessageType = MessageType.Success });
                }
                else {
                    return Json(new Responce() { Message = "Issue while Activating/De-Activating User.", Data = null, MessageType = MessageType.Fail });
                }
            }
            else
            {
                return Json(new Responce() { Message = "Invalid User for Activation/De-activation.", Data = null, MessageType = MessageType.Fail });
            }
        }

        [System.Web.Http.HttpGet]
        public ActionResult GetUserDetails(int userId) {
            if (userId > 0)
            {
                Users user = userService.GetUserDetails(userId);
                if (user != null)
                {
                    return Json(new Responce() { Message = "User details fetched successfully.", Data = user, MessageType = MessageType.Success });
                }
                else
                {
                    return Json(new Responce() { Message = "Issue while fetching user details.", Data = null, MessageType = MessageType.Fail });
                }
            }
            else
            {
                return Json(new Responce() { Message = "Invalid User selected.", Data = null, MessageType = MessageType.Fail });
            }
        }


    }

    public class UserInput
    {
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string mobileNo { get; set; }
        public string emailId { get; set; }
        public Nullable<System.DateTime> entryDate { get; set; }
        public Nullable<int> countryId { get; set; }
        public Nullable<int> stateId { get; set; }
        public Nullable<int> cityId { get; set; }
        public bool isActive { get; set; }
    }

}