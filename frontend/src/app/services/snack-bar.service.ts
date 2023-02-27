import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackBarService {

  public constructor(private snackBar: MatSnackBar) {}

  public showMessage(error: any) {
      this.snackBar.open(error, 'Ok', { duration: 116000, panelClass: 'error-snack-bar' });
  }
}
