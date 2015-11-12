namespace OMX.Web.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.AspNet.Identity;

    using OMX.Common;
    using OMX.Data.UoW;
    using OMX.Infrastructure.Populators;
    using OMX.Models;
    using OMX.Web.Models.BindingModels;
    using OMX.Web.Models.ViewModels;

    using PagedList;

    [Authorize]
    public class AdsController : BaseController
    {
        private IDropDownListPopulator populator;

        public AdsController(IOMXData data, IDropDownListPopulator populator)
            : base(data)
        {
            this.populator = populator;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Ads(int id, int page = 1)
        {
            var ads =
                this.Data.Ads.All()
                    .OrderByDescending(a => a.CreatedOn)
                    .ThenBy(a => a.Id)
                    .Where(a => a.SubCategoryId == id)
                    .Project()
                    .To<AdViewModel>();

            return this.View(ads.ToPagedList(page, GlobalConstants.AdsPageSize));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult PreviewAdById(int id)
        {
            var ad = this.Data.Ads.GetById(id);
            if (ad == null)
            {
                return this.HttpNotFound("Ad no longer exists");
            }

            var viewModel = Mapper.Map<AdViewModel>(ad);

            return this.View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AdPictures(int id)
        {
            var ad = this.Data.Ads.GetById(id);
            if (ad == null)
            {
                return this.HttpNotFound();
            }

            var pictures = 
                ad
                .Pictures
                .AsQueryable()
                .Project()
                .To<PictureViewModel>()
                .ToList();

            return this.PartialView("_AdPicturesPartial",pictures);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var adsBindingModel = new CreateAdBindingModel()
            {
                SubCategories = this.populator.GetSubCategories()
            };

            return this.View(adsBindingModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAdBindingModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var ad = Mapper.Map<Ad>(model);
                var currentUserId = this.User.Identity.GetUserId();
                ad.OwnerId = currentUserId;
                if (model.files != null)
                {
                    foreach (var file in model.files)
                    {
                        var picture = this.ConvertBytesToPicture(file);
                        ad.Pictures.Add(picture);
                    }
                }

                this.Data.Ads.Add(ad);
                this.Data.SaveChanges();
                return this.RedirectToAction("Index", "Home");
            }

            model.SubCategories = this.populator.GetSubCategories();
            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SearchAds(SearchAdBindingModel model)
        {
            var adsContaingSearchSubstring =
                    this.Data.Ads.All()
                        .Where(a =>
                            a.Title.ToLower().Contains(model.AdSubstring.ToLower()) ||
                            a.Content.ToLower().Contains(model.AdSubstring.ToLower()))
                        .Project()
                        .To<AdViewModel>()
                        .ToList();

            return this.View(adsContaingSearchSubstring);
        }
        
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var ad = this.Data.Ads.GetById(id);
            if (ad == null)
            {
                return this.HttpNotFound("Ad no longer existts");
            }
            var hasAuthorizationForDelete = 
                ad.OwnerId == this.UserProfile.Id || this.User.IsInRole(GlobalConstants.AdminRole);
            if (hasAuthorizationForDelete)
            {
                ad.IsDeleted = true;
                ad.DeletedOn = DateTime.Now;
                this.Data.SaveChanges();
                this.TempData["message"] = "You have successfully deleted your Ad";
                return this.RedirectToAction("MyAds", "Users");
            }
            else
            {
                return this.Content("Error during ad's deletion");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ad = this.Data.Ads.GetById(id);
            if (ad == null)
            {
                return this.HttpNotFound("Ad no longer exists");
            }
            var viewModel = Mapper.Map<EditAdViewModel>(ad);
            viewModel.SubCategories = this.populator.GetSubCategories();
            
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditAdViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var ad = Mapper.Map<Ad>(model);
                ad.OwnerId = this.UserProfile.Id;
                ad.ModifiedOn = DateTime.Now;
                ad.CreatedOn = DateTime.Now;
                if (model.files != null)
                {
                    foreach (var file in model.files)
                    {
                        var picture = this.ConvertBytesToPicture(file);
                        picture.AdId = ad.Id;
                        ad.Pictures.Add(picture);
                    }
                }
                this.Data.Ads.Update(ad);
                this.Data.SaveChanges();
                return this.RedirectToAction("MyAds", "Users");
            }

            return this.View(model);
        }

        private Picture ConvertBytesToPicture(HttpPostedFileBase file)
        {
            Picture image = null;
            using (var memory = new MemoryStream())
            {
                file.InputStream.CopyTo(memory);
                var imgBytes = memory.GetBuffer();
                image = new Picture
                {
                    Content = imgBytes,
                    OwnerId = this.UserProfile.Id
                };
            }
            this.Data.Pictures.Add(image);
            this.Data.SaveChanges();
            return image;
        }
    }
}