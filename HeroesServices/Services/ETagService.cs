using HeroesServices.Interface;
using HeroesWeb.Repositorys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeroesServices.Services
{
    public class ETagService : IETagService
    {
        private readonly IETagRepository _eTagRepository;
        
        public ETagService(IETagRepository eTagRepository)
        {
            _eTagRepository = eTagRepository;
        }

        public async Task<long> GetETagItemAsync(string collection, string key, string id)
        {
            return await _eTagRepository.GetETagItemAsync(collection, key, id);
        }

        public async Task<long> GetETagListAsync(string list)
        {
            return await _eTagRepository.GetETagListAsync(list);
        }

        public async Task SetETagAsync(string collection, string id, long value)
        {
            Task task1 = _eTagRepository.SetETagItemAsync(collection, id, value);
            Task task2 = _eTagRepository.SetETagListAsync(collection, value);

            await task1;
            await task2;
        }
    }
}
