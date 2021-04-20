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

        // GET: api/Default
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<ShrinkRayUrlModel> shortUrls = shortUrlService.GetCollectionFromDataStore();
            return Ok(shortUrls);
        }

        [HttpGet]
        [Route("GetSpecific")]
        public IActionResult GetSpecific(string shorturl, [FromQuery(Name = "redirect")] bool redirect = true)
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

        // POST: api/Default
        [HttpPost]
        public IActionResult Post([FromBody] ShrinkRayUrlRequestModel model)
        {
            if (ModelState.IsValid)
            {
                ShrinkRayUrlResponseModel result = shortUrlService.SaveItemToDataStore(model);
                if (result != null)
                    return Ok(result);
            }

            return BadRequest(ModelState.Values);
        }
    }
}