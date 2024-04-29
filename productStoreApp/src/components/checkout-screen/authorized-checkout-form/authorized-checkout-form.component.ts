import { Component, Host } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import ButtonComponent from '../../button/button.component';
import { CheckoutScreenComponent } from '../checkout-screen.component';

@Component({
  selector: 'app-authorized-checkout-form',
  standalone: true,
  imports: [ReactiveFormsModule, ButtonComponent],
  templateUrl: './authorized-checkout-form.component.html',
  styleUrl: './authorized-checkout-form.component.scss',
})
export class AuthorizedCheckoutFormComponent {
  checkoutForm = this.formBuilder.group({
    comment: null,
  });

  constructor(
    private readonly formBuilder: FormBuilder,
    @Host() readonly checkoutScreen: CheckoutScreenComponent
  ) {}

  onSubmit(): void {
    if (this.checkoutForm.valid) {
      this.checkoutScreen.isActivated = false;
      this.checkoutForm.reset();

      throw new Error('Method not implemented.');
    }
  }
}
