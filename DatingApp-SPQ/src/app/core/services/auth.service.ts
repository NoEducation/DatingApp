import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserLogin } from '../models/user-login.model';
import {map} from 'rxjs/operators';
import { Observable } from 'rxjs';
import {JwtHelperService} from '@auth0/angular-jwt'
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

private url : string= environment.apiUrl + 'Auth/';
jwtHelper = new JwtHelperService()
decodedToken : any;

constructor(private readonly http: HttpClient) { }

  public login(user: UserLogin): Observable<any> {
    return this.http.post(this.url + 'Login', user).pipe(
      map((response: any) => {
        const userToken: any = response;
        if (userToken) {
          localStorage.setItem('token', userToken.token);
          this.decodedToken = this.jwtHelper.decodeToken(userToken.token);
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
