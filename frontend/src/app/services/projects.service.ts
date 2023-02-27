import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { Project } from '../models/project';
import { NewProject } from '../models/new-project';
import { UpdateProject } from '../models/update-project';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {
  public routePrefix = 'api/projects';

  constructor(private httpService: HttpInternalService) { }

  getProjects(){
    console.log(this.httpService.getFullRequest<Project[]>(`${this.routePrefix}`));
    return this.httpService.getFullRequest<Project[]>(`${this.routePrefix}`);
  }

  addProject(project: NewProject) {
    return this.httpService.postFullRequest<any>(`${this.routePrefix}`, project);
  }

  editProject(project: UpdateProject) {
    return this.httpService.putFullRequest<any>(`${this.routePrefix}`, project);
  }

  searchFromId(id:number) {
    return this.httpService.getFullRequest<any>(`${this.routePrefix}/${id}`);
  }
}
