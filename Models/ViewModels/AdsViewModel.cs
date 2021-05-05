using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Models.ViewModels
{
    public class AdsViewModel
    {
        public Community Community { get; set; }


       // public IEnumerable<Community> Communities { get; set; }


        public IEnumerable<AnswerImage> Advertisements { get; set; }

        public string comID
        {
            get;
            set;
        }




    }
}
