using Model;
using Service.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
namespace Service.Map
{
    public static class ShrinkRayUrlModelMapper
    {
        /// <summary>
        /// Creates a short url model from Request.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public static ShrinkRayUrlModel MapRequestModelToDBModel(ShrinkRayUrlRequestModel requestModel)
        {
            ShrinkRayUrlModel result = new ShrinkRayUrlModel
            {
                CreateDate = DateTime.Now,
                LongURL = requestModel.LongURL
            };

            result.ShortURL = TokenGenerator.GenerateShortUrl();

            return result;
        }
    }
}
