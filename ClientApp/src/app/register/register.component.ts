import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  @Output() CancelToggler = new EventEmitter();

  model: any = {};

  constructor() { }

  ngOnInit(): void {
  }

  register() {
    
  }

  cancel() {
    this.CancelToggler.emit(false);
  }

}
