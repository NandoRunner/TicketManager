using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TicketManager.MVC.Models
{
    [MetadataType(typeof(MetadataTicketHistory))]
    public partial class tickethistory
    {
        public class MetadataTicketHistory
        {
            [Required]
            [DataType(DataType.MultilineText)]
            public string TicketHistoryDetail { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, HtmlEncode = true)]
            public System.DateTime TicketHistoryDate { get; set; }

        }
    }


}