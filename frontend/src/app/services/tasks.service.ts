import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { NewUser } from '../models/new-user';
import { UpdateUser } from '../models/update-user';
import { Task } from '../models/task';
import { NewTask } from '../models/new-task';
import { UpdateTask } from '../models/update-task';

@Injectable({
  providedIn: 'root'
})
export class TasksService {
  public routePrefix = 'api/tasks';

  constructor(private httpService: HttpInternalService) { }

  get(){
    return this.httpService.getFullRequest<Task[]>(`${this.routePrefix}`);
  }

  add(task: NewTask) {
    return this.httpService.postFullRequest<any>(`${this.routePrefix}`, task);
  }

  edit(task: UpdateTask) {
    return this.httpService.putFullRequest<any>(`${this.routePrefix}`, task);
  }

  searchFromId(id:number) {
    return this.httpService.getFullRequest<any>(`${this.routePrefix}/${id}`);
  }
}
