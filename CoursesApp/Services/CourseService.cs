using CoursesApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoursesApp.Services
{
    public interface ICourseService
    {
        int Create(Course course);
        int Update(Course updatedCourse);
        List<Course> ReadAll(string query = null, int? trainerId = null, int? categoryId = null);
       
        Course Get(int Id);
        bool Delete(int id);

    }
    public class CourseService : ICourseService
    {
        private readonly CoursesEntities db;
        public CourseService()
        {
            db = new CoursesEntities();
        }
        public int Create(Course course)
        {
            course.Creation_Date = DateTime.Now;
            db.Courses.Add(course);
            return db.SaveChanges();
        }

        public bool Delete(int id)
        {
            var course = Get(id);
            if (course != null)
            {
                db.Courses.Remove(course);
                return db.SaveChanges() > 0 ? true : false;
            }
            return false;
        }

        public Course Get(int Id)
        {
            return db.Courses.Find(Id);
            throw new NotImplementedException();
        }

        public List<Course> ReadAll(string query = null, int? trainerId = null, int? categoryId = null)
        {
            return db.Courses.Where(c => 
                                    (trainerId == null || c.Trainer_id == trainerId)
                                   && (categoryId == null || c.Category_Id == categoryId)
                                   && (query == null || c.Name.Contains(query))).ToList();
        }

        

        public int Update(Course updatedCourse)
        {
            db.Courses.Attach(updatedCourse);
            db.Entry(updatedCourse).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }

    }
}