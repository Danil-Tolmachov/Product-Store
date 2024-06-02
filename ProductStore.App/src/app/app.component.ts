import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { LayoutModule } from '@angular/cdk/layout';
import NavBarComponent from '../components/nav-bar/nav-bar.component';
import ProductCardComponent from '../components/product-list/product-card/product-card.component';
import FooterComponent from '../components/footer/footer.component';
import MessageScreenComponent from '../components/message-screen/message-screen.component';
import CheckoutScreenComponent from '../components/checkout-screen/checkout-screen.component';

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
