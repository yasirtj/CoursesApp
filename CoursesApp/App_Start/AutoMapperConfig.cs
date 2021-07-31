using AutoMapper;
using CoursesApp.Data;
using CoursesApp.Models;

namespace CoursesApp
{
    public static class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }
        public static void init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryModel>()
                    .ForMember(dst => dst.Id, src => src.MapFrom(e => e.ID))
                    .ForMember(dst => dst.ParentId, src => src.MapFrom(e => e.Category2.Parent_Id))
                    .ForMember(dst => dst.ParentName, src => src.MapFrom(e => e.Category2.Name))
                .ReverseMap();

                cfg.CreateMap<Trainer, TrainerModel>()
                   // .ForMember(dst => dst.ID, src => src.MapFrom(e => e.ID))
                    //.ForMember(dst => dst.Name, src => src.MapFrom(e => e.Name))
                    //.ForMember(dst => dst.Email, src => src.MapFrom(e => e.Email))
                    //.ForMember(dst => dst.Description, src => src.MapFrom(e => e.Description))
                    //.ForMember(dst => dst.Website, src => src.MapFrom(e => e.Website))
                    //.ForMember(dst => dst.Creation_Date, src => src.MapFrom(e => e.Creation_Date))
                .ReverseMap();

                cfg.CreateMap<Course, CourseModel>()
                     .ForMember(dst => dst.ID, src => src.MapFrom(e => e.ID))
                     .ForMember(dst => dst.TrainerName, src => src.MapFrom(e => e.Trainer.Name))
                     .ForMember(dst => dst.Category_Name, src => src.MapFrom(e => e.Category.Name))
                .ReverseMap();

                cfg.CreateMap<Trainee, TraineeModel>().ReverseMap();


                cfg.CreateMap<Trainee_Courses, TraineeCourseModel>()
                    .ForMember(dst => dst.CourseId, src => src.MapFrom(c => c.Course_Id))
                    .ForMember(dst => dst.Registeration_date, src => src.MapFrom(c => c.Registeration_Date))
                    .ForMember(dst => dst.trainee, src => src.MapFrom(c => c.Trainee));


                cfg.CreateMap<Course_Units, CourseUnitModel>()
                    .ForMember(dst => dst.CourseName, src => src.MapFrom(c => c.Name))
                   // .ForMember(dst => dst.Name, src => src.MapFrom(c => c.Name))
               .ReverseMap();
                    
            });

            Mapper = config.CreateMapper();
        }
    }
}