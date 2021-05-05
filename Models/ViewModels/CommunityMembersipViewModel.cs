using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Models.ViewModels
{
     public class CommunityMembersipViewModel
    {




        /*
        public Student Student { get; set; }


        public IEnumerable<Community> re { get; set; }

        public IEnumerable<Community> ur { get; set; }
        */

         public string CommunityId { get; set; }
         public string Title { get; set; }
         public bool IsMember { get; set; }

        public string number { get; set; }



        public IEnumerable<Student> Students { get; set; }

         public IEnumerable<Community> Communities { get; set; }


         public IEnumerable<CommunityMembership> CommunityMemberships { get; set; }

    }
}
