using CoursesApp.Common;
using CoursesApp.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace CoursesApp.Services
{
    public interface ICourseUnitService
    {
        SavingStatus Create(Course_Units unit);
        int Update(Course_Units updatedCourse);
        IEnumerable<Course_Units> ReadCourseUnit(int courseId);

        Course_Units Get(int Id);
        Course_Units Get(int courseId, string name);
       // bool Delete(int id);

    }
    public class CourseUnitService : ICourseUnitService
    {
        private readonly CoursesEntities _db;
        public CourseUnitService()
        {
            _db = new CoursesEntities();
        }
        public SavingStatus Create(Course_Units unit)
        {
                
                var unitExists = Get(unit.Course_Id, unit.Name);
                if (unitExists != null)
                {
                    return SavingStatus.Exists;
                }
          
                _db.Course_Units.Add(unit);
          
                int result = _db.SaveChanges();
                
            if (result > 0)
                return SavingStatus.Saved;
        
            return SavingStatus.Error;
        }

        //public bool Delete(int id)
        //{
        //    var courseUnit = Get(id);
        //    if (courseUnit != null)
        //    {
        //        _db.Course_Units.Remove(courseUnit);
        //        return _db.SaveChanges() > 0 ? true : false;
        //    }
        //    return false;
        //}

        public Course_Units Get(int Id)
        {
            return _db.Course_Units.Find(Id);
        }

        public Course_Units Get(int courseId, string name)
        {
            return _db.Course_Units.FirstOrDefault(u => u.Course_Id == courseId &&  u.Name == name);
        }

        public IEnumerable<Course_Units> ReadCourseUnit(int courseId)
        {
            return _db.Course_Units.Where(u => u.Course_Id == courseId);
        }

        public int Update(Course_Units updatedCourseUnit)
        {
            _db.Course_Units.Attach(updatedCourseUnit);
            _db.Entry(updatedCourseUnit).State = System.Data.Entity.EntityState.Modified;
            return _db.SaveChanges();
        }
    }
}