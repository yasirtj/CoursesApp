using CoursesApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesApp.Services
{
    public interface ITrainerService
    {
        
        int Update(Trainer updatedTrainer);
        int Create(Trainer trainer);
        Trainer FindByEmail(string email);
        Trainer ReadById(int Id);
        List<Trainer> ReadAll();
        Trainer Get(int Id);
        bool Delete(int id);
    }
    public class TrainerService : ITrainerService
    {
        private readonly CoursesEntities db;
        public TrainerService()
        {
            db = new CoursesEntities();
        }
        public int Create(Trainer trainer)
        {
            var TrainerExists = FindByEmail(trainer.Email);
            if (TrainerExists != null)
            {
                return -2;
            }
            trainer.Creation_Date = DateTime.Now;
            db.Trainers.Add(trainer);
            return db.SaveChanges();
           
        }

        public bool Delete(int id)
        {
            var trainer = Get(id);
            if (trainer != null)
            {
                db.Trainers.Remove(trainer);
                return db.SaveChanges() > 0 ? true : false;
            }
            return false;
        }

        public Trainer FindByEmail(string email)
        {
            return db.Trainers.Where(t => t.Email == email).FirstOrDefault();
        }

        public Trainer Get(int Id)
        {
            return db.Trainers.Find(Id);
        }

        public List<Trainer> ReadAll()
        {
            return db.Trainers.ToList();
            throw new NotImplementedException();
        }

        public Trainer ReadById(int Id)
        {
            return db.Trainers.Find(Id);
            throw new NotImplementedException();
        }

        public int Update(Trainer updatedTrainer)
        {
            var trainerName = updatedTrainer.Name.ToLower();
            var trainersList = db.Trainers.Where(c => c.Name.ToLower() != trainerName);
            if (trainersList.Where(c => c.Name.ToLower() == trainerName).Any())
            {
                return -2;
            }

            db.Trainers.Attach(updatedTrainer);
            db.Entry(updatedTrainer).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }

    }
}