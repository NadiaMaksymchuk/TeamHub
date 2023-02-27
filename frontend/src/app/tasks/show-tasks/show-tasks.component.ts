import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Subject, takeUntil } from 'rxjs';
import { Task } from 'src/app/models/task';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { TasksService } from 'src/app/services/tasks.service';

@Component({
  selector: 'app-show-tasks',
  templateUrl: './show-tasks.component.html',
  styleUrls: ['./show-tasks.component.sass']
})
export class ShowTasksComponent implements OnInit, OnDestroy{
  public tasks: Task[] = [];
  displayedColumns: string[] = ['position', 'name', 'state','performerName', 'createdAt', 'finishedAt'];
  public loading = false;
  private unsubscribe$ = new Subject<void>();
  dataSource: any;

  constructor(
    private service:TasksService,
    private snackBarService: SnackBarService,) 
  {}

  @ViewChild(MatPaginator) paginator: MatPaginator;
  
  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  ngOnInit(): void {
    this.get();
  }

  private get() {
    this.loading = true;
    this.service
            .get()
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                (resp) => {
                    this.loading = false;
                    this.tasks = resp.body;
                    this.dataSource = new MatTableDataSource<Task>(this.tasks);
                    this.dataSource.paginator = this.paginator;
                },
                (error) => {
                  this.loading = false;
                  this.snackBarService.showMessage(error.error.message);
                }
            );
  }
}
