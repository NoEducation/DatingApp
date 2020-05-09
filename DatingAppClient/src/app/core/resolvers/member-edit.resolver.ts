import { Injectable } from "@angular/core";
import { User } from '../models/user.model';
import { UserService } from '../services/user.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AlertiflyService } from '../services/alertifly.service';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { catchError } from 'rxjs/operators';


@Injectable()
export class MemberEditResolver implements Resolve<User> {

    resolve(route: ActivatedRouteSnapshot): Observable<User> {
        return this.userService.getUserById(this.authService.decodedToken.nameid).pipe(
            catchError(error =>
                {
                    this.aletrifyService.error('Problerm retrieving your data token service');
                    this.router.navigate(['/members']);
                    return of<User>(null);
                })
        );
    }

    constructor(private readonly userService : UserService,
        private readonly jwtService : JwtHelperService,
        private readonly aletrifyService :AlertiflyService,
        private readonly authService : AuthService ,
        private readonly router : Router ){}

}