using AutoMapper;
using CoursesApp.Data;
using CoursesApp.Models;
using CoursesApp.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CoursesApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly IMapper mapper;
        private readonly CourseService courseService;

        public CourseController()
        {
            mapper = AutoMapperConfig.Mapper;
            courseService = new CourseService();
        }
        // GET: Admin/Course
        public ActionResult Index()
        {
            var courses = courseService.ReadAll();

            return View(mapper.Map<List<Course>, List<CourseModel>>(courses));
        }

        public ActionResult Info(int? Id)
        {
            if (Id == null || Id == 0)
                return HttpNotFound("Course Not Found!");

           var courseInfo = courseService.Get(Id.Value);
                if(courseInfo == null)
                    return HttpNotFound("Course Not Found!");

                var mappedCourseInfo = mapper.Map<Course, CourseModel>(courseInfo);
                return View(mappedCourseInfo);
            }

        public ActionResult Enroll(int Id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = $"/Course/Enroll/{Id}" });
            }
            
            return View();
        }

        }
    }
