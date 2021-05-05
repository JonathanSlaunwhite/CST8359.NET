using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class CommunityMembership
    {
        // You are missing ID here. The table cannot be there without a prmary key
        public int ID { get; set; }
        public int StudentID
        {
            set;
            get;

        }

        public string CommunityID
        {
            set;
            get;
        }

        // Just deleted this line

        public Community Community
        {
            get;
            set;
        }


        public Student Student
        {
            get;
            set;
        }











    }
}
