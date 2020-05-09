import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  
  private baseUrl : string = environment.apiUrl + 'User/';

  constructor(private readonly http: HttpClient) { }

  getUsers() : Observable<User[]>{
    return this.http.get<User[]>(this.baseUrl + 'AllUsers');
  }

  getUserById(userId : number) : Observable<User>{
    return this.http.get<User>(this.baseUrl + 'UserById/' + userId);
  } 

  updateUser(userId : number , user : User) {
    return this.http.post(this.baseUrl + "UpdateUser/" + userId, user);
  }
  setMainPhoto(userId : number, id: number){
    return this.http.post(this.baseUrl + userId + "photos" + id + "isMain", {});
  }
}
