import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.scss'],
})
export class TestErrorsComponent implements OnInit {
  baseURL = 'https://localhost:5001/API/';
  validationErrors: string[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {}

  get404Error() {
    this.http.get(this.baseURL + 'Buggy/not-found').subscribe({
      next: (nxt) => console.log(nxt),
      error: (err) => console.error(err),
    });
  }

  get400Error() {
    this.http.get(this.baseURL + 'Buggy/bad-request').subscribe({
      next: (nxt) => console.log(nxt),
      error: (err) => console.error(err),
    });
  }

  get500Error() {
    this.http.get(this.baseURL + 'Buggy/server-error').subscribe({
      next: (nxt) => console.log(nxt),
      error: (err) => console.error(err),
    });
  }

  get401Error() {
    this.http.get(this.baseURL + 'Buggy/auth').subscribe({
      next: (nxt) => console.log(nxt),
      error: (err) => console.error(err),
    });
  }

  get400ValidationError() {
    this.http.post(this.baseURL + 'UserAccount/register', {}).subscribe({
      next: (nxt) => console.log(nxt),
      error: (err) => { console.error(err), this.validationErrors = err; }
    });
  }
}
