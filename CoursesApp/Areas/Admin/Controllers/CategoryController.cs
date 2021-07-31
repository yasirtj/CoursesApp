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
    public class CategoryController : Controller
    {
        private readonly CategoryService categoryService;
        private readonly IMapper mapper;
        public CategoryController()
        {
            categoryService = new CategoryService();
            mapper = AutoMapperConfig.Mapper;
        }
        // GET: Admin/Category
        public ActionResult Index()
        {
            var categories = categoryService.ReadAll();

            var categoriesList = mapper.Map<List<CategoryModel>>(categories);

            return View(categoriesList);
        }
        public ActionResult Create()
        {
            var categoryModel = new CategoryModel();
            
            InitMainCategories(null, ref categoryModel);
            
            return View(categoryModel);
        }

        [HttpPost]
        public ActionResult Create(CategoryModel data)
        {
            var newCategory = mapper.Map<Category>(data);
            newCategory.Category2 = null;
            int creationResult = categoryService.Create(newCategory);

            if (creationResult == -2)
            {
                InitMainCategories(null, ref data);

                ViewBag.Message = "Category Already Exists!";
                return View(data);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("index", "home");
            }
            var currentCategory = categoryService.ReadById(id.Value);

            var categoryModel = mapper.Map<CategoryModel>(currentCategory);
            //var categoryModel = new CategoryModel { 
            //    Id = currentCategory.ID,
            //    Name = currentCategory.Name,
            //    ParentId = currentCategory.Parent_Id
            //};

            if (currentCategory == null)
            {
                return HttpNotFound($"Category ({id}) Not Found!");
            }
            
            InitMainCategories(currentCategory.ID , ref categoryModel);
            return View(categoryModel);
        }
        [HttpPost]
        public ActionResult Edit(CategoryModel data)
        {
           // var updatedCategory = mapper.Map<Category>(data);
            var updatedCategory = new Category
                {
                    ID = data.Id,
                    Name = data.Name,
                    Parent_Id = data.ParentId
                };
                var result = categoryService.Update(updatedCategory);

                if (result == -2)
                {
                //InitMainCategories(data.Id, ref data);
                ViewBag.Message = "Category Already Exists!";
                InitMainCategories(data.Id, ref data);
                return View(data);
                }
                
                else if (result > 0) {
                    ViewBag.Success = true;
                    ViewBag.Message = $"Category ({data.Id}) Updated Successfully ";
                }
                else
                    ViewBag.Message = $"An Error Occured!";
            InitMainCategories(data.Id, ref data);
            return View(data);
        }
        public ActionResult Delete(int? Id)
        {
            if (Id != null)
            {
                var category = categoryService.ReadById(Id.Value);
                var categoryInfo = mapper.Map<CategoryModel>(category);
                //var categoryInfo = new CategoryModel {
                //    Id = category.ID,
                //    Name = category.Name,
                //    ParentName = category.Category2?.Name
                //};
                return View(categoryInfo);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ConfirmDelete(int? Id)
        {
            if (Id != null)
            {
                var deleted = categoryService.Delete(Id.Value);
                if (deleted)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Delete", new { Id = Id});
            }
            return HttpNotFound();
        }
        private void InitMainCategories(int? categoryToExclude, ref CategoryModel categoryModel)
        {
            var categoriesList = categoryService.ReadAll();
            if (categoryToExclude != null)
            {
                var currentCategory = categoriesList.Where(c=> c.ID == categoryToExclude).FirstOrDefault();
                categoriesList.Remove(currentCategory);
            }
            categoryModel.MainCategories = new SelectList(categoriesList, "ID", "Name");
        }
    }
}