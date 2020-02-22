import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { UserLogin } from 'src/app/core/models/user-login.model';
import { AlertiflyService } from 'src/app/core/services/alertifly.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { UserToRegister } from 'src/app/core/models/user-to-register.model';
import { Router } from '@angular/router';


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
    private readonly formBuilder : FormBuilder,
    private readonly router : Router) {
     }

  ngOnInit() {
    this.createRegisterForm();
  }

  private createRegisterForm(){
    this.registerForm = this.formBuilder.group({
      "username" : ['', Validators.compose([Validators.required,Validators.maxLength(10),Validators.minLength(2)])],
      "gender" : ['male'],
      "knownAs" : ['', Validators.required],
      "dateOfBirth" : [null, Validators.required],
      "city" : ['', Validators.required],
      "country" : ['', Validators.required],
      "password" : ['', [Validators.maxLength(10),Validators.required]],
      "confirmPassword" : ['',[Validators.maxLength(10),Validators.required]]
    }, {validators : this.validatorPasswordsMatch})
  }

  close(): void{
    this.closeRegistration.emit();
  }
  registerNewUser() : void{
    let userToRegister = new UserToRegister(
        this.registerForm.get('username').value,
        this.registerForm.get('password').value,
        this.registerForm.get('confirmPassword').value,
        this.registerForm.get('gender').value,
        this.registerForm.get('dateOfBirth').value,
        this.registerForm.get('city').value,
        this.registerForm.get('knownAs').value,

    )

    this.authService.register(userToRegister).subscribe(response => {
      this.alertify.success("User has been registered");
      this.router.navigateByUrl("home");
    },error => this.alertify.error("Error occured during register"))
  };

  private validatorPasswordsMatch( group : FormGroup){
    return group.get("confirmPassword").value === group.get("password").value ? null : { "mismach" : true}
  }

}
