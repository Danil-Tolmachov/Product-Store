import { Component, OnInit } from '@angular/core';
import IMessageModel from '../../interfaces/models/IMessageModel';
import MessageService from '../../services/message.service';
import { CommonModule } from '@angular/common';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-message-screen',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './message-screen.component.html',
  styleUrl: './message-screen.component.scss',
  animations: [
    trigger('fadeInOutAnimation', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('0.1s', style({ opacity: 1 })),
      ]),
      transition(':leave', [animate('0.1s', style({ opacity: 0 }))]),
    ]),
  ],
})
export default class MessageScreenComponent implements OnInit {
  isActivated: boolean = false;
  message: IMessageModel | null = null;

  constructor(private readonly messageService: MessageService) {}

  ngOnInit(): void {
    this.messageService.message.subscribe((message) => {
      if (this.validateMessage(message)) {
        this.message = message;
        this.isActivated = true;
      }
    });
  }

  closeMessage(): void {
    this.isActivated = false;
  }

  private validateMessage(message: IMessageModel): boolean {
    if (
      typeof message.header !== 'string' ||
      message.header.trim().length === 0
    ) {
      return false;
    }

    return true;
  }
}
