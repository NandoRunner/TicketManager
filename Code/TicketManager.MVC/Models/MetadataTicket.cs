using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TicketManager.MVC.Models
{
    [MetadataType(typeof(MetadataTicket))]
    public partial class ticket
    {
        public class MetadataTicket
        {
            [Required]
            [Display(Name = "Customer")]
            public int CustomerId { get; set; }

            [Required]
            [Display(Name = "Status")]
            public int StatusId { get; set; }

            [Required]
            [Display(Name = "Create User")]
            public int CreateUserid { get; set; }

            [Required]
            [Display(Name = "Contact Person")]
            public int ContactId { get; set; }

            [Required]
            [Display(Name = "Position/Result")]
            public Nullable<int> StatusDetailId { get; set; }

            [Required]
            [Display(Name = "Issue Description")]
            [DataType(DataType.MultilineText)]
            public string TicketIssueDescription { get; set; }

            [Required]
            [Display(Name = "Issue Subject")]
            public string TicketIssueSubject { get; set; }

            [Required]
            [Display(Name = "Create Date")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, HtmlEncode = true)]
            public System.DateTime TicketDate { get; set; }

        }
    }


}