using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TicketManager.MVC.Models
{
    [MetadataType(typeof(MetadataContact))]
    public partial class contact
    {
        public class MetadataContact
        {
            [Required]
            [Display(Name = "Contact Person")]
            public string ContactName { get; set; }

            [DataType(DataType.EmailAddress)]
            [Display(Name = "Email")]
            public string ContactEmail { get; set; }

        }
    }


}