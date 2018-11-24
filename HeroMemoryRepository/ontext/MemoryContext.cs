using HeroesWeb.Models;
using HeroMemoryRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroMemoryRepository.ontext
{
    public class MemoryContext : IMemoryContext
    {
        public MemoryContext()
        {
            int i = 0;
        }

        public List<HeroItem> Heroeslist { get; set; } = new List<HeroItem>()
            {
                new HeroItem() { Id = "8a2bccc1-74a4-4b62-87c8-8d27a5eb40cb", EmpNo = 1, Name = "Viktor", City = "Hässelby"},
                new HeroItem() { Id = "5539e604-8da3-4840-bb35-119cbd9faa74", EmpNo = 2, Name = "Mr. Nice", City = "Stockholm" },
                new HeroItem() { Id = "2ccc668b-d75f-4090-9f46-97c033b3bc0f", EmpNo = 3, Name = "Narco", City = "London"},
                new HeroItem() { Id = "b214cb12-5f6d-45de-940a-a233bb2e7028", EmpNo = 4, Name = "Bombasto", City = "New York" },
                new HeroItem() { Id = "5e7c1ffd-49eb-4ba3-8fc5-6950ae38a4d9", EmpNo = 5, Name = "Celeritas", City = "Lissabon" },
                new HeroItem() { Id = "d538e784-e850-4006-9530-ccb034af9bae", EmpNo = 6, Name = "Magneta", City = "Brussel" },
                new HeroItem() { Id = "22649be9-12a9-4ded-8680-39352fb3fa8c", EmpNo = 7, Name = "RubberMan", City = "Berlin" },
                new HeroItem() { Id = "ea752db6-f342-4d18-aec3-79c22076bd2c", EmpNo = 8, Name = "Dynama", City = "Tehran" },
                new HeroItem() { Id = "f16ed91a-d473-47ad-8fa9-6d88868daac9", EmpNo = 9, Name = "Dr IQ", City = "Cario" },
                new HeroItem() { Id = "bea94db7-22a7-4cfc-97be-1c2cb11f4d6a", EmpNo = 10, Name = "Magma", City = "Peking" },
                new HeroItem() { Id = "5607b9d7-b18d-4c50-af1f-906ad45c456a", EmpNo = 11, Name = "Tornado", City = "Farsta" }
            };

    }
}
