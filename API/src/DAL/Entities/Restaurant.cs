using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DAL.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Customers { get; set; }
        public int Employees { get; set; }

        [ForeignKey("Menu")]
        public int? MealId { get; set; }
        public Menu Menu { get; set; }
    }
}
