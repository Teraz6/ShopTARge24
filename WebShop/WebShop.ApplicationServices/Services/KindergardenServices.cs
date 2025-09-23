using Microsoft.EntityFrameworkCore;
using WebShop.Core.Domain;
using WebShop.Core.Dto;
using WebShop.Core.ServiceInterface;
using WebShop.Data;

namespace WebShop.ApplicationServices.Services
{
    public class KindergardenServices : IKindergardenServices
    {
        private readonly WebShopContext _context;

        public KindergardenServices(WebShopContext context)
        {
            _context = context;
        }

        public async Task<Kindergarden> Create(KindergardenDto dto)
        {
            Kindergarden kindergarden = new()
            {
                Id = Guid.NewGuid(),
                GroupName = dto.GroupName,
                ChildrenCount = dto.ChildrenCount,
                KindergardenName = dto.KindergardenName,
                TeacherName = dto.TeacherName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Kindergardens.Add(kindergarden);
            await _context.SaveChangesAsync();
            return kindergarden;
        }

        public async Task<Kindergarden> Update(KindergardenDto dto)
        {
            var domain = await _context.Kindergardens.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (domain == null)
            {
                return null;
            }

            domain.GroupName = dto.GroupName;
            domain.ChildrenCount = dto.ChildrenCount;
            domain.KindergardenName = dto.KindergardenName;
            domain.TeacherName = dto.TeacherName;
            domain.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return domain;
        }

        public async Task<Kindergarden> Delete(Guid id)
        {
            var result = await _context.Kindergardens
                  .FirstOrDefaultAsync(x => x.Id == id);
            _context.Kindergardens.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<Kindergarden> DetailAsync(Guid id)
        {
            return await _context.Kindergardens.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
