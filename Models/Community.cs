using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;///i added


using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class Community
    {

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]// make sure this is not database generated
        [Display(Name = "Registration Number")]
        public string ID
        {

            get;
            set;
        }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title
        {
            set;
            get;

        }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget
        {

            get;
            set;
        }


        public IEnumerable<CommunityMembership> Membership { get; set; }




     


    }
}
