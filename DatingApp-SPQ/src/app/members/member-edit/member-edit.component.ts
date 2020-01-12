import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { User } from 'src/app/core/models/user.model';
import { ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/core/services/user.service';
import { AuthService } from 'src/app/core/services/auth.service';
import { AlertiflyService } from 'src/app/core/services/alertifly.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('form', {static : true}) form : NgForm; 
  @HostListener('window:beforeunload',['$event'])
  unloadNotification($event : any){
    if(this.form.dirty){
      $event.returnValue = true;
    }
  }
  user : User;

  constructor(private route: ActivatedRoute,
   private readonly userService : UserService,
   private readonly authService : AuthService,
   private readonly alertifly : AlertiflyService) { }

  ngOnInit() {
    this.route.data.subscribe( data =>
      this.user = data['user'])
  }

  updateUser() : void {
    this.userService.updateUser(this.authService.decodedToken.nameid,this.user).subscribe(v => {
      this.alertifly.success("User has been updated");
      this.form.reset();
    }, error => this.alertifly.error(error))
    
  }

}
