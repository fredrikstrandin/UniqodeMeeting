﻿using HeroesServices.Interface;
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

        public async Task<long> GetETagAsync(string collection, string key, string id)
        {
            return await _eTagRepository.GetETagAsync(id, collection, key);
        }

        public async void SetETagAsync(string collection, string key, string id, long value)
        {
            await _eTagRepository.SetETagAsync(collection, key, id, value);
        }
    }
}
