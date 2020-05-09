import { Component, OnInit } from '@angular/core';
import { UserService } from '../core/services/user.service';
import { AlertiflyService } from '../core/services/alertifly.service';
import { User } from '../core/models/user.model';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})
export class MembersComponent implements OnInit {

  users : User[];

  constructor(
    private readonly userService : UserService,
    private readonly alertify: AlertiflyService,
    private readonly route : ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users']
    })
  }

}
