using Model;
using Repository.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.DAL

{
    public class ShrinkRayUrlRepository : BaseMongoRepository<ShrinkRayUrlModel>, IRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mongoDBConnectionString"></param>
        /// <param name="dbName"></param>
        /// <param name="collectionName"></param>
        public ShrinkRayUrlRepository(string mongoDBConnectionString, string dbName, string collectionName) : base(mongoDBConnectionString, dbName, collectionName)
        {

        }
        /// <summary>
        /// Get All URLs from DB. This calls the BaseMongoRepository.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ShrinkRayUrlModel> GetCollectionFromDataStore()
        {
            return GetList();
        }

        /// <summary>
        /// Get a specific URL from the DB. This calls the BaseMongoRepository.
        /// </summary>
        /// <param name="shortUrl"></param>
        /// <returns></returns>
        public ShrinkRayUrlModel GetItemFromDataStoreByShortUrl(string shortUrl)
        {
            return GetBy(c => c.ShortURL == shortUrl);
        }


        /// <summary>
        /// Get specific URL by its Long URL. This is used to verify if an URL was already specified. This calls the BaseMongoRepository.
        /// </summary>
        /// <param name="longUrl"></param>
        /// <returns></returns>
        public ShrinkRayUrlModel GetItemFromDataStoreByLongUrl(string longUrl)
        {
            return GetBy(c => c.LongURL == longUrl);
        }

        /// <summary>
        /// Save an URL into the DB. This calls the BaseMongoRepository.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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