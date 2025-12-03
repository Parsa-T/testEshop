using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Services
{
    public class ProductsGalleriseServices : IProductsGalleriseServices
    {
        IGalleriseRepository _galleriseRepository;
        string _uploadFolder;
        public ProductsGalleriseServices(IGalleriseRepository gallerise)
        {
            _galleriseRepository = gallerise;
            _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Products", "Gallerise");
            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }
        }

        public async Task AddAsync(AddGalleriseDTO dto)
        {
            string? fileName = null;

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                // نام یکتا برای فایل
                fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.ImageFile.FileName)}";
                var filePath = Path.Combine(_uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }
            }

            var entity = new _Products_Galleries
            {
                ProductsId = dto.ProductsId,
                Title = dto.Title,
                ImageName = fileName
            };

            await _galleriseRepository.AddAsync(entity);
            await _galleriseRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _galleriseRepository.GetByIdAsync(id);
            if (entity == null) return;

            // حذف فایل از پوشه
            if (!string.IsNullOrEmpty(entity.ImageName))
            {
                var filePath = Path.Combine(_uploadFolder, entity.ImageName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            await _galleriseRepository.DeleteAsync(entity);
            await _galleriseRepository.SaveChangesAsync();
        }

        public async Task<List<AddGalleriseDTO>> GetAllAsync(int productId)
        {
            var entities = await _galleriseRepository.GetAllAsync(productId);
            return entities.Select(e => new AddGalleriseDTO
            {
                Id = e.Id,
                ProductsId = e.ProductsId,
                Title = e.Title,
                ImageName = e.ImageName
            }).ToList();
        }

        public async Task<AddGalleriseDTO?> GetByIdAsync(int id)
        {
            var entity = await _galleriseRepository.GetByIdAsync(id);
            if (entity == null) return null;

            return new AddGalleriseDTO
            {
                Id = entity.Id,
                ProductsId = entity.ProductsId,
                Title = entity.Title,
                ImageName = entity.ImageName
            };
        }
    }
}
