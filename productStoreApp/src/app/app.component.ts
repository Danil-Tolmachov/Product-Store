import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {
  HttpClientModule,
} from '@angular/common/http';
import NavBarComponent from '../components/nav-bar/nav-bar.component';
import ProductItemBriefComponent from '../components/product-list/product-item-brief/product-item-brief.component';
import FooterComponent from '../components/footer/footer.component';
import MessageScreenComponent from '../components/message-screen/message-screen.component';
import UserService from '../services/user.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    NavBarComponent,
    ProductItemBriefComponent,
    FooterComponent,
    MessageScreenComponent,
  ],
  providers: [HttpClientModule, UserService],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export default class AppComponent {}
