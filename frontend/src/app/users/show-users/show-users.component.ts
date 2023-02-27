import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Subject, takeUntil } from 'rxjs';
import { User } from 'src/app/models/user';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-show-users',
  templateUrl: './show-users.component.html',
  styleUrls: ['./show-users.component.sass']
})
export class ShowUsersComponent implements OnInit, OnDestroy{
  public users: User[] = [];
  displayedColumns: string[] = ['position', 'firstName', 'lastName', 'email', 'createdAt', 'birthDay'];
  public loading = false;
  private unsubscribe$ = new Subject<void>();
  dataSource: any;

  constructor(
    private service:UsersService,
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
                    this.users = resp.body;
                    this.dataSource = new MatTableDataSource<User>(this.users);
                    this.dataSource.paginator = this.paginator;
                },
                (error) => {
                  this.loading = false;
                  this.snackBarService.showMessage(error.error.message);
                }
            );
  }
}
