using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Lab4.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Data
{
    public class SchoolCommunityContext : DbContext
    {

        public SchoolCommunityContext(DbContextOptions<SchoolCommunityContext> options) : base(options)
        {

        }


        public DbSet<Student> Students { get; set; }
        public DbSet<Community> Communities { get; set; }

        public DbSet<CommunityMembership> CommunityMemberships { get; set; }//one might think




        public DbSet<AnswerImage> AnswerImages
        {
            get;
            set;
        }









        protected override void OnModelCreating(ModelBuilder modelBuilder)//one would think 
        {

            //do i need this??!?!?!?!?!?!

            /* modelBuilder.Entity<Student>().ToTable("Student");
             modelBuilder.Entity<Community>().ToTable("Community");
             modelBuilder.Entity<CommunityMembership>().ToTable("Community Membership").HasKey(c => new { c.StudentID, c.CommunityID });*/

            modelBuilder.Entity<CommunityMembership>()
                            .HasKey(c => new { c.StudentID, c.CommunityID });







            modelBuilder.Entity<AnswerImage>().ToTable("AnswerImage");

        }







    }
}
