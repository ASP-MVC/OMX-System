﻿using System.Collections.Generic;
using Kendo.Mvc.Infrastructure;
using OMX.Infrastructure.Populators;

namespace OMX.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.UI;

    using AutoMapper.QueryableExtensions;

    using OMX.Data.UoW;
    using OMX.Models;
    using OMX.Web.Areas.Administration.Models.BindingModels;
    using OMX.Web.Areas.Administration.Models.ViewModels;
    using OMX.Web.Models.ViewModels;

    public class CategoriesController : BaseController
    {
        private const int TopNineCategories = 9;
        
        public CategoriesController(IOMXData data)
            : base(data)
        {
        }

        [HttpGet]
        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public ActionResult All()
        {
            var categories = this.Populator
                .GetCategoriesWithSubCategories()
                .AsQueryable()
                .ProjectTo<CategoryViewModel>()
                .ToList();
            
            return this.PartialView("_AllCategoriesPartial", categories);
        }

        [HttpGet]
        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public ActionResult GetTopCategories()
        {
            var categories = this.Populator
                .GetCategoriesWithSubCategories()
                .Take(TopNineCategories)
                .AsQueryable()
                .ProjectTo<CategoryViewModel>()
                .ToList();

            return this.PartialView("_AllCategoriesPartial", categories);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryBindingModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var category = new Category { Title = model.Title };
                this.Data.Categories.Add(category);
                this.Data.SaveChanges();
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(model);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult CategorySubCategories(int id)
        {
            var category = this.Data.Categories.GetById(id);
            if (category == null)
            {
                return this.HttpNotFound("The selected category no longer exists");
            }
            
            this.ViewBag.CategoryName = category.Title;

            var subCategories = 
                category.SubCategories
                .AsQueryable()
                .Project()
                .To<MinifiedSubCategoryViewModel>()
                .ToList();

            return this.PartialView("_SubCategoriesPartial", subCategories);
        }
    }
}