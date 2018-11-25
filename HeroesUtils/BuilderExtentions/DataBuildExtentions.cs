using HeroesWeb.Models;
using HeroesWeb.Services;
using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;

namespace HeroesUtils.BuilderExtentions
{
    public static class DataBuildExtentions
    {
        //
        // Summary:
        //     Adds Date to application.
        //
        // Parameters:
        //   app:
        //     The Microsoft.AspNetCore.Builder.IApplicationBuilder instance this method extends.
        //
        // Returns:
        //     The Microsoft.AspNetCore.Builder.IApplicationBuilder for HttpsRedirection.
        public static IApplicationBuilder UseData(this IApplicationBuilder app)
        {
            IHeroesService _heroesService = (IHeroesService)app.ApplicationServices.GetService(typeof(IHeroesService));

            foreach (var item in new List<HeroItem>()
            {
                new HeroItem() { EmpNo = 1, Name = "Viktor", City = "Hässelby"},
                new HeroItem() { EmpNo = 2, Name = "Mr. Nice", City = "Stockholm" },
                new HeroItem() { EmpNo = 3, Name = "Narco", City = "London"},
                new HeroItem() { EmpNo = 4, Name = "Bombasto", City = "New York" },
                new HeroItem() { EmpNo = 5, Name = "Celeritas", City = "Lissabon" },
                new HeroItem() { EmpNo = 6, Name = "Magneta", City = "Brussel" },
                new HeroItem() { EmpNo = 7, Name = "RubberMan", City = "Berlin" },
                new HeroItem() { EmpNo = 8, Name = "Dynama", City = "Tehran" },
                new HeroItem() { EmpNo = 9, Name = "Dr IQ", City = "Cario" },
                new HeroItem() { EmpNo = 10, Name = "Magma", City = "Peking" },
                new HeroItem() { EmpNo = 11, Name = "Tornado", City = "Farsta" }
            })
            {
                var x = _heroesService.CreateAsync(item);
                x.Wait();
            }

            return app;
        }
    }
}
