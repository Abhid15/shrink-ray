using Model;
using Repository.DAL.Interfaces;
using Service.Interfaces;
using Service.Map;
using System.Collections.Generic;

namespace Service
{
    public class ShrinkRayUrlService : IShrinkRayUrlService
    {
        private readonly IRepository shortUrlRepository;

        public ShrinkRayUrlService(IRepository repository)
        {
            shortUrlRepository = repository;
        }

        /// <summary>
        /// Gets URL Collection. Makes the call to the Repo layer.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ShrinkRayUrlModel> GetCollectionFromDataStore()
        {
            return shortUrlRepository.GetCollectionFromDataStore();
        }

        /// <summary>
        /// Get Specific URL Object from DB.
        /// </summary>
        /// <param name="shortUrl"></param>
        /// <returns></returns>
        public ShrinkRayUrlModel GetItemFromDataStore(string shortUrl)
        {
            return shortUrlRepository.GetItemFromDataStoreByShortUrl(shortUrl);

        }

        /// <summary>
        /// Used to save data to DB. Makes appropriate calls to repo layer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ShrinkRayUrlResponseModel SaveItemToDataStore(ShrinkRayUrlRequestModel model)
        {
            // Check if URL object exists for this request: If Yes, then return that object with appropriate message. If No, then save to DB. 
            ShrinkRayUrlModel previouslySaved = shortUrlRepository.GetItemFromDataStoreByLongUrl(model.LongURL);
            if (previouslySaved != null)
            {
                return new ShrinkRayUrlResponseModel { Model = previouslySaved, Success = true, Message = "This url has been saved previously" };
            }
            else
            {
                ShrinkRayUrlModel savedModel = shortUrlRepository.SaveItemToDataStore(ShrinkRayUrlModelMapper.MapRequestModelToDBModel(model));

                return new ShrinkRayUrlResponseModel
                {
                    Model = savedModel,
                    Success = true,
                    Message = "Saved successfully"
                };
            }

        }
    }
}