import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Users } from '../_models/users';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  toggler: boolean = false;

  public users: Users[];

  constructor(private http: HttpClient) {
   }

  ngOnInit() {
    this.getUsers();
  }



  registerToggle() {
    this.toggler = !this.toggler;
  }

  cancelToggler(event: boolean) {
    this.toggler = event;
  }

  getUsers() {
    this.http.get<Users[]>('https://localhost:5001/API/AppUsers').subscribe({
      next: (result) => this.users = result,
      error: (err) => console.error(err)
    });
  }

}
