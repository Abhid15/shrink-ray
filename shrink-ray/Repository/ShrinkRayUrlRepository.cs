using Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class ShortUrlRepository : BaseMongoRepository<ShrinkRayUrlModel>, IRepository
    {

        public ShortUrlRepository(string mongoDBConnectionString, string dbName, string collectionName) : base(mongoDBConnectionString, dbName, collectionName)
        {

        }
        public IEnumerable<ShrinkRayUrlModel> GetCollectionFromDataStore()
        {
            return GetList();
        }

        public ShrinkRayUrlModel GetItemFromDataStoreByShortUrl(string shortUrl)
        {
            return GetBy(c => c.ShortURL == shortUrl);
        }


        public ShrinkRayUrlModel GetItemFromDataStoreByLongUrl(string longUrl)
        {
            return GetBy(c => c.LongURL == longUrl);
        }

        public ShrinkRayUrlModel SaveItemToDataStore(ShrinkRayUrlModel model)
        {
            try
            {
                this.Create(model);
            }
            catch (Exception)
            {
                return null;
            }

            return model;
        }
    }
}