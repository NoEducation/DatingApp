import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { MemberEditComponent } from 'src/app/members/member-edit/member-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChanges implements CanDeactivate<MemberEditComponent> {

constructor() { }

canDeactivate(component: MemberEditComponent){

  if(component.form.dirty){
    return confirm("There are unsaved changes, are you sure that you want leave page");
  }
  else return true;
}

}
