namespace OMX.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using OMX.Application.Ads.Commands;
    using OMX.Application.Ads.Queries.GetAdById;
    using OMX.Application.Ads.Queries.GetAdPictures;
    using OMX.Application.Ads.Queries.GetSubCategoryAds;
    using OMX.Application.Ads.Queries.SearchAdsByTitle;
    using System.Threading.Tasks;

    public class AdsController : BaseController
    {
        [HttpGet]
        //[AllowAnonymous]
        public async Task<IActionResult> GetSubCategoryAds(int id, int page = 1)
        {
            var ads = await Mediator.Send(new GetSubCategoryAdsQuery { SubCategoryId = id, Page = page });

            return Ok(ads);
        }

        [HttpGet]
      //  [Route("{id:int}")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetAdById(int id)
        {
            var ad = await Mediator.Send(new GetAdByIdQuery { AdId = id});
            if (ad == null)
            {
                return NotFound("Ad no longer exists");
            }

            return Ok(ad);
        }

        [HttpGet]
       // [Route("ad-pictures/{id:int}")]
       // [AllowAnonymous]
        public async Task<IActionResult> GetAdPictures(int id)
        {
            var pictures = await Mediator.Send(new GetAdPicturesQuery { AdId = id });

            return Ok(pictures);

        }

        [HttpPost]
       // [Route("create")]
        public async Task<IActionResult> Create(CreadAdCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet]
      //  [AllowAnonymous]
        public async Task<IActionResult> SearchAdsByTitle(string adTitleSubstring)
        {
            var adsContaingSearchSubstring = await Mediator.Send(new SearchAdsByTitleQuery { AdTitleSubstring = adTitleSubstring });

            return Ok(adsContaingSearchSubstring);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await Mediator.Send(new DeleteAdCommand { AdId = id });
            // TODO return object instead of message
            return Ok(message);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAdCommand command)
        {
            var updateAd = await Mediator.Send(command);

            return Ok(updateAd);
        }
    }
}
