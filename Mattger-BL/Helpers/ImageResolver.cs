using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mattger_BL.Helpers
{
    using System.Runtime;
    using AutoMapper;
    using Mattger_BL.DTOs;
    using Mattger_DAL.Entities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

public class ProductImagesUrlResolver 
    : IValueResolver<Product, ProductDTO, List<string>>
{
    private readonly ApiSettings _settings;

    public ProductImagesUrlResolver(IOptions<ApiSettings> options)
    {
        _settings = options.Value;
    }

    public List<string> Resolve(Product source, ProductDTO destination, List<string> destMember, ResolutionContext context)
    {
        if (source.Images == null || !source.Images.Any())
            return new List<string>();

        return source.Images
            .Where(i => !string.IsNullOrEmpty(i.ImageUrl))
            .Select(i => $"{_settings.BaseUrl}images/products/{i.ImageUrl}")
            .ToList();
    }
}

    public class UpdateProductImagesUrlResolver
        : IValueResolver<Product, UpdateProductDTO, List<string>>
    {
        private readonly ApiSettings _settings;

        public UpdateProductImagesUrlResolver(IOptions<ApiSettings> options)
        {
            _settings = options.Value;
        }

        public List<string> Resolve(Product source, UpdateProductDTO destination, List<string> destMember, ResolutionContext context)
        {
            if (source.Images == null || !source.Images.Any())
                return new List<string>();

            return source.Images
                .Where(i => !string.IsNullOrEmpty(i.ImageUrl))
                .Select(i => $"{_settings.BaseUrl}images/products/{i.ImageUrl}")
                .ToList();
        }
    }
    public class CartItemImageUrlResolver : IValueResolver<CartItem, CartItemDTO, string>
    {
        private readonly ApiSettings _settings;

        public CartItemImageUrlResolver(IOptions<ApiSettings> options)
        {
            _settings = options.Value;

        }

        public string Resolve(CartItem source, CartItemDTO destination, string destMember, ResolutionContext context)
        {
            if (source.Product == null || source.Product.Images == null || !source.Product.Images.Any())
                return null;

            var firstImage = source.Product.Images.FirstOrDefault();

            if (firstImage == null)
                return null;

            return $"{_settings.BaseUrl}images/products/{firstImage.ImageUrl}";
        }

    }
}
