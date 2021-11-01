using System;
namespace API.Dtos
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Customers { get; set; }
        public int Employees { get; set; }
        public int? MealId { get; set; }
        public string MealName { get; set; }
    }
}
