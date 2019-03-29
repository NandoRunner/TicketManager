using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TicketManager.MVC.Models
{
    [MetadataType(typeof(MetadataUser))]
    public partial class user
    {
        public class MetadataUser
        {
            [Required]
            [Display(Name = "User Name")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Email")]
            public string UserEmail { get; set; }

            [Required]
            [MinLength(2, ErrorMessage = "O tamanho mínimo da Abreviação são 2 caracteres.")]
            [StringLength(2, ErrorMessage = "O tamanho máximo são 2 caracteres.")]

            [Display(Name = "User Abrev.")]
            public string UserAbrev { get; set; }
        }
    }


}