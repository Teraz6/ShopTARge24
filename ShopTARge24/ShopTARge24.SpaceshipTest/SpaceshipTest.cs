using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using System;

namespace ShopTARge24.SpaceshipTest
{
    public class SpaceshipTest : TestBase
    {

        [Fact]
        // Test creating a spaceship with correct data
        public async Task Should_CreateSpaceship_WithCorrectData()
        {
            SpaceshipDto dto = new SpaceshipDto
            {
                Id = Guid.NewGuid(),
                Name = "Test Ship",
                Classification = "Cargo",
                BuiltDate = new DateTime(2020, 2, 13),
                Crew = 8,
                EnginePower = 1500,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            var result = await Svc<ISpaceshipServices>().Create(dto);

            Assert.NotNull(result);
            Assert.Equal("Test Ship", result.Name);
        }

        [Fact]
        public async Task Should_UpdateSpaceship_WhenUpdatedSpaceship()
        {
            var guid = Guid.Parse("24083139-8531-4f6f-9ab5-cb7b4e12ba6f");
            SpaceshipDto dto = MockSpaceshipDto();

            SpaceshipDto domain = new();
            domain.Id = Guid.Parse("24083139-8531-4f6f-9ab5-cb7b4e12ba6f");
            domain.Name = "Updated Ship";
            domain.Classification = "Military";
            domain.BuiltDate = new DateTime(2021, 6, 20);
            domain.Crew = 15;
            domain.EnginePower = 2500;
            domain.CreatedAt = dto.CreatedAt;
            domain.ModifiedAt = DateTime.UtcNow;
           
            await Svc<ISpaceshipServices>().Update(dto);

            Assert.NotNull(domain);
            Assert.Equal(domain.Id, guid);
            Assert.NotEqual(dto.Name, domain.Name);
            Assert.NotEqual(dto.Classification, domain.Classification);
            Assert.NotEqual(dto.Crew, domain.Crew);
            Assert.NotEqual(dto.EnginePower, domain.EnginePower);
            Assert.NotEqual(dto.BuiltDate, domain.BuiltDate);
            Assert.NotEqual(dto.ModifiedAt, domain.ModifiedAt);
        }

        //TODO: Add 3 more tests

        private SpaceshipDto MockSpaceshipDto()
        {
            return new SpaceshipDto
            {
                Name = "Mock Ship",
                Classification = "Passenger",
                BuiltDate = new DateTime(2019, 11, 5),
                Crew = 10,
                EnginePower = 1800,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
        }
    }
}
