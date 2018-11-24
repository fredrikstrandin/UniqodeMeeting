using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeroesServices.Interface
{
    public interface IETagService
    {
        Task<long> GetETagAsync(string collection, string key, string id);
        void SetETagAsync(string collection, string key, string id, long value);
    }
}
