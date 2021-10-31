import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RestaurantMenu } from '../models/restaurant-menu';

@Injectable({
  providedIn: 'root'
})
export class RestaurantMenuService {
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
   }

  public getMeals(): Observable<RestaurantMenu[]> {
    return this.http.get<RestaurantMenu[]>("https://localhost:5002/api/menu");
  }

  public addMeal(menu: RestaurantMenu): Observable<RestaurantMenu> {
    return this.http.post<RestaurantMenu>("https://localhost:5002/api/menu", menu);
  }

  public deleteMeal(id:number): Observable<unknown> {
    return this.http.delete(`${"https://localhost:5002/api/menu"}/${id}`);
  }

  public updateMeal(menu: RestaurantMenu): Observable<RestaurantMenu> {
    return this.http.put<RestaurantMenu>(`${"https://localhost:5002/api/menu"}/${menu.id}`, menu);
  }
}