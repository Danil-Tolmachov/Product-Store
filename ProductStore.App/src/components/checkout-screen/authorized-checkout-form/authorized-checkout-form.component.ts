import { Component, Host } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { UntilDestroy } from '@ngneat/until-destroy';
import { switchMap, take } from 'rxjs';
import ButtonComponent from '../../button/button.component';
import CheckoutScreenComponent from '../checkout-screen.component';
import OrderService from '../../../services/order.service';
import CartService from '../../../services/cart.service';

@UntilDestroy()
@Component({
  selector: 'app-authorized-checkout-form',
  standalone: true,
  imports: [ReactiveFormsModule, ButtonComponent],
  templateUrl: './authorized-checkout-form.component.html',
  styleUrl: './authorized-checkout-form.component.scss',
})
export default class AuthorizedCheckoutFormComponent {
  checkoutForm = this.formBuilder.group({
    comment: null,
  });

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly orderService: OrderService,
    private readonly cartService: CartService,
    @Host() readonly checkoutScreen: CheckoutScreenComponent
  ) {}

  onSubmit(): void {
    if (this.checkoutForm.valid) {
      const comment: string | null = this.checkoutForm.get(['comment'])?.value;
      this.orderService
        .submitCart({ comment })
        .pipe(
          take(1),
          switchMap(() => {
            return this.cartService.getCart();
          })
        )
        .subscribe({
          complete: () => {
            this.checkoutScreen.isActivated = false;
            this.checkoutForm.reset();
          },
        });
    }
  }
}
