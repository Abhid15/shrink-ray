using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Service.Interfaces;
using System.Collections.Generic;

namespace shrink_ray.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ShrinkRayUrlController  : ControllerBase
    {
        private IShrinkRayUrlService shortUrlService;
        private readonly ILogger<ShrinkRayUrlController> logger;
        public ShrinkRayUrlController(IShrinkRayUrlService shortUrlService, ILogger<ShrinkRayUrlController> logger)
        {
            this.shortUrlService = shortUrlService;
            this.logger = logger;
        }

        
        /// <summary>
        /// Gets all the URL Objects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<ShrinkRayUrlModel> shortUrls = shortUrlService.GetCollectionFromDataStore();
            return Ok(shortUrls);
        }

        /// <summary>
        /// Gets Specific URL or redirects from Short URL to long URL.
        /// </summary>
        /// <param name="shorturl"></param>
        /// <param name="redirect"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSpecific")]
        public IActionResult Get(string shorturl, [FromQuery(Name = "redirect")] bool redirect = true)
        {
            ShrinkRayUrlModel shortUrl = shortUrlService.GetItemFromDataStore(shorturl);

            if (shortUrl != null)
            {
                if (redirect)
                {
                    return RedirectPermanent(shortUrl.LongURL);
                }
                else
                {
                    return Ok(shortUrl);
                }
            }

            return NotFound();
        }

        /// <summary>
        /// Creates Short URL Object.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] ShrinkRayUrlRequestModel model)
        {
            if (ModelState.IsValid)
            {
                ShrinkRayUrlResponseModel result = shortUrlService.SaveItemToDataStore(model);
                if (result != null)
                {
                    var url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                    result.ShrinkRayUrlModel = url + "/api/v1/ShrinkRayUrl/GetSpecific?shorturl=" + result.Model.ShortURL;
                    return Ok(result);
                }
            }

            return BadRequest(ModelState.Values);
        }
    }
}