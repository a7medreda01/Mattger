using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_BL.DTOs;
using Mattger_BL.IServices;
using Mattger_DAL.Entities;
using Mattger_DAL.IRepos;
using Microsoft.AspNetCore.Identity;

namespace Mattger_BL.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepo<AppUser> _repo;
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtService _jwtService;

        public UserService(IGenericRepo<AppUser> repo,UserManager<AppUser>userManager, JwtService jwtService)
        {
            _repo = repo;
            this._userManager = userManager;
            this._jwtService = jwtService;
        }

        public IEnumerable<AppUser> GetAll()
        {
            return _repo.GetAll();
        }

        public AppUser GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void Add(AppUser appUser)
        {
            _repo.Add(appUser);
            _repo.Save();
        }

        public void Update(AppUser appUser)
        {
            _repo.Update(appUser);
            _repo.Save();
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
            _repo.Save();
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new Exception("User already exists");

            var user = new AppUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                throw new Exception("Registration failed");
            var token = await _jwtService.GenerateToken(user);

            return new AuthResponseDTO
            {
                UserId = user.Id,
                FullName = user.UserName,        // أو FullName لو عندك
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Token = token
            };
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                throw new Exception("Invalid credentials");

            var isValid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!isValid)
                throw new Exception("Invalid credentials");

            var token = await _jwtService.GenerateToken(user);

            return new AuthResponseDTO
            {
                UserId = user.Id,
                FullName = user.UserName,        // أو FullName لو عندك
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Token = token
            };
        }
    }
}
