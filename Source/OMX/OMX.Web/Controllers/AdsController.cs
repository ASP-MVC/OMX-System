namespace OMX.Web.Controllers
{
    using System.IO;
    using System.Linq;
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

    public class AdsController : BaseController
    {
        private IDropDownListPopulator populator;

        public AdsController(IOMXData data, IDropDownListPopulator populator)
            : base(data)
        {
            this.populator = populator;
        }

        [HttpGet]
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
        [Authorize]
        public ActionResult Create()
        {
            var adsBindingModel = new CreateAdBindingModel()
            {
                SubCategories = this.populator.GetSubCategories()
            };

            return this.View(adsBindingModel);
        }

        [HttpPost]
        [Authorize]
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
                        using (var memory = new MemoryStream())
                        {
                            file.InputStream.CopyTo(memory);
                            var imgBytes = memory.GetBuffer();
                            var image = new Picture
                            {
                                Content = imgBytes,
                                OwnerId = currentUserId
                            };
                            ad.Pictures.Add(image);
                        }
                    }
                }

                this.Data.Ads.Add(ad);
                this.Data.SaveChanges();
                return this.RedirectToAction("Index", "Home");
            }

            model.SubCategories = this.populator.GetSubCategories();
            return this.View(model);
        }
    }
}