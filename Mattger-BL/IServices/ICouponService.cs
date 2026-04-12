using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_BL.DTOs;
using Mattger_DAL.Entities;

namespace Mattger_BL.IServices
{
    public interface ICouponService
    {
        IEnumerable<Coupon> GetAll();
        Coupon GetById(int id);
        Coupon GetByCode(string code);
        void Create(Coupon coupon);
        void Update(Coupon coupon);
        void Delete(int id);

        bool IsValid(string code);
        ApplyCouponResultDto ApplyCoupon(ApplyCouponDto dto);
    }
}
