﻿namespace OMX.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Data.Entity;
    using System.Web.Mvc;

    using AutoMapper;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using OMX.Data.UoW;

    public abstract class KendoAdminGridController : BaseAdminController
    {
        protected KendoAdminGridController(IOMXData data)
            : base(data)
        {
        }

        protected abstract IEnumerable GetData();

        protected abstract T GetById<T>(object id) where T : class;

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var ads =
                this.GetData()
                .ToDataSourceResult(request);

            return this.Json(ads);
        }
        
        protected JsonResult GridOperation<T>(T model, [DataSourceRequest]DataSourceRequest request)
        {
            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        protected void ChangeEntityStateAndSave(object dbModel, EntityState state)
        {
            var entry = this.Data.Context.Entry(dbModel);
            entry.State = state;
            this.Data.SaveChanges();
        }
    }
}