import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { DirtyComponent } from 'src/app/models/dirty-component';
import { NewTask } from 'src/app/models/new-task';
import { Task } from 'src/app/models/task';
import { UpdateTask } from 'src/app/models/update-task';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { TasksService } from 'src/app/services/tasks.service';

@Component({
  selector: 'app-update-tasks',
  templateUrl: './update-tasks.component.html',
  styleUrls: ['./update-tasks.component.sass']
})
export class UpdateTasksComponent implements OnDestroy, OnInit, DirtyComponent{
  public oldTask: Task = {} as Task;
  public editedTask: UpdateTask = {} as UpdateTask;

  public loading = false;
  private unsubscribe$ = new Subject<void>();
  id: number;

  taskForm: FormGroup;
  isDirty = false;

  isLinear = true;

  constructor(
    private service:TasksService,
    private snackBarService: SnackBarService,
    private router: Router) 
  {}

  ngOnInit(): void {
    this.taskForm = new FormGroup({
      taskId: new FormControl(),
      name:  new FormControl(),
      description:  new FormControl(),
      state:  new FormControl(),
      finishedAt:  new FormControl(),
      projectId:  new FormControl(),
      performerId:  new FormControl(),
    });
    this.taskForm.valueChanges.subscribe( e => this.isDirty = true );
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  canDeactivate() {
    return this.isDirty;
  }

  public edit() {
    var updateTask: UpdateTask = {
      id: this.id,
      name: this.editedTask.name ? this.editedTask.name: this.oldTask.name,
      projectId: this.editedTask.projectId,
      performerId: this.editedTask.performerId,
      description: this.editedTask.description ? this.editedTask.description: this.oldTask.description,
      state: this.editedTask.state ? this.editedTask.state: this.oldTask.state,
      finishedAt: this.editedTask.finishedAt ? this.editedTask.finishedAt: this.oldTask.finishedAt,
    }; 

    this.service
    .edit(updateTask)
    .subscribe(
      (responce) => {
        this.snackBarService.showMessage('Successfully updated');
        this.goToMainePage();
    },
    (error) => this.snackBarService.showMessage(error.message))  
  }

  public autocomplete() {
    this.service
    .searchFromId(this.id)
    .subscribe(
      (responce) => {
          this.oldTask = responce.body;
          console.log(this.oldTask)
    },
    (error) => this.snackBarService.showMessage("Enter id of existed project!"))
  }

  private goToMainePage() {
    this.isDirty = false;
    this.router.navigate(['/']);
  }
}
