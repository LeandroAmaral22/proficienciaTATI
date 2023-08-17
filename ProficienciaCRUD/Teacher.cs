using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class Teacher
{
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Gender { get; set; }

        public string Experience { get; set; }

        public int YearsExperience { get; set; }

        public DateTime AdmissionDate { get; set; }

        public string Discipline { get; set; }

        public string Active { get; set; }
}

