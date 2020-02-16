import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserLogin } from '../models/user-login.model';
import {map} from 'rxjs/operators';
import { Observable, BehaviorSubject } from 'rxjs';
import {JwtHelperService} from '@auth0/angular-jwt'
import { environment } from 'src/environments/environment';
import { User } from '../models/user.model';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

private url : string= environment.apiUrl + 'Auth/';
jwtHelper = new JwtHelperService()
decodedToken : any;
currentUser : User
photoUrl = new BehaviorSubject<string>('../../../assets')
currentPhotoUrl = this.photoUrl.asObservable();

constructor(private readonly http: HttpClient) { }

changeMemberPhoto(photoUrl : string){
  this.photoUrl.next(photoUrl);
}

  public login(user: any): Observable<any> {
    return this.http.post(this.url + 'Login', user).pipe(
      map((response: any) => {
        const userToken: any = response;
        if (userToken) {
          localStorage.setItem('token', userToken.token);
          localStorage.setItem('user',JSON.stringify(response.user))
          this.decodedToken = this.jwtHelper.decodeToken(userToken.token);
          this.currentUser = response.user;
          this.changeMemberPhoto(this.currentUser.photoUrl);
        }
      })
    );}

    public register(user: UserLogin) : Observable<any> {
      return this.http.post(this.url +  'Register', user);
    }

    public loggedIn() : boolean{
      const token = localStorage.getItem('token');
      return !this.jwtHelper.isTokenExpired(token);
    }
}
