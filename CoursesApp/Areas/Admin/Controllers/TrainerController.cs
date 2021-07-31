using AutoMapper;
using CoursesApp.Data;
using CoursesApp.Models;
using CoursesApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TrainerController : Controller
    {
        private readonly IMapper mapper;
        private readonly TrainerService trainerService;
        public TrainerController()
        {
            mapper = AutoMapperConfig.Mapper;
            trainerService = new TrainerService();
        }
        // GET: Admin/Trainer
        public ActionResult Index()
        {
            var trainersList = trainerService.ReadAll();
            var mappedTrainersList = mapper.Map<IEnumerable<TrainerModel>>(trainersList);
            return View(mappedTrainersList);
        }

        // GET: Admin/Trainer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Trainer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Trainer/Create
        [HttpPost]
        public ActionResult Create(TrainerModel trainerData)
        {
            try
            {
                if (ModelState.IsValid)
                {

                
                var trainerDTO = mapper.Map<Trainer>(trainerData);
                int result = trainerService.Create(trainerDTO);
                if (result >= 1)
                {
                    return RedirectToAction("Index");

                }
                else if (result == -2)
                {
                    ViewBag.Message = "A trainer with the same email address already exists!";
                }
                else
                {
                    ViewBag.Message = "An error occured, Please try again!";
                }
               
             }
                return View(trainerData);
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(trainerData);
            }
        }

        // GET: Admin/Trainer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("index", "home");
            }
            var currentTrainer = trainerService.ReadById(id.Value);
            var trainerModel = mapper.Map<TrainerModel>(currentTrainer);
            if (currentTrainer == null)
            {
                return HttpNotFound($"Trainer ({id}) Not Found!");
            }
            
            InitMainTrainers(currentTrainer.ID, ref trainerModel);
            return View(trainerModel);
        }
        [HttpPost]
        public ActionResult Edit(TrainerModel data)
        {
            var trainerDTO = mapper.Map<Trainer>(data);

            int result = trainerService.Update(trainerDTO);

            if (result == -2)
            {
                InitMainTrainers(data.ID, ref data);
                ViewBag.Message = "Trainer Already Exists!";
                return View(data);
            }

            else if (result > 0)
            {
                ViewBag.Success = true;
                ViewBag.Message = $"Trainer ({data.ID}) Updated Successfully ";
            }
            else
                ViewBag.Message = $"An Error Occured!";
            InitMainTrainers(data.ID, ref data);
            return View(data);
        }

        // GET: Admin/Trainer/Delete/5
        public ActionResult Delete(int? Id)
        {
            if (Id != null)
            {
                var trainer = trainerService.Get(Id.Value);
                
                var trainerModel = mapper.Map<TrainerModel>(trainer);

                //var trainerInfo = new TrainerModel
                //{
                //    ID = trainer.ID,
                //    Name = trainer.Name,
                //    Email = trainer.Email,
                //    Description = trainer.Description,
                //    Creation_Date = trainer.Creation_Date,
                //    Website = trainer.Website

                //};
                return View(trainerModel);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ConfirmDelete(int? Id)
        {
            if (Id != null)
            {
                var deleted = trainerService.Delete(Id.Value);
                if (deleted)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Delete", new { Id = Id });
            }
            return HttpNotFound();
        }
        private void InitMainTrainers(int? trainerToExclude, ref TrainerModel trainerModel)
        {
            var trainersList = trainerService.ReadAll();
            if (trainerToExclude != null)
            {
                var currentTrainer = trainersList.Where(c => c.ID == trainerToExclude).FirstOrDefault();
                trainersList.Remove(currentTrainer);
            }
            trainerModel.MainTrainers = new SelectList(trainersList, "ID", "Name");
        }
    }
}
