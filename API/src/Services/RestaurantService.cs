using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DAL.Entities;
using API.DAL.Repositories;

namespace API.Services
{
    public class RestaurantService
    {
        private readonly IGenericRepository<Restaurant> _restaurantRepository;

        public RestaurantService(IGenericRepository<Restaurant> restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<List<Restaurant>> GetAllAsync()
        {
            return await _restaurantRepository.GetAllAsync(q => q.OrderBy(s => s.Title));
        }

        public async Task<Restaurant> GetByIdAsync(int id)
        {
            return await _restaurantRepository.GetByIDAsync(id);
        }

        public async Task<Restaurant> CreateAsync(Restaurant restaurant)
        {
            //Employee entity = _mapper.Map<Employee>(employee);
            return await _restaurantRepository.CreateAsync(restaurant);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            await _restaurantRepository.DeleteAsync(entity);
        }

        public async Task<Restaurant> UpdateAsync(Restaurant restaurant)
        {
            //Employee entity = _mapper.Map<Employee>(employee);
            return await _restaurantRepository.UpdateAsync(restaurant);
        }
    }
}
