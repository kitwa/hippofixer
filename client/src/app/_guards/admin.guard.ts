import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';

export const adminGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);

  return accountService.currentUser$.pipe(
    map(user => {
      if (!user) return false;
      if (user.roles.includes('Admin') || user.roles.includes('Client') || user.roles.includes('SuperAdmin') || user.roles.includes('Contractor')) {
        return true;
      } else {
        toastr.error('You do not have the right.');
        return false;
      }
    })
  )
};