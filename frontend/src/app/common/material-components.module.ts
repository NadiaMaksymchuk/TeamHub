import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {MatMenuModule} from '@angular/material/menu';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTableModule} from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { HttpClientModule } from '@angular/common/http';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import {MatIconModule} from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import {MatStepperModule} from '@angular/material/stepper';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatSnackBarModule} from '@angular/material/snack-bar';

@NgModule({
    imports: [
        BrowserModule,
        MatMenuModule,
        BrowserAnimationsModule,
        MatTableModule,
        MatPaginatorModule,
        HttpClientModule,
        MatCardModule,
        MatProgressSpinnerModule,
        MatIconModule,
        FormsModule,
        MatFormFieldModule,
        MatInputModule,
        ReactiveFormsModule,
        MatStepperModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatSnackBarModule
    ],
    exports: [
        BrowserModule,
        MatMenuModule,
        BrowserAnimationsModule,
        MatTableModule,
        MatPaginatorModule,
        HttpClientModule,
        MatCardModule,
        MatProgressSpinnerModule,
        MatIconModule,
        FormsModule,
        MatFormFieldModule,
        MatInputModule,
        ReactiveFormsModule,
        MatStepperModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatSnackBarModule
    ],
    declarations: []
})
export class MaterialComponentsModule {}
