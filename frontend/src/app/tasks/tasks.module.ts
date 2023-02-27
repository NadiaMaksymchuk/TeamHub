import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowTasksComponent } from './show-tasks/show-tasks.component';
import { RouterModule, Routes } from '@angular/router';
import { AddTasksComponent } from './add-tasks/add-tasks.component';
import { UpdateTasksComponent } from './update-tasks/update-tasks.component';
import { MaterialComponentsModule } from '../common/material-components.module';
import { DirtyCheckGuard } from '../guards/dirty-check.guard';
import { TaskDirective } from '../directives/task.directive';

const AppRoutes: Routes = [
  { path: 'tasks/show', component:  ShowTasksComponent},
  { path: 'tasks/add', component:  AddTasksComponent},
  {
    path: 'tasks/update',
    component: UpdateTasksComponent,
    canDeactivate: [DirtyCheckGuard]
  },
];

@NgModule({
  declarations: [
    ShowTasksComponent,
    AddTasksComponent,
    UpdateTasksComponent,
    TaskDirective
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot(AppRoutes),
    MaterialComponentsModule
  ]
})
export class TasksModule { }
