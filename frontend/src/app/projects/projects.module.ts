import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowProjectsComponent } from './show-projects/show-projects.component';
import { RouterModule, Routes } from '@angular/router';
import { MaterialComponentsModule } from '../common/material-components.module';
import { AddProjectComponent } from './add-project/add-project.component';
import { UpdateProjectComponent } from './update-project/update-project.component';
import { DirtyCheckGuard } from '../guards/dirty-check.guard'; 

const AppRoutes: Routes = [
  { path: 'projects/show', component:  ShowProjectsComponent},
  { path: 'projects/add', component:  AddProjectComponent},
  {
    path: 'projects/update',
    component: UpdateProjectComponent,
    canDeactivate: [DirtyCheckGuard]
  },
];

@NgModule({
  declarations: [
    ShowProjectsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot(AppRoutes),
    MaterialComponentsModule
  ]
})
export class ProjectsModule { }
