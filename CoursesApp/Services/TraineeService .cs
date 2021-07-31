using CoursesApp.Data;

namespace CoursesApp.Services
{
    public interface ITraineeService
    {
        Trainee Create(Trainee trainee);
    }
    public class TraineeService : ITraineeService
    {
        private readonly CoursesEntities db;
        public TraineeService()
        {
            db = new CoursesEntities();
        }

        public Trainee Create(Trainee trainee)
        {
            db.Trainees.Add(trainee);
            int savingResult = db.SaveChanges();
            if (savingResult > 0)
            {
                return trainee;
            }
            return null;
            
        }

    }
}