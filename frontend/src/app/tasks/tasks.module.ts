import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowTasksComponent } from './show-tasks/show-tasks.component';
import { RouterModule, Routes } from '@angular/router';
import { AddTasksComponent } from './add-tasks/add-tasks.component';
import { UpdateTasksComponent } from './update-tasks/update-tasks.component';

const AppRoutes: Routes = [
  { path: 'tasks/show', component:  ShowTasksComponent},
  { path: 'tasks/add', component:  AddTasksComponent},
  { path: 'tasks/update', component:  UpdateTasksComponent},
];

@NgModule({
  declarations: [
    ShowTasksComponent,
    AddTasksComponent,
    UpdateTasksComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot(AppRoutes)
  ]
})
export class TasksModule { }
