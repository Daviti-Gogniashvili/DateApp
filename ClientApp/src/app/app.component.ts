import { Component, OnInit } from '@angular/core';
import { User } from './_models/users';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'Dating App';

  changeToTrue: boolean;

  constructor(private accountservice: AccountService) {}

  ngOnInit() {
    this.setCurrentUser();

  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user')!);
    this.accountservice.setCurrentUser(user);
  }

  getCommand(event: boolean) {
    this.changeToTrue = event;
  }
}
