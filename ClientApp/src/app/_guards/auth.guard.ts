import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private accountservice: AccountService,
    private toastr: ToastrService,
    private router: Router
  ) {}
  canActivate(): Observable<boolean> {
    return this.accountservice.currentUser$.pipe(
      map((user) => {
        if (user) return true;
        else {
          this.toastr.error('No Pass!');
          this.router.navigateByUrl('/');
          return false;
        }
      })
    );
  }
}
