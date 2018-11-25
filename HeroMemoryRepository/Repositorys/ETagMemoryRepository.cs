using HeroMemoryRepository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeroesWeb.Repositorys
{
    public class ETagMemoryRepository : IETagRepository
    {
        private IMemoryContext _context;

        public ETagMemoryRepository(IMemoryContext context)
        {
            _context = context;
        }
        public Task<long> GetETagItemAsync(string list, string key, string id)
        {
            if (_context.DictionaryItem.ContainsKey(list))
            {
                if (_context.DictionaryItem[list].TryGetValue(id, out long version))
                {
                    return Task.FromResult<long>(version);
                }
            }

            return Task.FromResult<long>(0);
        }

        public Task<long> GetETagListAsync(string list)
        {
            if (_context.DictionaryList.TryGetValue(list, out long version))
            {
                return Task.FromResult<long>(version);
            }

            return Task.FromResult<long>(0);
        }

        public Task SetETagItemAsync(string list, string id, long value)
        {
            if (_context.DictionaryItem.ContainsKey(list))
            {
                if (_context.DictionaryItem[list].ContainsKey(id))
                {
                    _context.DictionaryItem[list][id] = value;
                }
                else
                {
                    _context.DictionaryItem[list].Add(id, value);
                }
            }
            else
            {
                _context.DictionaryItem.Add(list, new Dictionary<string, long>());
                _context.DictionaryItem[list].Add(id, value);
            }

            return Task.CompletedTask;
        }

        public Task SetETagListAsync(string list, long value)
        {
            if (_context.DictionaryList.ContainsKey(list))
            {
                _context.DictionaryList[list] = value;
            }
            else
            {
                _context.DictionaryList.Add(list, value);
            }

            return Task.CompletedTask;
        }

        public Task DeleteETagAsync(string list, string id)
        {
            if (_context.DictionaryItem.ContainsKey(list))
            {
                _context.DictionaryItem[list].Remove(id);
            }

            return Task.CompletedTask;
        }

        public Task DeleteETagAsync(string list)
        {
            if (_context.DictionaryList.ContainsKey(list))
            {
                _context.DictionaryList.Remove(list);
            }

            if (_context.DictionaryItem.ContainsKey(list))
            {
                _context.DictionaryItem[list] = new Dictionary<string, long>();
            }

            return Task.CompletedTask;
        }
    }
}
