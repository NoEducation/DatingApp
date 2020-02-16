import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { UserLogin } from 'src/app/core/models/user-login.model';
import { AlertiflyService } from 'src/app/core/services/alertifly.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';


@Component({
  selector: 'app-register-home',
  templateUrl: './register-home.component.html',
  styleUrls: ['./register-home.component.css']
})
export class RegisterHomeComponent implements OnInit {

  user : UserLogin = new UserLogin();

  @Output("closeRegistration") closeRegistration 
  : EventEmitter<boolean> = new EventEmitter<boolean>();

  registerForm : FormGroup

  constructor(private readonly authService : AuthService,
    private readonly alertify : AlertiflyService,
    private readonly formBuilder : FormBuilder) {
     }

  ngOnInit() {
    this.createRegisterForm();
  }

  private createRegisterForm(){
    this.registerForm = this.formBuilder.group({
      "username" : ['', Validators.compose([Validators.required,Validators.maxLength(10),Validators.minLength(2)])],
      "password" : ['', [Validators.maxLength(10),Validators.required]],
      "confirmPassword" : ['',[Validators.maxLength(10),Validators.required]]
    }, this.validatorPasswordsMatch)
  }

  close(): void{
    this.closeRegistration.emit();
  }
  registerNewUser() : void{
    // this.authService.register(this.user).subscribe(response =>{
    //   this.alertify.success('Success ! User created')
    //   this.closeRegistration.emit();
    // }, error => this.alertify.error(error))
  };

  private validatorPasswordsMatch( group : FormGroup){
    return group.get("username").value === group.get("password") ? null : {  "mismach" : true}
  }

}
