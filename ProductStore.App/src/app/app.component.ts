import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { LayoutModule } from '@angular/cdk/layout';
import ProductCardComponent from '../app/shared/components/product-list/product-card/product-card.component';
import CheckoutScreenComponent from './shared/components/checkout-screen/checkout-screen.component';
import FooterComponent from './shared/components/footer/footer.component';
import MessageScreenComponent from './shared/components/message-screen/message-screen.component';
import NavBarComponent from './shared/components/nav-bar/nav-bar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    NavBarComponent,
    ProductCardComponent,
    FooterComponent,
    MessageScreenComponent,
    CheckoutScreenComponent,
  ],
  providers: [HttpClientModule, LayoutModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export default class AppComponent {}
