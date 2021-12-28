import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  errorMsg: string;
  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error) => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                const modalStateErrors = [];
                for (const key in error.error.errors)
                  if (error.error.errors[key])
                    modalStateErrors.push(error.error.errors[key]);
                throw modalStateErrors.flat();
              } else {
                this.errorMsg = `Error Code: ${error.status}, Message: ${error.message}`;
                this.toastr.error(error.error, error.status);
              }
              break;

            case 401:
              this.toastr.error(error.error, error.status);
              this.errorMsg = `Error Code: ${error.status}, Message: ${error.message}`;
              break;

            case 404:
              this.router.navigateByUrl('/not-found');
              this.errorMsg = `Error Code: ${error.status}, Message: ${error.message}`;
              break;

            case 500:
              const navigationExtras: NavigationExtras = {
                state: { error: error.error },
              };
              this.router.navigateByUrl('/server-error', navigationExtras);
              this.errorMsg = `Error Code: ${error.status}, Message: ${error.error.message}`;
              break;

            default:
              this.toastr.error('Unexpected Error');
              console.error(error);
              break;
          }
        }
        return throwError(() => new Error(this.errorMsg));
      })
    );
  }
}
