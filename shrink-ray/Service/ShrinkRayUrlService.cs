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

        public IEnumerable<ShrinkRayUrlModel> GetCollectionFromDataStore()
        {
            return shortUrlRepository.GetCollectionFromDataStore();
        }

        public ShrinkRayUrlModel GetItemFromDataStore(string shortUrl)
        {
            return shortUrlRepository.GetItemFromDataStoreByShortUrl(shortUrl);

        }

        public ShrinkRayUrlResponseModel SaveItemToDataStore(ShrinkRayUrlRequestModel model)
        {
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