import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CheckoutScreenService } from '../../services/checkout-screen.service';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { tap } from 'rxjs';
import { CommonModule } from '@angular/common';
import { AuthorizedCheckoutFormComponent } from './authorized-checkout-form/authorized-checkout-form.component';
import { trigger, transition, style, animate } from '@angular/animations';
import UserService from '../../services/user.service';

@UntilDestroy()
@Component({
  selector: 'app-checkout-screen',
  standalone: true,
  imports: [CommonModule, AuthorizedCheckoutFormComponent],
  templateUrl: './checkout-screen.component.html',
  styleUrl: './checkout-screen.component.scss',
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
export class CheckoutScreenComponent implements OnInit {
  isActivated: boolean = false;
  isAuthenticated: boolean = false;

  constructor(
    private readonly checkoutScreenService: CheckoutScreenService,
    private readonly userService: UserService,
    private readonly cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.checkoutScreenService.isActive
      .pipe(
        untilDestroyed(this),
        tap(() => {
          this.isActivated = true;
          this.cdr.markForCheck();
        })
      )
      .subscribe();

    this.userService.currentUser
      .pipe(
        untilDestroyed(this),
        tap((user) => {
          if (user != null) {
            this.isAuthenticated = true;
          }
        })
      )
      .subscribe();
  }

  closeMessage(): void {
    this.isActivated = false;
  }
}
