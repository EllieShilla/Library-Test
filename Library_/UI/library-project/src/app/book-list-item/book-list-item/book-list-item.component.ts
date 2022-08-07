import { Component, Inject, OnInit, Optional, SecurityContext } from '@angular/core';
import { BookDetailsDTO } from 'src/app/models/BookDetailsDTO.model';
import { ReviewDTO } from 'src/app/models/ReviewDTO.model';
import { BookListItemService } from 'src/app/services/book-list-item.service';
import { DomSanitizer } from '@angular/platform-browser';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DataShareService } from 'src/app/services/data-share.service';
import { ViewBookComponent } from 'src/app/view-book/view-book/view-book.component';

@Component({
  selector: 'app-book-list-item',
  templateUrl: './book-list-item.component.html',
  styleUrls: ['./book-list-item.component.css'],
})
export class BookListItemComponent implements OnInit {
  constructor(@Optional() @Inject(MAT_DIALOG_DATA) public dialogData: any,
    @Optional() public dialogRef: MatDialogRef<BookListItemComponent>,
    private bookListItemServices: BookListItemService,
    private sanitizer: DomSanitizer,
    private dataShare: DataShareService,
    public dialog: MatDialog,
  ) { }
  reviewsDTO: ReviewDTO[] = [];
  imagePath!: any;

  bookDetail: BookDetailsDTO[] = [];

  ngOnInit(): void {
    this.getBookById(this.dialogData);
  }


  ngOnDestroy() {
    this.dialogRef.close();
  }

  async getBookById(id: any) {
    let img: any;
    this.bookListItemServices.getBookById(Number(id))
      .subscribe(
        async response => {
          let bookBuff = JSON.parse(response);
          img = await this.sanitizer.sanitize(SecurityContext.HTML, this.sanitizer.bypassSecurityTrustHtml(bookBuff.cover));
          bookBuff.reviews.forEach((review: any) => {
            let rev: ReviewDTO = {
              Id: review.id,
              Reviwer: review.reviwer,
              Message: review.message
            }
            this.reviewsDTO.push(rev);
          }
          )
          let book: BookDetailsDTO = {
            Id: bookBuff.id,
            Title: bookBuff.title,
            Author: bookBuff.author,
            Genre: bookBuff.genre,
            Content: bookBuff.content,
            Cover: img,
            Reviews: this.reviewsDTO,
            Rating: bookBuff.rating
          }
          this.bookDetail.push(book);
        }
      )
      this.openDialog(this.bookDetail);
  };

  setReviews(Reviews: ReviewDTO[]): Promise<ReviewDTO[]> {
    return new Promise((resolve, reject) => {
      let reviews = Reviews;
      resolve(reviews);
    });
  }

  onClose() {
    this.dialogRef.close();
  }

  getBookByIdForEdit(id: number) {
    this.bookListItemServices.getBookById(Number(id))
      .subscribe(
        (response: any) => {
          (<HTMLInputElement>document.getElementById("title")).value = JSON.parse(response).title;
          (<HTMLInputElement>document.getElementById("title")).dispatchEvent(new Event('input', { bubbles: true }));
          (<HTMLInputElement>document.getElementById("genre")).value = JSON.parse(response).genre;
          (<HTMLInputElement>document.getElementById("genre")).dispatchEvent(new Event('input', { bubbles: true }));
          (<HTMLInputElement>document.getElementById("author")).value = JSON.parse(response).author;
          (<HTMLInputElement>document.getElementById("author")).dispatchEvent(new Event('input', { bubbles: true }));
          (<HTMLInputElement>document.getElementById("content")).value = JSON.parse(response).content;
          (<HTMLInputElement>document.getElementById("content")).dispatchEvent(new Event('input', { bubbles: true }));

          this.dataShare.sendId(JSON.parse(response).id);
          this.dataShare.updateData(JSON.parse(response).cover);
        });
    (<HTMLInputElement>document.getElementById("formLabel")).innerHTML = 'Edit Book';
  };


  openDialog(bookDetail:BookDetailsDTO[]) {
    let dialogRef = this.dialog.open(ViewBookComponent, {
      data: bookDetail
    });

    dialogRef.afterClosed().subscribe(
      result => {

      }
    )
  }
}
