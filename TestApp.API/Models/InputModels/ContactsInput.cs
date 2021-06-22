using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TestApp.API.Models.InputModels
{
    public class ContactInput
    {
        public int contactId { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string mobileNo { get; set; }

        [DataType(DataType.EmailAddress)]
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