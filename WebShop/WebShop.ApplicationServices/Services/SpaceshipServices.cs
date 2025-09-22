using Microsoft.EntityFrameworkCore;
using WebShop.Core.Domain;
using WebShop.Core.Dto;
using WebShop.Core.ServiceInterface;
using WebShop.Data;
using WebShop.Core.Domain;
using WebShop.Core.Dto;
using WebShop.Core.ServiceInterface;


namespace WebShop.ApplicationServices.Services
{
    public class SpaceshipServices : ISpaceshipServices
    {
        private readonly WebShopContext _context;

        public SpaceshipServices
            (
                WebShopContext context
            )
        {
            _context = context;
        }

        public async Task<Spaceships> Create(SpaceshipDto dto)
        {
            Spaceships spaceships = new Spaceships();

            spaceships.Id = Guid.NewGuid();
            spaceships.Name = dto.Name;
            spaceships.Classification = dto.Classification;
            spaceships.BuiltDate = dto.BuiltDate;
            spaceships.Crew = dto.Crew;
            spaceships.EnginePower = dto.EnginePower;
            spaceships.CreatedAt = DateTime.Now;
            spaceships.ModifiedAt = DateTime.Now;

            await _context.Spaceships.AddAsync(spaceships);
            await _context.SaveChangesAsync();

            return spaceships;
        }

        public async Task<Spaceships> Update(SpaceshipDto dto)
        {
            //vaja leida doamini objekt, mida saaks mappida dto-ga
            Spaceships spaceships = new Spaceships();

            spaceships.Id = dto.Id;
            spaceships.Name = dto.Name;
            spaceships.Classification = dto.Classification;
            spaceships.BuiltDate = dto.BuiltDate;
            spaceships.Crew = dto.Crew;
            spaceships.EnginePower = dto.EnginePower;
            spaceships.CreatedAt = dto.CreatedAt;
            spaceships.ModifiedAt = DateTime.Now;

            //tuleb db-s teha andmete uuendamine jauue oleku salvestamine
            _context.Spaceships.Update(spaceships);
            await _context.SaveChangesAsync();

            return spaceships;
        }

        public async Task<Spaceships> DetailAsync(Guid id)
        {
            var result = await _context.Spaceships
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Spaceships> Delete(Guid id)
        {
            //leida ülesse konkreetne soovitud rida, mida soovite kustutada
            var result = await _context.Spaceships
                .FirstOrDefaultAsync(x => x.Id == id);


            //kui rida on leitud, siis eemaldage andmebaasist
            _context.Spaceships.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }

    }
}