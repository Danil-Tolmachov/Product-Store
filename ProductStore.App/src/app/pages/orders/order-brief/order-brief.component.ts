import { Component, Input } from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { RouterLink } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { IOrder } from '../../../core/interfaces/IOrder';
import MessageService from '../../../core/services/message.service';
import OrderService from '../../../core/services/order.service';
import ButtonComponent from '../../../shared/ui/button/button.component';

@UntilDestroy()
@Component({
  selector: 'app-order-brief',
  standalone: true,
  imports: [RouterLink, ButtonComponent, ButtonComponent],
  templateUrl: './order-brief.component.html',
  styleUrl: './order-brief.component.scss',
})
export default class OrderBriefComponent {
  @Input() order: IOrder | null = null;

  constructor(
    private readonly orderService: OrderService,
    private readonly messageService: MessageService
  ) {}

  cancelButtonClick(orderId: number): void {
    this.orderService
      .cancelOrder(orderId)
      .pipe(untilDestroyed(this))
      .subscribe({
        error: (error) => {
          if (error instanceof HttpErrorResponse) {
            this.cancelOrderErrorHandler(error);
          }

          console.error(error);
        },
      });
  }

  cancelOrderErrorHandler(error: HttpErrorResponse): void {
    if (error.status === 0) {
      this.messageService.showMessage({
        header: 'Cancelation failed',
        message: ['Server connection error.'],
      });
      return;
    }

    if (error.status === 400) {
      this.messageService.showMessage({
        header: 'Cancelation failed',
        message: ['Invalid action.'],
      });
      return;
    }

    if (error.status === 401) {
      this.messageService.showMessage({
        header: 'Cancelation failed',
        message: ['Authentication failed.'],
      });
      return;
    }

    if (error.status === 500) {
      this.messageService.showMessage({
        header: 'Cancelation failed',
        message: ['Server error.'],
      });
    }
  }
}
