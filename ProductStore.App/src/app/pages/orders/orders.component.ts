import { Component, OnInit } from '@angular/core';
import { Observable, map, take } from 'rxjs';
import { AsyncPipe, CommonModule } from '@angular/common';
import OrderBriefComponent from './order-brief/order-brief.component';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { IOrder } from '../../core/interfaces/IOrder';
import OrderService from '../../core/services/order.service';
import UserService from '../../core/services/user.service';

@UntilDestroy()
@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, AsyncPipe, OrderBriefComponent],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.scss',
})
export default class OrdersComponent implements OnInit {
  orders$: Observable<IOrder[] | null> = this.orderService.orders.pipe(
    map((orders) => orders?.reverse() ?? [])
  );

  constructor(
    private readonly orderService: OrderService,
    private readonly userService: UserService
  ) {}

  ngOnInit(): void {
    this.userService.getUser().pipe(untilDestroyed(this), take(1)).subscribe();
    this.userService.currentUser.pipe(untilDestroyed(this)).subscribe();
  }
}
