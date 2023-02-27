import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { DirtyComponent } from 'src/app/models/dirty-component';
import { NewTeam } from 'src/app/models/new-team';
import { Team } from 'src/app/models/team';
import { UpdateTeam } from 'src/app/models/update-team';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { TeamsService } from 'src/app/services/teams.service';

@Component({
  selector: 'app-update-team',
  templateUrl: './update-team.component.html',
  styleUrls: ['./update-team.component.sass']
})
export class UpdateTeamComponent implements OnDestroy, OnInit, DirtyComponent {
  public oldTeam: Team = {} as Team;
  public editedTeam: NewTeam = {} as NewTeam;

  public loadingProjects = false;
  private unsubscribe$ = new Subject<void>();
  id: number;

  teamForm: FormGroup;
  isDirty = false;

  isLinear = true;

  constructor(
    private service:TeamsService,
    private snackBarService: SnackBarService,
    private router: Router) 
  {}

  ngOnInit(): void {
    this.teamForm = new FormGroup({
      teamId: new FormControl(),
      name:  new FormControl(),
    });

    this.teamForm.valueChanges.subscribe( e => this.isDirty = true );
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  canDeactivate() {
    return this.isDirty;
  }

  public edit() {
    var updateTeam: UpdateTeam = {
      id: this.id,
      name: this.editedTeam.name? this.editedTeam.name: this.oldTeam.name
    }; 

    this.service
    .edit(updateTeam)
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
          this.oldTeam = responce.body;
    },
    (error) => this.snackBarService.showMessage("Enter id of existed team!"))
  }

  private goToMainePage() {
    this.isDirty = false;
    this.router.navigate(['/']);
  }
}
