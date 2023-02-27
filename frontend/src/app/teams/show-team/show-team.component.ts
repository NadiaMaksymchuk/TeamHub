import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Subject, takeUntil } from 'rxjs';
import { Project } from 'src/app/models/project';
import { Team } from 'src/app/models/team';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { TeamsService } from 'src/app/services/teams.service';

@Component({
  selector: 'app-show-team',
  templateUrl: './show-team.component.html',
  styleUrls: ['./show-team.component.sass']
})
export class ShowTeamComponent implements OnInit, OnDestroy{
  public teams: Team[] = [];
  displayedColumns: string[] = ['position', 'name', 'createdAt', 'usersCount' ];
  public loadingProjects = false;
  private unsubscribe$ = new Subject<void>();
  dataSource: any;

  constructor(
    private service:TeamsService,
    private snackBarService: SnackBarService,) 
  {}

  @ViewChild(MatPaginator) paginator: MatPaginator;
  
  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  ngOnInit(): void {
    this.getAll();
  }

  private getAll() {
    this.loadingProjects = true;
    this.service
            .get()
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                (resp) => {
                    this.loadingProjects = false;
                    this.teams = resp.body;
                    this.dataSource = new MatTableDataSource<Team>(this.teams);
                    this.dataSource.paginator = this.paginator;
                },
                (error) => {
                  this.loadingProjects = false;
                  this.snackBarService.showMessage(error.error.message);
                }
            );
  }
}
