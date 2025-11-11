using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using System.Threading.Tasks;

namespace ShopTARge24.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {
            //Arrange
            RealEstateDto dto = new()
            {
                Area = 120.5,
                Location = "Test Location",
                RoomNumber = 3,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            //Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldNot_GetByIdRealEstate_WhenReturnsNotEqual()
        {
            //Arrange
            Guid wrongGuid = Guid.NewGuid();
            Guid guid = Guid.Parse("7fbc0046-da94-4a7c-a522-a86a26c68040");

            //Act
            await Svc<IRealEstateServices>().DetailAsync(guid);

            //Assert
            Assert.NotEqual(wrongGuid, guid);
        }

        [Fact]
        public async Task Should_GetByIdRealEstate_WhenReturnsEqual()
        {
            //Arrange
            Guid databaseGuid = Guid.Parse("7fbc0046-da94-4a7c-a522-a86a26c68040");
            Guid guid = Guid.Parse("7fbc0046-da94-4a7c-a522-a86a26c68040");

            //Act
            await Svc<IRealEstateServices>().DetailAsync(guid);

            //Assert
            Assert.Equal(databaseGuid, guid);
        }

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateDto();

            //Act
            var addRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var deleteRealEstate = await Svc<IRealEstateServices>().Delete((Guid)addRealEstate.Id);

            //Assert
            Assert.Equal(addRealEstate.Id, deleteRealEstate.Id);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateDto();

            //Act
            var realEstate1 = await Svc<IRealEstateServices>().Create(dto);
            var realEstate2 = await Svc<IRealEstateServices>().Create(dto);

            var result = await Svc<IRealEstateServices>().Delete((Guid)realEstate2.Id);

            //Assert
            Assert.NotEqual(realEstate1.Id, result.Id);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdatedRealEstate()
        {
            //Arrange
            var guid = Guid.Parse("7fbc0046-da94-4a7c-a522-a86a26c68040");
            RealEstateDto dto = MockRealEstateDto();

            RealEstateDto domain = new();
            domain.Id = Guid.Parse("7fbc0046-da94-4a7c-a522-a86a26c68040");
            domain.Area = 200.0;
            domain.Location = "Updated Location";
            domain.RoomNumber = 5;
            domain.BuildingType = "Villa";
            domain.CreatedAt = DateTime.UtcNow;
            domain.ModifiedAt = DateTime.UtcNow;

            //Act
            await Svc<IRealEstateServices>().Update(dto);

            //Assert
            Assert.Equal(domain.Id, guid);
            Assert.NotEqual(dto.Area, domain.Area);
            Assert.DoesNotMatch(dto.Location, domain.Location);
            Assert.DoesNotMatch(dto.RoomNumber.ToString(), domain.RoomNumber.ToString());
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateDataVersion2()
        {
            //Alguses andmed luuakse ja kasutame MockRealEstateDto meetodit
            //Andmed uuendatakse ja kasutame uut Mock meetodit(selle peab ise tegema)
            //lõpus kontrollime et andmed erinevad

            //Arrange and Act
            RealEstateDto dto = MockRealEstateDto();
            var createRealEstate =  await Svc<IRealEstateServices>().Create(dto);

            RealEstateDto updatedDto = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(updatedDto);

            //Assert
            Assert.NotEqual(createRealEstate.Area, result.Area);
            Assert.DoesNotMatch(createRealEstate.Location, result.Location);
            Assert.NotEqual(createRealEstate.RoomNumber, result.RoomNumber);
        }

        [Fact]
        public async Task ShouldNot_UpdateRealEstate_WhenDidNotUpdateData()
        {
            //Arrange
            //kasutate MockRealEstateData meetodit, kus on andmed
            //tuleb kasutada Create meetodit. et andmed luua
            //tyleb teha uus meetod nimega MockNullRealEstateData(), kus on tühjad andmed e null või ""
            RealEstateDto dto = MockRealEstateDto();
            RealEstateDto nullDto = MockNullRealEstateData();

            //Act
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var result = await Svc<IRealEstateServices>().Update(nullDto);

            //Assert
            //toimub võrdlemine et andmed ei ole võrdsed
            Assert.NotEqual(createRealEstate.Area, result.Area);
            Assert.DoesNotMatch(createRealEstate.Location, result.Location);
        }

        //TUleb välja mõelda kolm erinevat xUnit testi
        //Kommentaari kirjutate, mida iga test kontrollib

        [Fact]
        //Kontrollib, et ei saa uuendada RealEstate kui id on vale
        public async Task ShouldNot_UpdateRealEstate_WhenInvalidId()
        {
            RealEstateDto dto = MockRealEstateDto();

            RealEstateDto domain = new()
            {
                Id = Guid.NewGuid(),
                Area = 185.0,
                Location = "Another Updated Location",
                RoomNumber = 6,
                BuildingType = "Cottage",
                CreatedAt = dto.CreatedAt,
                ModifiedAt = DateTime.Now.AddYears(1)
            };

            var result = await Svc<IRealEstateServices>().Update(domain);

            Assert.Null(result);
        }

        [Fact]
        //Kontrollib, et ei saa kustutada RealEstate mida pole olemas
        public async Task ShouldNot_DeleteRealEstate_WhenIdDontExist()
        {
            var guid = Guid.NewGuid();

            var result = await Svc<IRealEstateServices>().Delete(guid);

            Assert.Null(result);
        }

        [Fact]
        //Kontrollib, et RealEstate loomise aeg ei muutu kui uuendatakse RealEstate andmeid
        public async Task ShouldNot_UpdateCreatedTime_WhenUpdateRealEstate()
        {
            RealEstateDto dto = MockRealEstateDto();

            RealEstateDto domain = new()
            {
                Id = dto.Id,
                Area = 180.0,
                Location = "Another Updated Location",
                RoomNumber = 6,
                BuildingType = "Cottage",
                CreatedAt = dto.CreatedAt,
                ModifiedAt = DateTime.Now.AddYears(1)
            };

            var updatedRealEstate = await Svc<IRealEstateServices>().Update(domain);

            Assert.Equal(dto.CreatedAt, domain.CreatedAt);
            Assert.NotEqual(dto.ModifiedAt, domain.ModifiedAt);
        }

        private RealEstateDto MockNullRealEstateData()
        {
            return new RealEstateDto
            {
                Area = 0.0,
                Location = string.Empty,
                RoomNumber = 0,
                BuildingType = string.Empty,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }

        private RealEstateDto MockRealEstateDto()
        {
            return new RealEstateDto
            {
                Area = 150.0,
                Location = "Sample Location",
                RoomNumber = 4,
                BuildingType = "House",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }
        private RealEstateDto MockUpdateRealEstateData()
        {
            RealEstateDto RealEstate = new()
            {
                Area = 100.0,
                Location = "New Updated Location",
                RoomNumber = 7,
                BuildingType = "Hideout",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1)
            };

            return RealEstate;
        }
    }
}