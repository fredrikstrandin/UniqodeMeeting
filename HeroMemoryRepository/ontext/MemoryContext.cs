using HeroesWeb.Models;
using HeroMemoryRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroMemoryRepository.ontext
{
    public class MemoryContext : IMemoryContext
    {
        public List<HeroItem> Heroeslist { get; set; } = new List<HeroItem>();
        
        public Dictionary<string, Dictionary<string, long>> DictionaryItem { get; set; } = new Dictionary<string, Dictionary<string, long>>();
        public Dictionary<string, long> DictionaryList { get; set; } = new Dictionary<string, long>();
    }
}
