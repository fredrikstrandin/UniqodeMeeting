using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeroesServices.Interface
{
    public interface IETagService
    {
        Task<long> GetETagItemAsync(string list, string key, string id);
        Task<long> GetETagListAsync(string list);
        Task SetETagAsync(string list, string id, long value);        
    }
}
