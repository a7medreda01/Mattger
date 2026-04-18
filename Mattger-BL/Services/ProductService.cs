using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mattger_BL.Services
{
    using AutoMapper;
    using Mattger_BL.DTOs;
    using Mattger_BL.IServices;
    using Mattger_DAL.Entities;
    using Mattger_DAL.IRepos;
    using Microsoft.AspNetCore.Http;

    public class ProductService : IProductService
    {
        private readonly IGenericRepo<Product> _repo;
        private readonly IMapper mapper;

        public ProductService(IGenericRepo<Product> repo,IMapper _mapper)
        {
            _repo = repo;
            mapper = _mapper;
        }
        ////include
        //public IEnumerable<Product> GetAll()
        //{
        //    return _repo.GetAll(p=>p.ProductBrand,p=>p.ProductType);
        //}
        ////search
        //public IEnumerable<Product> GetAll(string search)
        //{
        //    return _repo.GetAll(p => p.Name.Contains(search), [p => p.ProductBrand, p => p.ProductType]);
        //}
        ////category
        //public IEnumerable<Product> GetAll(int? catId)
        //{
        //    return _repo.GetAllByCategory(p => p.ProductType.Id == catId, [p => p.ProductBrand, p => p.ProductType]);
        //}
        ////paging
       
            //public async Task<(List<Product> Items, int TotalItems)> GetProductsAsync(
            //    int page, int pageSize, int? category = null, string? search = null)
            //{
            //    IQueryable<Product> query;

            //    // Filter
            //    query = _repo.GetAll(
            //        filter: p =>
            //            (!category.HasValue || p.ProductType.Id == category.Value) &&
            //            (string.IsNullOrEmpty(search) || p.Name.Contains(search)),
            //        skip: (page - 1) * pageSize,
            //        take: pageSize,
            //        p => p.ProductBrand, p => p.ProductType // Includes
            //    );

            //    var totalItems = query.Count(); // قبل pagination لو عايز العدد الكامل
            //    var items = query.ToList();

            //    return (items, totalItems);
            //}
        
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _repo.GetProductAsync(id);
        }

        public void Add(CreateProductDTO dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Details = dto.Details,
                Price = dto.Price,
                OldPrice = dto.OldPrice,
                StockQuantity = dto.StockQuantity,
                Discount = dto.Discount,
                ProductBrandId = dto.ProductBrandId,
                ProductTypeId = dto.ProductTypeId,
                CreatedAt = DateTime.Now
            };

            // 📌 رفع الصور
            var images = UploadImages(dto.Images);

            if (images.Any())
                product.Images = images;

            _repo.Add(product);
            _repo.Save();
        }

        public void Update(int id, UpdateProductDTO dto)
        {
            var product = _repo.GetById(id);

            if (product == null)
                throw new Exception("Product not found");

            mapper.Map(dto, product); // 👈 هنا المهم

            if (dto.Images != null && dto.Images.Any())
            {
                product.Images = UploadImages(dto.Images);
            }

            _repo.Update(product);
            _repo.Save();
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
            _repo.Save();
        }
        public async Task <List<Product>> GetAllProductsAsync(ProductQueryParams param)
        {
            // 1️⃣ Get raw data from repo
            var products = await _repo.GetAllProductsAsync();

            var query = products.AsQueryable();

            // 🔍 Search
            if (!string.IsNullOrEmpty(param.Search))
            {
                var search = param.Search.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(search));
            }

            // 💰 Price range
            if (param.MinPrice.HasValue)
                query = query.Where(p => p.Price >= param.MinPrice);

            if (param.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= param.MaxPrice);

            // 📌 Status
            if (param.Status.HasValue)
                query = query.Where(p => p.Status == param.Status);

            // 🏷️ Type Id
            if (param.ProductTypeId.HasValue)
                query = query.Where(p => p.ProductTypeId == param.ProductTypeId);

            // ⭐ Rating
            if (param.Rating.HasValue)
                query = query.Where(p => p.Rating >= param.Rating);

            // 🔥 Type (flash / new / top)
            if (!string.IsNullOrEmpty(param.Type))
            {
                var now = DateTime.Now;

                switch (param.Type.ToLower())
                {
                    case "flash":
                        query = query.Where(p =>
                            p.FlashStartDate.HasValue &&
                            p.FlashEndDate.HasValue &&
                            p.FlashStartDate <= now &&
                            p.FlashEndDate >= now);
                        break;

                    case "new":
                        query = query.Where(p =>
                            p.CreatedAt >= DateTime.Now.AddDays(-7));
                        break;

                    case "top":
                        query = query.Where(p =>
                            p.SalesCount.HasValue && p.SalesCount > 50);
                        break;
                }
            }

            // 📊 Total count
            var count = query.Count();

            // 🔃 Sorting
            query = query.OrderByDescending(p => p.CreatedAt);

            // 📄 Pagination
            var data = query
                .Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize)
                .ToList();

            return data;
        }

        private List<ProductImages> UploadImages(List<IFormFile> files)
        {
            var images = new List<ProductImages>();

            if (files == null || files.Count == 0)
                return images;

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            foreach (var file in files)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                images.Add(new ProductImages
                {
                    ImageUrl = fileName
                });
            }

            return images;
        }
    }
}
