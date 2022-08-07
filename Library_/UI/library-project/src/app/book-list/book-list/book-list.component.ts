import { Component, OnInit, SecurityContext } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DomSanitizer } from '@angular/platform-browser';
import { BookListItemComponent } from 'src/app/book-list-item/book-list-item/book-list-item.component';
import { BookDTO } from 'src/app/models/BookDTO.model';
import { BooklistService } from 'src/app/services/booklist.service';
import { DataShareService } from 'src/app/services/data-share.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css'],
  providers: [BookListItemComponent]
})

export class BookListComponent implements OnInit {
  bookId = 0;
  messageReceived: any;
  constructor(private booklistServece: BooklistService,
    private bookListItem: BookListItemComponent,
    public dialog: MatDialog,
    private dataShare: DataShareService,
    private sanitizer: DomSanitizer) { }
  books: BookDTO[] = [];
  recomendetBooks: BookDTO[] = [];

  bookTypeList: any = [];

  ngOnInit(): void {
    this.GetAllBooks();
  }

  async GetAllBooks() {
    let img: any;
    this.books.length = 0;
    this.booklistServece.getAllBooks()
      .subscribe(
        async response => {
          await JSON.parse(response).forEach(async (element: { id: number, cover: string; author: string, title: string, reviewNumber: number, rating: number }) => {
            img= await this.sanitizer.sanitize(SecurityContext.HTML, this.sanitizer.bypassSecurityTrustHtml(element.cover));
            let book: BookDTO = {
              Id: element.id,
              Author: element.author,
              Title: element.title,
              Cover: img,
              reviewNumber: element.reviewNumber,
              Rating: element.rating
            }
            this.books.push(book);
          });
        }
      );

    document.getElementById('GetAllBooks')!.style.display = 'flex';
    document.getElementById('GetRecomendedBooks')!.style.display = 'none';
  }


  async GetRecomendedBooks() {
    let img: any;
    this.recomendetBooks.length = 0;

    this.booklistServece.RecommendedBook('horror')
      .subscribe(
        response => {
          JSON.parse(response).forEach(async (element: { id: number, cover: string; author: string, title: string, reviewNumber: number, rating: number }) => {
            img= await this.sanitizer.sanitize(SecurityContext.HTML, this.sanitizer.bypassSecurityTrustHtml(element.cover));
            let book: BookDTO = {
              Id: element.id,
              Author: element.author,
              Title: element.title,
              Cover: img,
              reviewNumber: element.reviewNumber,
              Rating: element.rating
            }
            this.recomendetBooks.push(book);
          });
        }
      );

    document.getElementById('GetAllBooks')!.style.display = 'none';
    document.getElementById('GetRecomendedBooks')!.style.display = 'flex';
  }

  getBookId(id: string | number) {
    document.getElementById('addBookButton')!.style.display = 'none';
    document.getElementById('updateBookButton')!.style.display = 'inline';
    this.bookListItem.getBookByIdForEdit(Number(id));
  }

  openDialog(id: string | number) {
    this.bookListItem.getBookById(id);
  }
}

