﻿using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain; 
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Shop.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly ShopContext _context;
        private readonly IFIleServices _fileServices;

        //makeing constructor
        public KindergartenServices
            (
                ShopContext context,
,               IFileServices fileServices
            )
        {
            _context = context;
            _fileServices = fileServices;
        }
        public async Task<Kindergarten> Create(KindergartenDto dto)
        {
            Kindergarten kindergarten = new Kindergarten();
            kindergarten.Id = Guid.NewGuid();
            kindergarten.GroupName = dto.GroupName;
            kindergarten.ChidlrenCount = dto.ChidlrenCount;
            kindergarten.KindergartenName = dto.KindergartenName;
            kindergarten.TeacherName = dto.TeacherName;
            kindergarten.CreatedAt = DateTime.Now;
            kindergarten.UpdatedAt = DateTime.Now;

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, kindergarten);
            }


            await _context.Kindergarten.AddAsync(kindergarten);
            await _context.SaveChangesAsync();

            return kindergarten;
        }

        public async Task<Kindergarten> DetailAsync(Guid id)
        {
            var result = await _context.Kindergarten
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Kindergarten> Update(KindergartenDto dto)
        {
            // Fetch the existing entity from the database
            var kindergarten = await _context.Kindergarten.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (kindergarten == null) return null;

            // Update only the editable fields
            kindergarten.GroupName = dto.GroupName;
            kindergarten.ChidlrenCount = dto.ChidlrenCount;
            kindergarten.KindergartenName = dto.KindergartenName;
            kindergarten.TeacherName = dto.TeacherName;
            kindergarten.UpdatedAt = DateTime.UtcNow;

            _context.Kindergarten.Update(kindergarten);

            await _context.SaveChangesAsync();
            return kindergarten;
        }


        public async Task<Kindergarten> Delete(Guid id)
        {
            var result = await _context.Kindergarten
                 .FirstOrDefaultAsync(x => x.Id == id);

            _context.Kindergarten.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}