using System;
using System.Collections.Generic;
using System.ComponentModel;//added in for file name
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Lab4.Models
{
    public class AnswerImage
    {



        public int AnswerImageId
        {
            get;
            set;
        }

        [Required]
        [DisplayName("File Name")]
        public string FileName
        {
            get;
            set;
        }

        [Required]
        [Url]
        public string Url
        {
            get;
            set;
        }

        [Required]
        [DisplayName("File sdf")]
        public string Memb
        {
            get;
            set;
        }




    }
}
