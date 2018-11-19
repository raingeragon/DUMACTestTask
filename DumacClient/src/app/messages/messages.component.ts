import { Component, OnInit } from '@angular/core';
import { Message } from './message';
import {MessagesService} from '../services/messages-service.service'

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  message: Message = {
    Subject : "",
    Body : "",
    Recipients: new Array()
  }
  resultId : number = 0;
  recipients: string = "";
  loading: boolean = false;

  onSendClicked(): void {
    this.loading = true;
    this.message.Recipients = this.recipients.split(';');
    this.messagesService.sendMessage(this.message)
    .subscribe(
        (data: number) => {
          this.resultId=data; 
          this.loading = false;
          alert("Message was sent, id: "+ this.resultId);
        },
        error => {
          console.log(error);
          this.loading = false;
          alert("Sorry, error ocured");
        }
    );
  }


  constructor(private messagesService: MessagesService) { }

  ngOnInit() {
  }

}