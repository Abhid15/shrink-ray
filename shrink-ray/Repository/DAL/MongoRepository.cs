
using Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Repository.DAL
{
    public class BaseMongoRepository<TModel>
       where TModel : MongoBaseModel
    {
        private readonly IMongoCollection<TModel> mongoCollection;

        /// <summary>
        /// Constructor. Gets connection string from config and constructs the db objects.
        /// </summary>
        /// <param name="mongoDBConnectionString"></param>
        /// <param name="dbName"></param>
        /// <param name="collectionName"></param>
        public BaseMongoRepository(string mongoDBConnectionString, string dbName, string collectionName)
        {
            var client = new MongoClient(mongoDBConnectionString);
            var database = client.GetDatabase(dbName);
            mongoCollection = database.GetCollection<TModel>(collectionName);
        }

        /// <summary>
        /// Get all URLs.
        /// </summary>
        /// <returns></returns>
        public virtual List<TModel> GetList()
        {
            return mongoCollection.Find(book => true).ToList();
        }

        /// <summary>
        /// Get URL by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TModel GetById(string id)
        {
            var docId = new ObjectId(id);
            return mongoCollection.Find<TModel>(m => m.Id == docId).FirstOrDefault();
        }

        /// <summary>
        /// Get by can search based on a parameter. LongUrl is passed from Repo to use that as the predicate. 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TModel GetBy(System.Linq.Expressions.Expression<Func<TModel, bool>> predicate)
        {
            TModel query = mongoCollection.Find(predicate).FirstOrDefault();
            return query;
        }

        /// <summary>
        /// Create URL object.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual TModel Create(TModel model)
        {
            mongoCollection.InsertOne(model);
            return model;
        }

        /// <summary>
        /// Update an URL object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public virtual void Update(string id, TModel model)
        {
            var docId = new ObjectId(id);
            mongoCollection.ReplaceOne(m => m.Id == docId, model);
        }

        /// <summary>
        /// Delete a specific URL object.
        /// </summary>
        /// <param name="model"></param>
        public virtual void Delete(TModel model)
        {
            mongoCollection.DeleteOne(m => m.Id == model.Id);
        }

        /// <summary>
        /// Delete URL object by Id.
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(string id)
        {
            var docId = new ObjectId(id);
            mongoCollection.DeleteOne(m => m.Id == docId);
        }
    }
}