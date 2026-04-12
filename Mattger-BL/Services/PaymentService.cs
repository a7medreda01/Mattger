using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_BL.IServices;
using Mattger_BL.Services;
using Mattger_DAL.Entities;
using Mattger_DAL.IRepos;

namespace Mattger_BL.Services
{

    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IGenericRepo<PaymentMethod> _repo;

    public PaymentMethodService(IGenericRepo<PaymentMethod> repo)
    {
        _repo = repo;
    }

    public IEnumerable<PaymentMethod> GetAll(string userId)
    {
        return _repo.GetAll()
            .Where(x => x.UserId == userId)
            .ToList();
    }

    public PaymentMethod GetById(int id)
    {
        return _repo.GetById(id);
    }

    public void Create(PaymentMethod paymentMethod)
    {
        _repo.Add(paymentMethod);
        _repo.Save();
    }


    public void Delete(int id)
    {
        var entity = _repo.GetById(id);
        if (entity == null) return;

        _repo.Delete(entity.Id);
        _repo.Save();
    }
}
}