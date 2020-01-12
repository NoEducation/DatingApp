import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { UserLogin } from 'src/app/core/models/user-login.model';
import { AlertiflyService } from 'src/app/core/services/alertifly.service';


@Component({
  selector: 'app-register-home',
  templateUrl: './register-home.component.html',
  styleUrls: ['./register-home.component.css']
})
export class RegisterHomeComponent implements OnInit {

  user : UserLogin = new UserLogin();

  @Output("closeRegistration") closeRegistration 
  : EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(private readonly authService : AuthService,
    private readonly alertify : AlertiflyService) { }

  ngOnInit() {
  }

  close(): void{
    this.closeRegistration.emit();
  }
  registerNewUser() : void{
    this.authService.register(this.user).subscribe(response =>{
      this.alertify.success('Success ! User created')
      this.closeRegistration.emit();
    }, error => this.alertify.error(error))
  };
}
