import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../models/user.model';
import { UserService } from '../services/user.service';
import { AlertiflyService } from '../services/alertifly.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';


@Injectable()
export class MemberDetailsResolver implements Resolve<User> {

    constructor(
        private readonly userService: UserService,
        private readonly router: Router,
        private readonly alteryify: AlertiflyService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<User> {
        return this.userService.getUserById(route.params['id'])
        .pipe( catchError(error => {
            this.alteryify.error('Error occured');
            this.router.navigate(['/members']);
            return of(null);
        }))
    }

}