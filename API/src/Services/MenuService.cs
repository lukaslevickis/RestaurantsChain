using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DAL.Entities;
using API.DAL.Repositories;

namespace API.Services
{
    public class MenuService
    {
        private readonly IGenericRepository<Menu> _menuRepository;

        public MenuService(IGenericRepository<Menu> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<List<Menu>> GetAllAsync()
        {
            return await _menuRepository.GetAllAsync(q => q.OrderBy(s => s.Price));
        }

        public async Task<Menu> GetByIdAsync(int id)
        {
            return await _menuRepository.GetByIDAsync(id);
        }

        public async Task<Menu> CreateAsync(Menu menu)
        {
            return await _menuRepository.CreateAsync(menu);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            await _menuRepository.DeleteAsync(entity);
        }

        public async Task<Menu> UpdateAsync(Menu menu)
        {
            return await _menuRepository.UpdateAsync(menu);
        }
    }
}
