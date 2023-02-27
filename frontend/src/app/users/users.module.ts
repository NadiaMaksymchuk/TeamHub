import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddUserComponent } from './add-user/add-user.component';
import { UpdateUserComponent } from './update-user/update-user.component';
import { ShowUsersComponent } from './show-users/show-users.component';
import { RouterModule, Routes } from '@angular/router';
import { DirtyCheckGuard } from '../guards/dirty-check.guard';
import { MaterialComponentsModule } from '../common/material-components.module';

const AppRoutes: Routes = [
  { path: 'users/show', component:  ShowUsersComponent},
  { path: 'users/add', component:  AddUserComponent},
  {
    path: 'users/update',
    component: UpdateUserComponent,
    canDeactivate: [DirtyCheckGuard]
  },
];

@NgModule({
  declarations: [
    AddUserComponent,
    UpdateUserComponent,
    ShowUsersComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot(AppRoutes),
    MaterialComponentsModule
  ]
})
export class UsersModule { }
