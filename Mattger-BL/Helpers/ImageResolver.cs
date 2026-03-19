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

    public class ImageUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly ApiSettings _settings;

        public ImageUrlResolver(IOptions<ApiSettings> options)
        {
            _settings = options.Value;
        }

        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return null;

            return $"{_settings.BaseUrl}images/{source.PictureUrl}";
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
            if (source.Product == null || string.IsNullOrEmpty(source.Product.PictureUrl))
                return null;
            return $"{_settings.BaseUrl}images/{source.Product.PictureUrl}";

        }

    }
}
