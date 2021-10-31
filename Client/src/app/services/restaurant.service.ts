import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Restaurant } from '../models/restaurant';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
   }

  public getRestaurants(): Observable<Restaurant[]> {
    return this.http.get<Restaurant[]>("https://localhost:5002/api/restaurant");
  }

  public addRestaurant(restaurant: Restaurant): Observable<Restaurant> {
    return this.http.post<Restaurant>("https://localhost:5002/api/restaurant", restaurant);
  }

  public deleteRestaurant(id:number): Observable<unknown> {
    return this.http.delete(`${"https://localhost:5002/api/restaurant"}/${id}`);
  }

  public updateRestaurant(restaurant: Restaurant): Observable<Restaurant> {
    return this.http.put<Restaurant>(`${"https://localhost:5002/api/restaurant"}/${restaurant.id}`, restaurant);
  }
}