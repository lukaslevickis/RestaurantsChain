using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DAL.Entities;
using API.DAL.Repositories;
using API.Dtos;
using AutoMapper;

namespace API.Services
{
    public class RestaurantService
    {
        private readonly IGenericRepository<Restaurant> _restaurantRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public RestaurantService(IGenericRepository<Restaurant> restaurantRepository,
                                 IGenericRepository<Menu> menuRepository,
                                 IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<List<RestaurantDto>> GetAllAsync()
        {
            var restaurants = await _restaurantRepository.GetAllAsync(q => q.OrderBy(s => s.Title));
            var menus = await _menuRepository.GetAllAsync(q => q.OrderBy(s => s.Price));

            List<RestaurantDto> restaurantsDto = _mapper.Map<List<Restaurant>, List<RestaurantDto>>(restaurants);

            foreach (var restaurant in restaurantsDto)
            {
                foreach (var menu in menus)
                {
                    if (restaurant.MealId == menu.Id)
                    {
                        restaurant.MealName = menu.Title;
                        break;
                    }
                }
            }

            return restaurantsDto;
        }

        public async Task<Restaurant> GetByIdAsync(int id)
        {
            return await _restaurantRepository.GetByIDAsync(id);
        }

        public async Task<Restaurant> CreateAsync(Restaurant restaurant)
        {
            return await _restaurantRepository.CreateAsync(restaurant);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            await _restaurantRepository.DeleteAsync(entity);
        }

        public async Task<RestaurantDto> UpdateAsync(Restaurant restaurant)
        {
            var item = await _restaurantRepository.UpdateAsync(restaurant);
            RestaurantDto updatedRestaurant = _mapper.Map<RestaurantDto>(restaurant);

            var menus = await _menuRepository.GetAllAsync(q => q.OrderBy(s => s.Price));
            foreach (var menu in menus)
            {
                if (menu.Id == updatedRestaurant.MealId)
                {
                    updatedRestaurant.MealName = menu.Title;
                    break;
                }
            }

            return updatedRestaurant;
        }

        public async Task<List<RestaurantDto>> GetRestaurantsByMealAsync(int id)
        {
            List<RestaurantDto> restaurants = await GetAllAsync();
            return restaurants.Where(x => x.MealId == id).ToList();
        }
    }
}
