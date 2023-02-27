import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ProjectsService } from 'src/app/services/projects.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Project } from 'src/app/models/project';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { SnackBarService } from 'src/app/services/snack-bar.service';

@Component({
  selector: 'app-show-projects',
  templateUrl: './show-projects.component.html',
  styleUrls: ['./show-projects.component.sass']
})
export class ShowProjectsComponent implements OnInit, OnDestroy{
    public projects: Project[] = [];
    displayedColumns: string[] = ['position', 'name', 'authorName', 'email', 'teamName','deadline', 'createdAt', ];
    public loadingProjects = false;
    private unsubscribe$ = new Subject<void>();
    dataSource: any;

    constructor(
      private service:ProjectsService,
      private snackBarService: SnackBarService,) 
    {}
  
    @ViewChild(MatPaginator) paginator: MatPaginator;
    
    ngOnDestroy(): void {
      this.unsubscribe$.next();
      this.unsubscribe$.complete();
    }
  
    ngOnInit(): void {
      this.getProjects();
    }

    private getProjects() {
      this.loadingProjects = true;
      this.service
              .getProjects()
              .pipe(takeUntil(this.unsubscribe$))
              .subscribe(
                  (resp) => {
                      this.loadingProjects = false;
                      this.projects = resp.body;
                      this.dataSource = new MatTableDataSource<Project>(this.projects);
                      this.dataSource.paginator = this.paginator;
                  },
                  (error) => {
                    this.loadingProjects = false;
                    this.snackBarService.showMessage(error.error.message);
                  }
              );
    }
}


