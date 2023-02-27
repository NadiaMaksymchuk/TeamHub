import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { User } from '../models/user';
import { NewUser } from '../models/new-user';
import { UpdateUser } from '../models/update-user';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  public routePrefix = 'api/users';

  constructor(private httpService: HttpInternalService) { }

  get(){
    return this.httpService.getFullRequest<User[]>(`${this.routePrefix}`);
  }

  add(user: NewUser) {
    return this.httpService.postFullRequest<any>(`${this.routePrefix}`, user);
  }

  edit(user: UpdateUser) {
    return this.httpService.putFullRequest<any>(`${this.routePrefix}`, user);
  }

  searchFromId(id:number) {
    return this.httpService.getFullRequest<any>(`${this.routePrefix}/${id}`);
  }
}
