import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  @Output() CancelToggler = new EventEmitter();

  model: any = {};

  constructor(
    public accountservice: AccountService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  register() {
    this.accountservice.register(this.model).subscribe({
      next: (nxt) => console.log(nxt),
      error: (err) => {
        console.error(err), 
        this.toastr.error(err.error);
      },
      complete: () => {
        console.info('complete'), 
        this.router.navigateByUrl('/members');
      },
    });
  }

  cancel() {
    this.CancelToggler.emit(false);
  }
}
