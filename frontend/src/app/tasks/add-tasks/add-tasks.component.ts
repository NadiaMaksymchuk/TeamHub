import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { NewTask } from 'src/app/models/new-task';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { TasksService } from 'src/app/services/tasks.service';

@Component({
  selector: 'app-add-tasks',
  templateUrl: './add-tasks.component.html',
  styleUrls: ['./add-tasks.component.sass']
})
export class AddTasksComponent implements OnDestroy {
  public newTask: NewTask = {} as NewTask;
  
  private unsubscribe$ = new Subject<void>();

  constructor(
    private service:TasksService,
    private snackBarService: SnackBarService,
    private router: Router) 
  {}

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public add() {
    this.service
    .add(this.newTask)
    .subscribe(
      (responce) => {
        this.snackBarService.showMessage('Successfully added');
        this.goToMainePage()
    },
      (error) => {
        error.erorr.errors.forEach((element: any) => {
          this.snackBarService.showMessage(element);
        });   
      })
    }

    private goToMainePage() {
      this.router.navigate(['/']);
    }
}
