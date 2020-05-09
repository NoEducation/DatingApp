import { FormControl, FormGroup, Validators } from '@angular/forms';


export class ListFromControl extends FormControl{

    label : string;
    modelProperty : string

    constructor(label : string,property: string, value : any ,validator : any) {
        super(value, validator);
    }

    ///Zbieram messages 
    // getValidationMessages(){
    //     let messages : string[] = [];
    //     if(this.errors){
    //         for
    //     }
    // }
} 

export class ListFormGroup extends FormGroup{
    constructor(){
        super({
            name : new ListFromControl("Produkt", "name", "", Validators.required), 
            category : new ListFromControl("Kategoria", "category", "", 
                Validators.compose([Validators.required,
                Validators.pattern("^[A-Za-z ]+$")]))
        });
    }
    get listControls() : ListFromControl[]{
        return Object.keys(this.controls)
            .map(key => this.controls[key] as ListFromControl);
    }
}