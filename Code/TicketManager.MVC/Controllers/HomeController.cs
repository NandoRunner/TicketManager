using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using TicketManager.MVC.Models;

namespace TicketManager.MVC.Controllers
{
    public class HomeController : Controller
    {
        private TicketDBEntities db = new TicketDBEntities();
        
        /// <summary>
                                                             /// Multiple Model in single view using Dynamic View
                                                             /// </summary>
                                                             /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to my demo!";
            dynamic mymodel = new ExpandoObject();
            mymodel.Teachers = GetTeachers();
            mymodel.Students = GetStudents();
            mymodel.Checks = GetChecks();
            return View(mymodel);
        }

        /// <summary>
        /// Multiple Model in single view using View Model
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexViewModel()
        {
            ViewBag.Message = "Welcome to my demo!";
            ViewModel mymodel = new ViewModel();
            mymodel.Teachers = GetTeachers();
            mymodel.Students = GetStudents();
            mymodel.Checks = GetChecks();
            return View(mymodel);
        }

        /// <summary>
        /// Multiple Model in single view using View Data
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexViewData()
        {
            ViewBag.Message = "Welcome to my demo!";
            ViewData["Teachers"] = GetTeachers();
            ViewData["Students"] = GetStudents();
            return View();
        }

        /// <summary>
        /// Multiple Model in single view using View Bag
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexViewBag()
        {
            ViewBag.Message = "Welcome to my demo!";
            ViewBag.Teachers = GetTeachers();
            ViewBag.Students = GetStudents();
            return View();
        }

        /// <summary>
        /// Multiple Model in single view using tuple
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexTuple()
        {
            ViewBag.Message = "Welcome to my demo!";
            var tupleModel = new Tuple<List<Teacher>, List<Student>>(GetTeachers(), GetStudents());
            return View(tupleModel);
        }

        /// <summary>
        /// Multiple Model in single view using Partial View
        /// </summary>
        /// <returns></returns>
        public ActionResult PartialView()
        {
            ViewBag.Message = "Welcome to my demo!";
            return View();
        }

        /// <summary>
        /// Render Teacher List
        /// </summary>
        /// <returns></returns>
        public PartialViewResult RenderCheck()
        {
            return PartialView(GetChecks());
        }

        public PartialViewResult RenderTeacher()
        {
            return PartialView(GetTeachers());
        }

        /// <summary>
        /// Render Student List
        /// </summary>
        /// <returns></returns>
        public PartialViewResult RenderStudent()
        {
            return PartialView(GetStudents());
        }


        public ActionResult About()
        {
            return View();
        }
        public List<Check> GetChecks()
        {
            List<Check> checks = new List<Check>();
            foreach (var item in db.status.ToList())
            {
                checks.Add(new Check { Id = item.StatusId, Checked = true, Name = item.StatusName });
            }
            return checks;
        }
        public List<Teacher> GetTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();
            teachers.Add(new Teacher { TeacherId = 1, Code = "TT", Name = "Tejas Trivedi" });
            teachers.Add(new Teacher { TeacherId = 2, Code = "JT", Name = "Jignesh Trivedi" });
            teachers.Add(new Teacher { TeacherId = 3, Code = "RT", Name = "Rakesh Trivedi" });
            return teachers;
        }

        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            students.Add(new Student { StudentId = 1, Code = "L0001", Name = "Amit Gupta", EnrollmentNo = "201404150001" });
            students.Add(new Student { StudentId = 2, Code = "L0002", Name = "Chetan Gujjar", EnrollmentNo = "201404150002" });
            students.Add(new Student { StudentId = 3, Code = "L0003", Name = "Bhavin Patel", EnrollmentNo = "201404150003" });
            return students;
        }
    }
}
