namespace OMX.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using OMX.Data.UoW;
    using OMX.Models;
    using OMX.Web.Areas.Administration.Models.BindingModels;
    using OMX.Web.Areas.Administration.Models.ViewModels;

    public class KendoAdsController : KendoAdminGridController
    {
        public KendoAdsController(IOMXData data)
            : base(data)
        {
        }

        public ActionResult ManageAds()
        {
            return this.View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Ads.All().Project().To<AdminAdViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Ads.GetById(id) as T;
        }

        public ActionResult Create([DataSourceRequest]DataSourceRequest request, AdminAdViewModel model)
        {
            //this.ModelState.Remove("Id");
            if (model != null && this.ModelState.IsValid)
            {
                var ad = Mapper.Map<Ad>(model);
                ad.OwnerId = this.UserProfile.Id;
                ad.CreatedOn = DateTime.Now;
                this.Data.Ads.Add(ad);
                this.Data.SaveChanges();
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Edit([DataSourceRequest]DataSourceRequest request, AdminAdViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var ad = this.Data.Ads.GetById(model.Id);
                ad.Title = model.Title;
                ad.Content = model.Content;
                ad.Price = model.Price;
                ad.SubCategoryId = model.SubCategoryId;
                this.Data.Ads.Update(ad);
                this.ChangeEntityStateAndSave(ad, EntityState.Modified);
            }
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, AdminAdViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                this.Data.Ads.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}