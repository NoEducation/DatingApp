
import { Routes} from '@angular/router'
import { MembersComponent } from './app/members/members.component'
import { HomeComponent } from './app/home/home.component'
import { ListComponent } from './app/list/list.component'
import { MessagesComponent } from './app/messages/messages.component'
import { AuthGuard } from './app/core/guards/auth.guard'
import { MemberDetailsComponent } from './app/members/member-details/member-details.component'
import { MemberDetailsResolver } from './app/core/resolvers/member-details.resolver'
import { MemberResolver } from './app/core/resolvers/member.resolver'
import { MemberEditComponent } from './app/members/member-edit/member-edit.component'
import { MemberEditResolver } from './app/core/resolvers/member-edit.resolver'
import { PreventUnsavedChanges } from './app/core/guards/prevent-unsaved-changes.guard'

export const appRoutes : Routes = [
    { path : 'home' , component : HomeComponent},

    { path : 'members/edit', component : MemberEditComponent , 
        canActivate : [AuthGuard], resolve : {user: MemberEditResolver }, canDeactivate : [PreventUnsavedChanges]},

    { path : 'members/:id' , component : MemberDetailsComponent, 
    canActivate : [AuthGuard],resolve: {user: MemberDetailsResolver}},

    { path : 'members' , component : MembersComponent,
        canActivate : [AuthGuard], resolve : {users : MemberResolver}},

    { path : 'list' , component : ListComponent,canActivate : [AuthGuard]},

    { path : 'messages' , component : MessagesComponent,canActivate : [AuthGuard]},

    { path : '**' , redirectTo : 'home',  pathMatch : 'full'},
]