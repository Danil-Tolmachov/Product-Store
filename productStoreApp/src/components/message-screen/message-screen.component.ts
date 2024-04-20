import {
  ChangeDetectionStrategy,
  Component,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { animate, style, transition, trigger } from '@angular/animations';
import IMessageModel from '../../interfaces/models/IMessageModel';
import MessageService from '../../services/message.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-message-screen',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './message-screen.component.html',
  styleUrl: './message-screen.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
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
export default class MessageScreenComponent implements OnInit, OnDestroy {
  private messageSubscription: Subscription | null = null;
  message: IMessageModel | null = null;

  isActivated: boolean = false;

  constructor(private readonly messageService: MessageService) {}

  ngOnInit(): void {
    this.messageSubscription = this.messageService.message.subscribe(
      (message) => {
        if (MessageScreenComponent.validateMessage(message)) {
          this.message = message;
          this.isActivated = true;
        }
      }
    );
  }

  ngOnDestroy(): void {
    this.messageSubscription?.unsubscribe();
  }

  closeMessage(): void {
    this.isActivated = false;
  }

  private static validateMessage(message: IMessageModel): boolean {
    if (
      typeof message.header !== 'string' ||
      message.header.trim().length === 0
    ) {
      return false;
    }

    return true;
  }
}
