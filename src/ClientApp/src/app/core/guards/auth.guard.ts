import {inject} from '@angular/core';
import {CanActivateFn, Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {map} from 'rxjs';
import {AccountService} from 'src/app/account/account.service';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);
  const router = inject(Router);

  return accountService.currentUser$.pipe(
    map(user => {
      if (user) {
        return true;
      } else {
        toastr.error('You shall not pass!');
        router.navigate(['/account/login'], { queryParams: { returnUrl: state.url } });
        return false;
      }
    })
  );
};
