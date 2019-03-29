using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketManager.MVC.Models
{
    public class Check
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }

    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Student
    {
        public int StudentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string EnrollmentNo { get; set; }
    }

    public class ViewModel
    {
        public IEnumerable<Check> Checks { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }

}