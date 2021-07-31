using CoursesApp.Models;
using CoursesApp.Data;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CoursesApp.Services
{
    public interface IAdminService
    {
        bool Login(string Email, string Password);
        bool ChangePassword(string Email, string Password);
        bool ForgotPassword(string Email);
    }
    public class AdminService : IAdminService
    {
        public CoursesEntities context { get; set; }
        public AdminService()
        {
            context = new CoursesEntities();
        }
        public bool Login(string Email, string Password)
        {
            return context.Admins.Where(a => a.Email == Email && a.Password == Password).Any();
           
        }
        public bool ChangePassword(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public bool ForgotPassword(string Email)
        {
            throw new NotImplementedException();
        }

    }
}