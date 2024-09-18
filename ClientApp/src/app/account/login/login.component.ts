import {Component} from '@angular/core';
import {AccountService} from '../account.service';
import {ActivatedRoute, Router} from '@angular/router';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {environment} from 'src/environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent {

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required)
  });

  errorMessage: string = '';
  showError: boolean = false;
  baseUrl = environment.apiUrl;

  constructor(private accountService: AccountService, private router: Router, private activatedRoute: ActivatedRoute) {}

  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe({
      next: () => {},
      error: (err) => {
        this.errorMessage = err.error;
        this.showError = true;
      }
    });
  }
}
