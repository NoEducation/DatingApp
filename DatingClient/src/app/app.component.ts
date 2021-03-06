import { Component, OnInit } from '@angular/core';
import { AuthService } from './core/services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from './core/models/user.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  
  title = 'DatingApp-SPQ';
  jwtHelper = new JwtHelperService();

  constructor(private readonly authService : AuthService){
  }

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    const user : User =  JSON.parse(localStorage.getItem('user'));

    if(token){
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
    if(user){
      this.authService.currentUser = user;
      this.authService.changeMemberPhoto(user.photoUrl);
    }
  }
}
