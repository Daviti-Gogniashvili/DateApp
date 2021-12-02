import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Dating App';
  public displayedColumns: string[] = ['id', 'userName'];
  public users: Users[];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.http.get<Users[]>('https://localhost:5001/API/AppUsers').subscribe(result => {
      this.users = result;
    }, error => console.error(error));
  }
}

interface Users {
  id: number;
  userName: string;
}
