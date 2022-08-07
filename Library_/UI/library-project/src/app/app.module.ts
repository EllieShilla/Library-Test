import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BookListComponent } from './book-list/book-list/book-list.component';
import { BookListItemComponent } from './book-list-item/book-list-item/book-list-item.component';
import { BooksPageComponent } from './books-page/books-page/books-page.component';
import { EditBookComponent } from './edit-book/edit-book/edit-book.component';
import { ViewBookComponent } from './view-book/view-book/view-book.component';

import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

import {MatCardModule} from '@angular/material/card';
import {MatDialogModule} from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatIconModule} from '@angular/material/icon';

@NgModule({
  declarations: [
    AppComponent,
    BookListComponent,
    BookListItemComponent,
    BooksPageComponent,
    EditBookComponent,
    ViewBookComponent
  ],
  entryComponents:[
ViewBookComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatCardModule,
    ReactiveFormsModule,
    MatDialogModule,
    BrowserAnimationsModule,
    MatIconModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
