import { BrowserModule } from '@angular/platform-browser';
import {TabsModule} from 'ngx-bootstrap/tabs'
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { SidebarTopComponent } from './layout/sidebar-top/sidebar-top.component';
import { FormsModule} from '@angular/forms';
import { AuthService } from './core/services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterHomeComponent } from './home/register-home/register-home.component';
import { ErrorInterceptorProvider } from './core/services/interceptors/error-interceptor.service';
import { AlertiflyService } from './core/services/alertifly.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { MessagesComponent } from './messages/messages.component';
import { MembersComponent } from './members/members.component';
import { ListComponent } from './list/list.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from 'src/routes';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UserService } from './core/services/user.service';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { JwtModule } from '@auth0/angular-jwt';
import { MemberDetailsComponent } from './members/member-details/member-details.component';
import { MemberDetailsResolver } from './core/resolvers/member-details.resolver';
import { MemberResolver } from './core/resolvers/member.resolver';
import { LearnMoreComponent } from './home/learn-more/learn-more.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './core/resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from './core/guards/prevent-unsaved-changes.guard';
import { PhotoEditingComponent } from './members/member-edit/photo-editing/photo-editing.component';
import { FileUploadModule } from 'ng2-file-upload';

export function tokenGetter() : string{
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      SidebarTopComponent,
      HomeComponent,
      RegisterHomeComponent,
      MessagesComponent,
      MembersComponent,
      ListComponent,
      MemberCardComponent,
      MemberDetailsComponent,
      LearnMoreComponent,
      MemberEditComponent,
      PhotoEditingComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule,
      TabsModule,
      TabsModule.forRoot(), 
      RouterModule.forRoot(appRoutes),
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      FileUploadModule,
      JwtModule.forRoot(
         {
            config : {
               tokenGetter : tokenGetter,
               whitelistedDomains : [
                  'localhost:5000'
               ],
               blacklistedRoutes : [ 'localhost:5000/api/Auth']
            }
         }
      )
   ],
   providers: [
      AuthService,
      AlertiflyService,
      ErrorInterceptorProvider,
      UserService,
      MemberDetailsResolver,
      MemberResolver,
      MemberEditResolver,
      PreventUnsavedChanges
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
