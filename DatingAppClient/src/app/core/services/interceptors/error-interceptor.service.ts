import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable()
export class  ErrorInterceptor implements HttpInterceptor{

    //intercept request and catch any errors
    intercept(
        req: import("@angular/common/http").HttpRequest<any>,
        next: import("@angular/common/http").HttpHandler) :
      import("rxjs").Observable<import("@angular/common/http").HttpEvent<any>>
    {   
        // error (HttpError) -> error (another class) -> errors -> (arry of errors defined for each popery in server)
        return next.handle(req).pipe(
            catchError( error => {
                // take care of 401 type errors
                if(error.status === 401){
                    return throwError(error.statusText);
                }
                // httpErrorResponse class defined by angular to handler errors 
                if(error instanceof HttpErrorResponse){
                    const applicationError = error.headers.get('Application-Error');
                    // take care of 500 + type errors
                    if(applicationError){
                        return throwError(applicationError);
                    }
                    const serverErrors = error.error;
                    let modelStateErrors = '';
                    if(serverErrors.errors && typeof serverErrors.errors === 'object'){
                        for(const key in serverErrors.errors){
                            if(serverErrors.errors[key]){
                                modelStateErrors += serverErrors.errors[key] + '\n';
                            }
                        }
                    }
                    return throwError(modelStateErrors || serverErrors || 'Server Error');
                }
            })
        )
    }

}
export const ErrorInterceptorProvider = {
    provide : HTTP_INTERCEPTORS,
    useClass : ErrorInterceptor,
    multi : true
};