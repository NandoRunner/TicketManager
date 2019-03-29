using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TicketManager.MVC.Models
{
    [MetadataType(typeof(MetadataStatus))]
    public partial class status
    {
        public class MetadataStatus
        {
            [Required]
            [Display(Name = "Status")]
            public string StatusName { get; set; }
        }
    }


}