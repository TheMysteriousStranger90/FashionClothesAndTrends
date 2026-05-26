import {ResolveFn} from '@angular/router';
import {User} from 'src/app/shared/models/user';
import {inject} from '@angular/core';
import {UsersService} from 'src/app/users/users.service';

export const userDetailedResolver: ResolveFn<User> = (route, state) => {
  const userService = inject(UsersService);

  return userService.getUser(route.paramMap.get('username')!)
};
