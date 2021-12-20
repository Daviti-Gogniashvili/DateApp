import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  public model: any = {};

  constructor(public accountservice: AccountService) {

  }

  ngOnInit(): void {
  }

  login() {
    this.accountservice.login(this.model).subscribe({
      next: (nxt) => console.log(nxt),
      error: (err) => console.error(err),
      complete: () => console.info('complete'),
    });
  }

  logout() {
    this.accountservice.logout();
    var x = document.getElementById('buttonLI_LO');
    x!.textContent = 'Login';
    x!.style.width ="70px";

  }
}
