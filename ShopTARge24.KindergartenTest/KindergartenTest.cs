using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using System.Threading.Tasks;


namespace ShopTARge24.KindergartenTest
{
    public class KindergartenTest : TestBase
    {
        // 1) Test, et Kindergarten luuakse ja tagastab objekti
        [Fact]
        public async Task Should_CreateKindergarten_WithValidData()
        {
            var dto = MockKindergartenDto();
            var result = await Svc<IKindergartenServices>().Create(dto);

            Assert.NotNull(result);
            Assert.Equal("Test Lasteaed", result.KindergartenName);
            Assert.Equal(50, result.ChildrenCount);
        }

        // 2) Test, et vigase andme sisestamisel visatakse error
        [Fact]
        public async Task ShouldNot_CreateKindergarten_WhenInvalidData()
        {
            var dto = new KindergartenDto
            {
                Id = Guid.NewGuid(),
                KindergartenName = null, // vigane
                GroupName = null,
                TeacherName = "Õpetaja",
                ChildrenCount = -5,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await Assert.ThrowsAsync<DbUpdateException>(() => Svc<IKindergartenServices>().Create(dto));
        }

        // 3) Test, et Kindergarteni saab uuendada
        [Fact]
        public async Task Should_UpdateKindergarten_WhenUpdated()
        {
            var dto = MockKindergartenDto();
            var createdDto = await Svc<IKindergartenServices>().Create(dto);

            var updatedDto = new KindergartenDto
            {
                Id = createdDto.Id,
                KindergartenName = "Maisi lasteaed", 
                GroupName = "Lepatriinu",
                TeacherName = "Deisi",
                ChildrenCount = 134,
                CreatedAt = createdDto.CreatedAt,
                UpdatedAt = DateTime.UtcNow
            };

            var updated = await Svc<IKindergartenServices>().Update(updatedDto);

            Assert.NotEqual(dto.ChildrenCount, updated.ChildrenCount);
            Assert.NotEqual(dto.TeacherName, updated.TeacherName);
            Assert.NotEqual(dto.KindergartenName, updated.KindergartenName);
            Assert.NotEqual(dto.GroupName, updated.GroupName);
        }

        // 4) Test, et saab saada Kindergarteni detailid
        [Fact]
        public async Task Should_ReturnKindergartenDetails()
        {
            var dto = MockKindergartenDto();
            var created = await Svc<IKindergartenServices>().Create(dto);

            var details = await Svc<IKindergartenServices>().DetailAsync(created.Id);

            Assert.NotNull(details);
            Assert.Equal("Test Lasteaed", details.KindergartenName);
        }

        // 5) Test, et Kindergarteni saab kustutada
        [Fact]
        public async Task Should_DeleteKindergarten()
        {
            var dto = MockKindergartenDto();
            var created = await Svc<IKindergartenServices>().Create(dto);

            var deleted = await Svc<IKindergartenServices>().Delete(created.Id);

            Assert.NotNull(deleted);
            var check = await Svc<IKindergartenServices>().DetailAsync(created.Id);
            Assert.Null(check);
        }

        [Fact]
        // Kontrollib, et kustutatud Kindergarten ei ole leitav
        public async Task Should_ReturnNull_WhenReadingDeletedKindergarten()
        {
            var dto = MockKindergartenDto();

            var created = await Svc<IKindergartenServices>().Create(dto);

            await Svc<IKindergartenServices>().Delete((Guid)created.Id);

            var result = await Svc<IKindergartenServices>().DetailAsync((Guid)created.Id);
            Assert.Null(result);
        }

        // Mock DTO meetod
        private KindergartenDto MockKindergartenDto()
        {
            return new KindergartenDto
            {
                Id = Guid.NewGuid(),
                KindergartenName = "Test Lasteaed",
                GroupName = "Test Grupp",
                TeacherName = "Õpetaja",
                ChildrenCount = 50,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}
