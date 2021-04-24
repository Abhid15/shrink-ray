using System.Collections.Generic;
using Model;

namespace Repository.DAL.Interfaces
{
    public interface IRepository
    {
        IEnumerable<ShrinkRayUrlModel> GetCollectionFromDataStore();
        ShrinkRayUrlModel GetItemFromDataStoreByShortUrl(string shortUrl);
        ShrinkRayUrlModel GetItemFromDataStoreByLongUrl(string shortUrl);

        ShrinkRayUrlModel SaveItemToDataStore(ShrinkRayUrlModel model);
    }
}
