import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {MatMenuModule} from '@angular/material/menu';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProjectsModule } from './projects/projects.module';
import { TasksModule } from './tasks/tasks.module';
import { TeamsModule } from './teams/teams.module';
import { UsersModule } from './users/users.module';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialComponentsModule } from './common/material-components.module';
import { MainPageComponent } from './main-page/main-page.component';
import {MatDividerModule} from '@angular/material/divider';
import {MatPaginatorModule} from '@angular/material/paginator';
import { AddProjectComponent } from './projects/add-project/add-project.component';
import { UpdateProjectComponent } from './projects/update-project/update-project.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MainPageComponent,
    AddProjectComponent,
    UpdateProjectComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ProjectsModule,
    TeamsModule,
    UsersModule,
    TasksModule,
    BrowserAnimationsModule,
    MaterialComponentsModule,
    MatMenuModule,
    MatDividerModule,
    MatPaginatorModule
  ],
  exports: [
    MatPaginatorModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
