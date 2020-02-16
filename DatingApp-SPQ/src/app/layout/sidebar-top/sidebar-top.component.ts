import { Component, OnInit } from '@angular/core';
import { UserLogin } from 'src/app/core/models/user-login.model';
import { AuthService } from 'src/app/core/services/auth.service';
import { AlertiflyService } from 'src/app/core/services/alertifly.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar-top',
  templateUrl: './sidebar-top.component.html',
  styleUrls: ['./sidebar-top.component.css']
})
export class SidebarTopComponent implements OnInit {

  user: UserLogin = new UserLogin();
  photoUrl : string;

  constructor(public readonly authService: AuthService,
    private readonly alertify : AlertiflyService,
    private readonly router : Router ) { }

  ngOnInit() {
    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
  }

  login(): void {
    this.authService.login(this.user).subscribe(x =>{
     this.alertify.success('logged in successfully')
    },error =>{
      console.log(error);
      this.alertify.error(error.type);
    });
  }

  checkUserLogIn(): boolean{
    return this.authService.loggedIn();
  }

  editProfile() : void {
    this.router.navigateByUrl(`/members/edit`)
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.alertify.message('logged out');
    this.authService.currentUser = null;
    this.authService.decodedToken = null;
    this.router.navigateByUrl("/messages");
  }
}
