import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Message } from 'src/app/_models/message';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
  @ViewChild('messageForm') messageForm: NgForm;
  @Input() username: string;
  @Input() message: Message[];
  messageContent: string;
  messages: Message[];

  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages() {
    this.messageService.getMessageThred(this.username).subscribe(messages => {
      this.messages = messages;
    })

  }

  sendMessage() {
    this.messageService.sendMessage(this.username, this.messageContent).subscribe(message => {

      this.messages.push(message);
      this.messageForm.reset();
    });
  }

}
