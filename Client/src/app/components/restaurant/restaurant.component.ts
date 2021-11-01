import { Component, OnInit } from '@angular/core';
import { Restaurant } from 'src/app/models/restaurant';
import { RestaurantService } from 'src/app/services/restaurant.service';
import { FormBuilder } from '@angular/forms';
import { RestaurantMenuService } from 'src/app/services/restaurant-menu.service';
import { RestaurantMenu } from 'src/app/models/restaurant-menu';

@Component({
  selector: 'app-restaurant',
  templateUrl: './restaurant.component.html',
  styleUrls: ['./restaurant.component.css']
})
export class RestaurantComponent implements OnInit {
  private restaurantService: RestaurantService;
  private restaurantMenuService: RestaurantMenuService;
  public restaurants: Restaurant[] = [];
  public meals: RestaurantMenu[] = [];
  update: boolean = false;
  mealId: number = null as any;
  employees: number = null as any;
  restaurantId: number = null as any;
  customers: number = null as any;
  title: string = '';

  postData = {} as Restaurant;
  restaurantForm = this.formBuilder.group({
    title: '',
    customers: null,
    employees: null,
    mealId: null
  });

  constructor(restaurantService: RestaurantService, restaurantMenuService: RestaurantMenuService, private formBuilder: FormBuilder) { 
    this.restaurantService = restaurantService;
    this.restaurantMenuService = restaurantMenuService;
   }

  ngOnInit(): void {
    this.restaurantMenuService.getMeals().subscribe((mealsFromApi) => {
      this.meals = mealsFromApi.sort((a, b) => (a.title > b.title) ? 1 : -1);
    });

    this.restaurantService.getRestaurants().subscribe((restaurantsFromApi) => {
      this.restaurants = restaurantsFromApi;
      this.restaurants.forEach(restaurant => {
        let index = this.restaurants.map(e => e.id).indexOf(restaurant.id);
        this.assignMealName(index);
      });
    });
  }

  addRestaurant(): void {
    this.postData.id = this.restaurantForm.value.id;
    this.postData.title = this.restaurantForm.value.title;
    this.postData.customers = this.restaurantForm.value.customers;
    this.postData.employees = this.restaurantForm.value.employees;
    this.postData.mealId = this.restaurantForm.value.mealId;
    this.restaurantService.addRestaurant(this.postData).subscribe(newRestaurant => {
      this.restaurants.push(newRestaurant);
      let index: number = this.restaurants.map(e => e.id).indexOf(newRestaurant.id);
      this.assignMealName(index);
      this.resetFormValues();
    });
  }

  deleteRestaurant(id: number): void {
    this.restaurantService.deleteRestaurant(id).subscribe(() => {
      this.restaurants = this.restaurants.filter(x => x.id != id);
    });
  }

  loadValuesToForm(restaurant: Restaurant): void {
    this.restaurantId = restaurant.id;
    this.title = restaurant.title;
    this.customers = restaurant.customers;
    this.employees = restaurant.employees;
    this.mealId = restaurant.mealId;
    this.update = true;
  }

  updateRestaurant(): void {
    this.postData.id = this.restaurantId;
    this.postData.title = this.title;
    this.postData.customers = this.customers;
    this.postData.employees = this.employees;
    this.postData.mealId = this.mealId;
    this.restaurantService.updateRestaurant(this.postData).subscribe(updatedRestaurant => {
      let index = this.restaurants.map(e => e.id).indexOf(updatedRestaurant.id);
      this.restaurants[index] = updatedRestaurant;
      this.assignMealName(index);
      this.resetFormValues();
    });
  }

  resetFormValues(): void {
    this.restaurantId = null as any;
    this.title = '';
    this.customers = null as any;
    this.employees = null as any;
    this.mealId = null as any;
    this.update = false;
  }

  assignMealName(restaurantIndex: number): void {
    this.meals.forEach(meal => {
      if(meal.id == this.restaurants[restaurantIndex].mealId) {
        this.restaurants[restaurantIndex].mealName = meal.title;
      }
    });
  }

}
