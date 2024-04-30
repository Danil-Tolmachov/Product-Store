import { Component, OnInit } from '@angular/core';
import OrderService from '../../services/order.service';
import { Observable, tap } from 'rxjs';
import { IOrder } from '../../interfaces/IOrder';
import { AsyncPipe, CommonModule } from '@angular/common';
import { OrderBriefComponent } from './order-brief/order-brief.component';
import UserService from '../../services/user.service';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Router } from '@angular/router';

@UntilDestroy()
@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, AsyncPipe, OrderBriefComponent],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.scss',
})
export class OrdersComponent implements OnInit {
  orders$: Observable<IOrder[] | null> = this.orderService.orders;
  constructor(
    private readonly orderService: OrderService,
    private readonly userService: UserService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.userService.currentUser
      .pipe(
        untilDestroyed(this),
        tap((user) => {
          if (user == null) {
            this.router.navigate(['home']);
          }
        })
      )
      .subscribe();
  }
}
