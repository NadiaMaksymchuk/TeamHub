import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs/internal/Subject';
import { NewProject } from 'src/app/models/new-project';
import { ProjectsService } from 'src/app/services/projects.service';
import { SnackBarService } from 'src/app/services/snack-bar.service';

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.sass']
})
export class AddProjectComponent implements OnDestroy{
  public newProject: NewProject = {} as NewProject;
  
  private unsubscribe$ = new Subject<void>();

  constructor(
    private service:ProjectsService,
    private snackBarService: SnackBarService,
    private router: Router) 
  {}

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public addProject() {
    this.service
    .addProject(this.newProject)
    .subscribe(
      (responce) => {
        this.snackBarService.showMessage('Successfully added');
        this.goToMainePage()
    },
      (error) => {
        error.errors.forEach((element: any) => {
          this.snackBarService.showMessage(element);
        });   
      })
    }

    private goToMainePage() {
      this.router.navigate(['/']);
    }
}

