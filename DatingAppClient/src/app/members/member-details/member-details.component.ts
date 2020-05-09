import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/core/models/user.model';
import { AlertiflyService } from 'src/app/core/services/alertifly.service';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css']
})
export class MemberDetailsComponent implements OnInit {

  user : User;

  constructor(private readonly userService : UserService,
    private readonly alterifyServcie : AlertiflyService,
    private readonly route : ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe( data => {
      this.user = data['user'];
    })
  }


}
