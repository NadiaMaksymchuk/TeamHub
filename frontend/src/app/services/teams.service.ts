import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { NewUser } from '../models/new-user';
import { UpdateUser } from '../models/update-user';
import { Task } from '../models/task';
import { NewTask } from '../models/new-task';
import { UpdateTask } from '../models/update-task';
import { Team } from '../models/team';
import { NewTeam } from '../models/new-team';
import { UpdateTeam } from '../models/update-team';

@Injectable({
  providedIn: 'root'
})
export class TeamsService {
  public routePrefix = 'api/teams';

  constructor(private httpService: HttpInternalService) { }

  get(){
    return this.httpService.getFullRequest<Team[]>(`${this.routePrefix}`);
  }

  add(team: NewTeam) {
    return this.httpService.postFullRequest<any>(`${this.routePrefix}`, team);
  }

  edit(team: UpdateTeam) {
    return this.httpService.putFullRequest<any>(`${this.routePrefix}`, team);
  }

  searchFromId(id:number) {
    return this.httpService.getFullRequest<any>(`${this.routePrefix}/${id}`);
  }
}
