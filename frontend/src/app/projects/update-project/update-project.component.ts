import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { NewProject } from 'src/app/models/new-project';
import { Project } from 'src/app/models/project';
import { ProjectsService } from 'src/app/services/projects.service';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { UpdateProject } from 'src/app/models/update-project';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { DirtyComponent } from 'src/app/models/dirty-component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-update-project',
  templateUrl: './update-project.component.html',
  styleUrls: ['./update-project.component.sass']
})
export class UpdateProjectComponent implements OnDestroy, OnInit, DirtyComponent {
  public oldProjects: Project = {} as Project;
  public editedProject: NewProject = {} as NewProject;

  public loadingProjects = false;
  private unsubscribe$ = new Subject<void>();
  id: number;

  projectForm: FormGroup;
  isDirty = false;

  isLinear = true;

  constructor(
    private service:ProjectsService,
    private snackBarService: SnackBarService,
    private router: Router) 
  {}

  ngOnInit(): void {
    this.projectForm = new FormGroup({
      projectId: new FormControl(),
      name:  new FormControl(),
      description:  new FormControl(),
      deadline:  new FormControl(),
      teamId:  new FormControl(),
      authorId:  new FormControl(),
    });
    this.projectForm.valueChanges.subscribe( e => this.isDirty = true );
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  canDeactivate() {
    return this.isDirty;
  }

  public editProject() {
    var updateProject: UpdateProject = {
      id: this.id,
      authorId: this.editedProject.authorId ,
      teamId: this.editedProject.teamId,
      name: this.editedProject.name? this.editedProject.name: this.oldProjects.name,
      description: this.editedProject.description ? this.editedProject.description: this.oldProjects.description,
      deadline: this.editedProject.deadline ? this.editedProject.deadline: this.oldProjects.deadline
    }; 

    this.service
    .editProject(updateProject)
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
          this.oldProjects = responce.body;
          console.log(this.oldProjects)
    },
    (error) => this.snackBarService.showMessage("Enter id of existed project!"))
  }

  private goToMainePage() {
    this.isDirty = false;
    this.router.navigate(['/']);
  }
}

