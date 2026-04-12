using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_BL.DTOs;
using Mattger_BL.IServices;
using Mattger_DAL.Entities;
using Mattger_DAL.Entities.Enums;
using Mattger_DAL.IRepos;

namespace Mattger_BL.Services
{
    public class CouponService : ICouponService
    {
        private readonly IGenericRepo<Coupon> _repo;

        public CouponService(IGenericRepo<Coupon> repo)
        {
            _repo = repo;
        }

        public IEnumerable<Coupon> GetAll()
        {
            return _repo.GetAll().ToList();
        }

        public Coupon GetById(int id)
        {
            return _repo.GetById(id);
        }

        public Coupon GetByCode(string code)
        {
            return _repo.GetAll()
                .FirstOrDefault(c => c.Code == code);
        }

        public void Create(Coupon coupon)
        {
            _repo.Add(coupon);
            _repo.Save();
        }

        public void Update(Coupon coupon)
        {
            _repo.Update(coupon);
            _repo.Save();
        }

        public void Delete(int id)
        {
            var coupon = _repo.GetById(id);
            if (coupon == null) return;

            _repo.Delete(coupon.Id);
            _repo.Save();
        }

        public bool IsValid(string code)
        {
            var coupon = GetByCode(code);

            if (coupon == null)
                return false;

            var now = DateTime.Now;

            return (coupon.StartDate == null || coupon.StartDate <= now)
                && (coupon.EndDate == null || coupon.EndDate >= now);
        }

        public ApplyCouponResultDto ApplyCoupon(ApplyCouponDto dto)
        {
            var coupon = GetByCode(dto.Code);

            if (coupon == null)
            {
                return new ApplyCouponResultDto
                {
                    IsValid = false,
                    Message = "Invalid coupon code",
                    SubTotal = dto.SubTotal,
                    ShippingCost = dto.ShippingCost,
                    FinalTotal = dto.SubTotal + dto.ShippingCost
                };
            }

            var now = DateTime.Now;

            if (coupon.StartDate != null && coupon.StartDate > now ||
                coupon.EndDate != null && coupon.EndDate < now)
            {
                return new ApplyCouponResultDto
                {
                    IsValid = false,
                    Message = "Coupon expired or not active",
                    SubTotal = dto.SubTotal,
                    ShippingCost = dto.ShippingCost,
                    FinalTotal = dto.SubTotal + dto.ShippingCost
                };
            }

            decimal discount = 0;
            decimal shipping = dto.ShippingCost;

            switch (coupon.DiscountType)
            {
                // 🔥 Percent discount
                case DiscountType.Precent:
                    discount = (dto.SubTotal * coupon.Discount) / 100;
                    break;

                // 🔥 Fixed value discount
                case DiscountType.Value:
                    discount = coupon.Discount;
                    break;

                // 🔥 Free delivery
                case DiscountType.FreeDelivary:
                    shipping = 0;
                    discount = 0;
                    break;
            }

            var finalTotal = dto.SubTotal + shipping - discount;

            if (finalTotal < 0)
                finalTotal = 0;

            return new ApplyCouponResultDto
            {
                IsValid = true,
                SubTotal = dto.SubTotal,
                ShippingCost = shipping,
                Discount = discount,
                FinalTotal = finalTotal,
                Message = "Coupon applied successfully"
            };
        }
    }
}