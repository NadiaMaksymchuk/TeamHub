import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UpdateTeamComponent } from './update-team/update-team.component';
import { ShowTeamComponent } from './show-team/show-team.component';
import { AddTeamComponent } from './add-team/add-team.component';
import { RouterModule, Routes } from '@angular/router';
import { MaterialComponentsModule } from '../common/material-components.module';
import { DirtyCheckGuard } from '../guards/dirty-check.guard';

const AppRoutes: Routes = [
  { path: 'teams/show', component:  ShowTeamComponent},
  { path: 'teams/add', component:  AddTeamComponent},
  {
    path: 'teams/update',
    component: UpdateTeamComponent,
    canDeactivate: [DirtyCheckGuard]
  },
];

@NgModule({
  declarations: [
    UpdateTeamComponent,
    ShowTeamComponent,
    AddTeamComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot(AppRoutes),
    MaterialComponentsModule
  ]
})
export class TeamsModule { }
