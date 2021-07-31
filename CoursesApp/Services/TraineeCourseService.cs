using CoursesApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesApp.Services
{
    public interface ITraineeCourseService
    {
        IEnumerable<Trainee_Courses> GetTrainees(int? courseId = null);
    }
    public class TraineeCourseService : ITraineeCourseService
    {
        private readonly CoursesEntities db;
        public TraineeCourseService()
        {
            db = new CoursesEntities();
        }

        public IEnumerable<Trainee_Courses> GetTrainees(int? courseId = null)
        {
            return db.Trainee_Courses.Where(t =>
                        courseId == null || t.Course_Id == courseId);
            throw new NotImplementedException();
        }
    }
}