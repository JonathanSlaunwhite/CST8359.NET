using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class Student
    {

        public int ID
        {
            get;
            set;

        }

        [Required]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string LastName
        {

            get;
            set;

        }

        [Required]
        [StringLength(50)]
        [DisplayName("First Name")]//is it display name LastName?           fergsergergegerggeg
        public string FirstName
        {

            get;
            set;

        }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public  DateTime EnrollmentDate
        {

            set;
            get;

            }


        public String FullName//return full name
        {

            get
            {
                return LastName + "," + FirstName;
            }


        }


        public IEnumerable<CommunityMembership> Membership { get; set; }




    }
}
