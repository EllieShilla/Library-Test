import { Component, Inject, OnInit, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BookDetailsDTO } from 'src/app/models/BookDetailsDTO.model';

@Component({
  selector: 'app-view-book',
  templateUrl: './view-book.component.html',
  styleUrls: ['./view-book.component.css']
})
export class ViewBookComponent implements OnInit {

  constructor(
    @Optional() @Inject(MAT_DIALOG_DATA) public dialogData: any,
    @Optional() public dialogRef: MatDialogRef<ViewBookComponent>,
  ) { }

  bookDetail!: BookDetailsDTO[];


  ngOnInit(): void {
    // this.bookDetail.length=0;
    this.bookDetail=this.dialogData;
  }

  onClose() {
    this.dialogData.length=0;
    this.dialogRef.close();
  }

  ngOnDestroy() {
    this.dialogRef.close();
  }

}
