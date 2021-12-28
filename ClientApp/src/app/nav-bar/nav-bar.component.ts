import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  public model: any = {};

  constructor(
    public accountservice: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  login() {
    this.accountservice.login(this.model).subscribe({
      next: (nxt) => console.log(nxt),
      complete: () => {
        console.info('complete'), this.router.navigateByUrl('/members');
      },
    });
  }

  logout() {
    this.accountservice.logout();
    this.router.navigateByUrl('/');
    var x = document.getElementById('buttonLI_LO');
    x!.textContent = 'Login';
    x!.style.width = '70px';
  }
}
