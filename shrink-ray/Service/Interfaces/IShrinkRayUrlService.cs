using Model;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IShrinkRayUrlService
    {
        IEnumerable<ShrinkRayUrlModel> GetCollectionFromDataStore();
        ShrinkRayUrlModel GetItemFromDataStore(string shortUrl);
        ShrinkRayUrlResponseModel SaveItemToDataStore(ShrinkRayUrlRequestModel model);
    }
}