using HeroesWeb.Models;
using HeroesWeb.Repositorys;
using HeroesWeb.Services;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using XUnitTestUniqodeMeeting.DataAttributes;

namespace XUnit.Test.Servies
{
    namespace XUnitTestUniqodeMeeting.Services
    {
        public class HeroesShold : IClassFixture<MongoDbDatabaseSetting>
        {

            private readonly ITestOutputHelper _output;
            private readonly MongoDbDatabaseSetting _dbSettning;

            private readonly Mock<IHeroRepository> _mockHeroesRepository = new Mock<IHeroRepository>();

            private readonly HeroesService _sut;

            public HeroesShold(ITestOutputHelper output,
                MongoDbDatabaseSetting dbSetting)
            {
                _output = output;
                _dbSettning = dbSetting;

                _sut = new HeroesService(_mockHeroesRepository.Object);

                _output.WriteLine("UserItem should include email.");
                
                _mockHeroesRepository.Setup(w => w.CreateAsync(It.Is<HeroItem>(q => !string.IsNullOrEmpty(q.Name))))
                                                    .ReturnsAsync((HeroItem type) =>
                                                    {
                                                        return type;
                                                    });
            }

            [Theory]
            [TestHeroes]
            [Trait("Hero", "Registration")]
            public async Task BeCreated(HeroItem item)
            {
                _output.WriteLine("Run CreateAsync with email.");
                HeroItem ret = await _sut.CreateAsync(item);

                _output.WriteLine("Check if CreateAsync returns OjectId.");
                Assert.True(ret.Name == item.Name);
            }
        }
    }
}
