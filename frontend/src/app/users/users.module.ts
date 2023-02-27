import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddUserComponent } from './add-user/add-user.component';
import { UpdateUserComponent } from './update-user/update-user.component';
import { ShowUsersComponent } from './show-users/show-users.component';
import { RouterModule, Routes } from '@angular/router';

const AppRoutes: Routes = [
  { path: 'users/show', component:  ShowUsersComponent},
  { path: 'users/add', component:  AddUserComponent},
  { path: 'users/update', component:  UpdateUserComponent},
];

@NgModule({
  declarations: [
    AddUserComponent,
    UpdateUserComponent,
    ShowUsersComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot(AppRoutes)
  ]
})
export class UsersModule { }
