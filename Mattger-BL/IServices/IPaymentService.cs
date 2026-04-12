using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_DAL.Entities;

namespace Mattger_BL.IServices
{
    public interface IPaymentMethodService
    {
        IEnumerable<PaymentMethod> GetAll(string userId);
        PaymentMethod GetById(int id);
        void Create(PaymentMethod paymentMethod);
        void Delete(int id);
    }
}
