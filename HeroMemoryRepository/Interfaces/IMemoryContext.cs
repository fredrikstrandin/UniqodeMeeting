using HeroesWeb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroMemoryRepository.Interfaces
{
    public interface IMemoryContext
    {
        List<HeroItem> Heroeslist { get; set; }
    }
}
