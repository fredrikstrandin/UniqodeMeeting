using HeroesWeb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroMemoryRepository.Interfaces
{
    public interface IMemoryContext
    {
        List<HeroItem> Heroeslist { get; set; }

        Dictionary<string, Dictionary<string, long>> DictionaryItem { get; set; }
        Dictionary<string, long> DictionaryList { get; set; }
    }
}
