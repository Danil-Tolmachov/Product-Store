import { Component, OnInit } from '@angular/core';
import { Observable, take } from 'rxjs';
import { AsyncPipe, CommonModule } from '@angular/common';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import OrderService from '../../services/order.service';
import OrderBriefComponent from './order-brief/order-brief.component';
import UserService from '../../services/user.service';
import { IOrder } from '../../interfaces/IOrder';

@UntilDestroy()
@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, AsyncPipe, OrderBriefComponent],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.scss',
})
export default class OrdersComponent implements OnInit {
  orders$: Observable<IOrder[] | null> = this.orderService.orders;

  constructor(
    private readonly orderService: OrderService,
    private readonly userService: UserService
  ) {}

  ngOnInit(): void {
    this.userService.getUser().pipe(untilDestroyed(this), take(1)).subscribe();
    this.userService.currentUser.pipe(untilDestroyed(this)).subscribe();
  }
}
