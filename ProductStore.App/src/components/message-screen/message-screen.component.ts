import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { animate, style, transition, trigger } from '@angular/animations';
import { Observable, tap } from 'rxjs';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import IMessageModel from '../../interfaces/models/IMessageModel';
import MessageService from '../../services/message.service';

@UntilDestroy()
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
export default class MessageScreenComponent {
  message$: Observable<IMessageModel> = this.messageService.message.pipe(
    tap((message) => {
      if (MessageScreenComponent.validateMessage(message)) {
        this.isActivated = true;
      }
    }),
    tap(() => this.cdr.markForCheck())
  );

  isActivated: boolean = false;

  constructor(
    private readonly messageService: MessageService,
    private readonly cdr: ChangeDetectorRef
  ) {
    this.message$.pipe(untilDestroyed(this)).subscribe();
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
