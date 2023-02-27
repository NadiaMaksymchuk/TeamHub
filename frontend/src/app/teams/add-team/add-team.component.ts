import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { NewTeam } from 'src/app/models/new-team';
import { ProjectsService } from 'src/app/services/projects.service';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { TeamsService } from 'src/app/services/teams.service';

@Component({
  selector: 'app-add-team',
  templateUrl: './add-team.component.html',
  styleUrls: ['./add-team.component.sass']
})
export class AddTeamComponent implements OnDestroy{
  public newTeam: NewTeam = {} as NewTeam;
  
  private unsubscribe$ = new Subject<void>();

  constructor(
    private service:TeamsService,
    private snackBarService: SnackBarService,
    private router: Router) 
  {}

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public addTeam() {
    this.service
    .add(this.newTeam)
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
