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

        //TODO:
        //ShouldNot_GetByIdRealEstate_WhenReturnsNotEqual()
        //Should_GetByIdRealEstate_WhenReturnsEqual()
        //Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        //ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()

        public async Task ShouldNot_GetByIdRealEstate_WhenReturnsNotEqual()
        {
            //Arrange
            RealEstateDto dto = new() //TODO: Search by id, not add
            {
                Area = 85.0,
                Location = "Another Location",
                RoomNumber = 2,
                BuildingType = "House",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            //Act
            var fetchedRealEstate = await Svc<IRealEstateServices>().GetById(createdRealEstate.Id);
        }

        public async Task Should_GetByIdRealEstate_WhenReturnsEqual()
        {
            //Arrange
            RealEstateDto dto = new() //TODO: Search by id, not add
            {
                Area = 95.0,
                Location = "Sample Location",
                RoomNumber = 4,
                BuildingType = "Condo",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            //Act
            var fetchedRealEstate = await Svc<IRealEstateServices>().GetById(createdRealEstate.Id);
        }

        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        {
            //Arrange
            RealEstateDto dto = new() //TODO: Search by id, not add
            {
                Area = 110.0,
                Location = "Delete Location",
                RoomNumber = 3,
                BuildingType = "Townhouse",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            //Act
            await Svc<IRealEstateServices>().Delete(createdRealEstate.Id);

        }

        public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
            //Arrange
            RealEstateDto dto = new() //TODO: Search by id, not add
            {
                Area = 130.0,
                Location = "Non-Delete Location",
                RoomNumber = 5,
                BuildingType = "Villa",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            //Act
            await Svc<IRealEstateServices>().Delete(createdRealEstate.Id);
        }
    }
}