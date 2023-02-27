import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { DirtyComponent } from 'src/app/models/dirty-component';
import { NewUser } from 'src/app/models/new-user';
import { UpdateUser } from 'src/app/models/update-user';
import { User } from 'src/app/models/user';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.sass']
})
export class UpdateUserComponent implements OnDestroy, OnInit, DirtyComponent{
  public oldUser: User = {} as User;
  public editedUser: NewUser = {} as NewUser;

  private unsubscribe$ = new Subject<void>();
  id: number;

  userForm: FormGroup;
  isDirty = false;

  isLinear = true;

  constructor(
    private service:UsersService,
    private snackBarService: SnackBarService,
    private router: Router) 
  {}

  ngOnInit(): void {
    this.userForm = new FormGroup({
      userId: new FormControl(),
      firstName:  new FormControl(),
      lastName:  new FormControl(),
      birthday:  new FormControl(),
      teamId:  new FormControl(),
      email: new FormControl()
    });
    this.userForm.valueChanges.subscribe( e => this.isDirty = true );
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  canDeactivate() {
    return this.isDirty;
  }

  public edit() {
    var updateUser: UpdateUser = {
      id: this.id,
      teamId: this.editedUser.teamId,
      firstName: this.editedUser.firstName ? this.editedUser.firstName: this.oldUser.firstName,
      lastName: this.editedUser.lastName ? this.editedUser.lastName: this.oldUser.lastName,
      birthDay: this.editedUser.birthDay ? this.editedUser.birthDay: this.oldUser.birthDay,
      email: this.editedUser.email ? this.editedUser.email: this.oldUser.email,
    }; 

    this.service
    .edit(updateUser)
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
          this.oldUser = responce.body; 
    },
    (error) => this.snackBarService.showMessage("Enter id of existed user!"))
  }

  private goToMainePage() {
    this.isDirty = false;
    this.router.navigate(['/']);
  }
}
