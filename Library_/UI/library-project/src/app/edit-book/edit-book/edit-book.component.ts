import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { EditBookService } from 'src/app/services/edit-book.service';
import { ImageFileToBase64 } from 'src/app/class/Base64/ImageFileTOBase64';
import { BookListComponent } from 'src/app/book-list/book-list/book-list.component';
import { DataShareService } from 'src/app/services/data-share.service';
import { BookListItemComponent } from 'src/app/book-list-item/book-list-item/book-list-item.component';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.css'],
  providers: [BookListComponent, BookListItemComponent]
})
export class EditBookComponent implements OnInit {

  constructor(private editbookdServices: EditBookService,
    private sanitizer: DomSanitizer,
    private dataShare: DataShareService,
    private bookListComponent: BookListComponent,
    private bookListItemComponent: BookListItemComponent,
    private router: Router,

  ) { }

  // formBuilder!: FormBuilder;
  file_!: any;
  str!: string;
  imagePath!: any;

  bookForm = new FormGroup({
    id: new FormControl(''),
    title: new FormControl(''),
    cover: new FormControl(''),
    genre: new FormControl(''),
    author: new FormControl(''),
    content: new FormControl(''),
  });

  ngOnInit(): void { }

  AddBook() {
    this.bookForm.value.id = "0";

    let imageFileToBase = new ImageFileToBase64();
    imageFileToBase.ImageToBase64(this.file_[0]).then(data => (
      this.bookForm.value.cover = data,
      this.editbookdServices.createBook(this.bookForm.value)
        .subscribe(
          response => {
            console.log(response);
            this.bookListComponent.GetAllBooks();

            this.bookListComponent.GetAllBooks(),
              this.router.navigate(['api/books'])
                .then(() => {
                  window.location.reload();
                });
            this.Clear();
            (<HTMLInputElement>document.getElementById("formLabel")).value = "Add Book"


          }
        )
    ));
  }
  error!:any;

  uploadSingleFile(event: Event) {
    this.file_ = (event.target as HTMLInputElement).files;
  }

  Clear() {
    this.bookForm.reset({
      'id': '',
      'title': '',
      'cover': '',
      'genre': '',
      'author': '',
      'content': '',
    });
  }

  updateBook() {
    this.dataShare.currentIdData.subscribe((tmp) => this.bookForm.value.id = tmp.toString());
    if (this.bookForm.value.cover === '') {
      this.dataShare.currentData.subscribe((tmp) => this.bookForm.value.cover = tmp);
      this.editBookWithOldImage();
    } else {
      this.editBookWithNewImage();
    }
  }

  editBookWithOldImage() {
    this.editbookdServices.updateBook(this.bookForm.value)
      .subscribe(
        response => {
          console.log(response);
          this.bookListComponent.GetAllBooks(),
            this.router.navigate(['api/books'])
              .then(() => {
                window.location.reload();
              }),
            this.Clear(),
            (<HTMLInputElement>document.getElementById("formLabel")).value = "Add Book";
        },
      );
  }

  editBookWithNewImage() {
    let imageFileToBase = new ImageFileToBase64();
    imageFileToBase.ImageToBase64(this.file_[0]).then(data => (this.bookForm.value.cover = data, this.editbookdServices.updateBook(this.bookForm.value)
      .subscribe(
        response => {
          console.log(response);
          this.bookListComponent.GetAllBooks(),
            this.router.navigate(['api/books'])
              .then(() => {
                window.location.reload();
              }),
            this.Clear(),
            (<HTMLInputElement>document.getElementById("formLabel")).value = "Add Book"
        },
      )));
  }
}




