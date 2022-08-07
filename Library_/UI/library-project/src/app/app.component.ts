import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'library-project';


  currentMsgFromChild1ToChild2 : any;
  fwdMsgToSib2($event: any) { this.currentMsgFromChild1ToChild2 = $event; }
}
