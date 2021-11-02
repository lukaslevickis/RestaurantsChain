import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { RestaurantMenu } from 'src/app/models/restaurant-menu';
import { RestaurantMenuService } from 'src/app/services/restaurant-menu.service';

@Component({
  selector: 'app-restaurant-menu',
  templateUrl: './restaurant-menu.component.html',
  styleUrls: ['./restaurant-menu.component.css']
})
export class RestaurantMenuComponent implements OnInit {
  @Output() onMenuUpdated = new EventEmitter();
  private restaurantMenuService: RestaurantMenuService;
  meals: RestaurantMenu[];
  update: boolean = false;
  mealId: number = null as any;
  title: string = '';
  price: number = null as any;
  weight: number = null as any;
  meat: number = null as any;
  about: string = '';

  postData = {} as RestaurantMenu;
  mealForm = this.formBuilder.group({
    title: '',
    price: null,
    weight: null,
    meat: '',
    about: null
  });

  constructor(restaurantMenuService: RestaurantMenuService, private formBuilder: FormBuilder) { 
    this.restaurantMenuService = restaurantMenuService;
   }

  ngOnInit(): void {
    this.restaurantMenuService.getMeals().subscribe((mealsFromApi) =>{
      this.meals = mealsFromApi;
    })
  }

  addMeal(): void {
    this.postData.id = this.mealForm.value.id;
    this.postData.title = this.mealForm.value.title;
    this.postData.price = this.mealForm.value.price;
    this.postData.weight = this.mealForm.value.weight;
    this.postData.meat = this.mealForm.value.meat;
    this.postData.about = this.mealForm.value.about;
    console.log(this.postData)
    this.restaurantMenuService.addMeal(this.postData).subscribe(newMeal => {
      this.meals.push(newMeal);
      
      this.resetFormValues();
      this.onMenuUpdated.emit("");
    });
  }

  deleteMeal(id: number): void {
    this.restaurantMenuService.deleteMeal(id).subscribe(() => {
      this.meals = this.meals.filter(x => x.id != id);
    });
  }

  loadValuesToForm(meal: RestaurantMenu): void {
    this.mealId = meal.id;
    this.title = meal.title;
    this.price = meal.price;
    this.weight = meal.weight;
    this.meat = meal.meat;
    this.about = meal.about;
    this.update = true;
  }

  updateMeal(): void {
    this.postData.id = this.mealId;
    this.postData.title = this.title;
    this.postData.price = this.price;
    this.postData.weight = this.weight;
    this.postData.meat = this.meat;
    this.postData.about = this.about;
    this.restaurantMenuService.updateMeal(this.postData).subscribe(updatedMeal => {
      let index = this.meals.map(e => e.id).indexOf(updatedMeal.id);
      this.meals[index] = updatedMeal;
      this.meals.sort((a, b) => (a.price > b.price) ? 1 : -1);
      this.resetFormValues();
    });
  }

  resetFormValues(): void {
    this.mealId = null as any;
    this.title = '';
    this.price = null as any;
    this.weight = null as any;
    this.meat = null as any;
    this.about = '';
    this.update = false;
  }

}
