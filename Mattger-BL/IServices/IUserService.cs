using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_BL.DTOs;
using Mattger_DAL.Entities;

namespace Mattger_BL.IServices
{
    public interface IUserService
    {
        IEnumerable<AppUser> GetAll();

        AppUser GetById(int id);

        void Add(AppUser appUser);

        void Update(AppUser appUser);

        void Delete(int id);
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO user);
        Task<AuthResponseDTO> LoginAsync(LoginDTO dto);
    }
}
