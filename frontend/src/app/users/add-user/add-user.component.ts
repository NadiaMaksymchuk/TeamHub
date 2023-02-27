import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { NewUser } from 'src/app/models/new-user';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.sass']
})
export class AddUserComponent implements OnDestroy{
  public newUser: NewUser = {} as NewUser;
  
  private unsubscribe$ = new Subject<void>();

  constructor(
    private service:UsersService,
    private snackBarService: SnackBarService,
    private router: Router) 
  {}

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public add() {
    this.service
    .add(this.newUser)
    .subscribe(
      (responce) => {
        this.snackBarService.showMessage('Successfully added');
        this.goToMainePage()
    },
      (error) => {
        error.error.errors.forEach((element: any) => {
          this.snackBarService.showMessage(element);
        });   
      })
    }

    private goToMainePage() {
      this.router.navigate(['/']);
    }
}
