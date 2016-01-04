namespace OMX.Web.Controllers
{
    #region
    using System;
    using System.Collections.Generic;
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
    using OMX.Infrastructure.Validators;
    using OMX.Models;
    using OMX.Web.Models.BindingModels;
    using OMX.Web.Models.ViewModels;

    using PagedList;
    #endregion

    [RoutePrefix("ads")]
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
        [Route("{id:int}")]
        [AllowAnonymous]
        public ActionResult PreviewAdById(int id)
        {
            var ad = this.Data.Ads.GetById(id);
            if (ad == null)
            {
                return this.HttpNotFound("Ad no longer exists");
            }
            this.IncrementVisitsCount(ad);
            var viewModel = Mapper.Map<AdViewModel>(ad);

            return this.View(viewModel);
        }

        [HttpGet]
        [Route("ad-pictures/{id:int}")]
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
                .Where(p => p.IsDeleted == false)
                .AsQueryable()
                .Project()
                .To<PictureViewModel>()
                .ToList();

            return this.PartialView("_AdPicturesPartial",pictures);
        }

        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            var adsBindingModel = new CreateAdBindingModel()
            {
                SubCategories = this.populator.GetSubCategories()
            };

            return this.View(adsBindingModel);
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAdBindingModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var ad = Mapper.Map<Ad>(model);
                var currentUserId = this.User.Identity.GetUserId();
                ad.OwnerId = currentUserId;
                if (this.TempData.ContainsKey("uploaded-pics"))
                {
                    model.files = this.TempData["uploaded-pics"] as IEnumerable<HttpPostedFileBase>;
                    if (model.files != null)
                    {
                        foreach (var file in model.files)
                        {
                            if (!ImageValidator.IsValidImage(model.files))
                            {
                                this.TempData["message-err"] = SystemMessages.InvalidImage;
                                return this.RedirectToAction("MyAds", "Users");
                            }
                            var picture = this.ConvertBytesToPicture(file);
                            ad.Pictures.Add(picture);
                        }
                    }
                }

                this.Data.Ads.Add(ad);
                this.Data.SaveChanges();
                return this.RedirectToAction("Index", "Home");
            }
            string validationErrors = string.Join("\n\r",
                    this.ModelState.Values.Where(e => e.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray());
            this.TempData[GlobalConstants.ErrorKey] = validationErrors;
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
                if (ad.OwnerId != this.UserProfile.Id && !this.User.IsInRole(GlobalConstants.AdminRole))
                {
                    this.TempData["message-err"] = SystemMessages.AdEditFailure;
                    return this.RedirectToAction("MyAds", "Users");
                }
                ad.ModifiedOn = DateTime.Now;
                ad.CreatedOn = DateTime.Now;
                if (this.TempData.ContainsKey("uploaded-pics"))
                {
                    model.files = this.TempData["uploaded-pics"] as IEnumerable<HttpPostedFileBase>;

                    if (model.files != null)
                    {
                        if (!ImageValidator.IsValidImage(model.files))
                        {
                            this.TempData["message-err"] = SystemMessages.InvalidImage;
                            return this.RedirectToAction("MyAds", "Users");
                        }
                        foreach (var file in model.files)
                        {
                            var picture = this.ConvertBytesToPicture(file);
                            picture.AdId = ad.Id;
                            ad.Pictures.Add(picture);
                        }
                    }
                }
                this.Data.Ads.Update(ad);
                this.Data.SaveChanges();
                this.TempData["message"] = SystemMessages.AdEditSuccess;
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

        private void IncrementVisitsCount(Ad ad)
        {
            ad.Visit = ad.Visit + 1;
            this.Data.Ads.Update(ad);
            this.Data.SaveChanges();
        }
    }
}