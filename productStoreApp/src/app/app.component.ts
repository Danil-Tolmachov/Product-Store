import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import NavBarComponent from '../components/nav-bar/nav-bar.component';
import ProductItemBriefComponent from '../components/product-list/product-item-brief/product-item-brief.component';
import FooterComponent from '../components/footer/footer.component';
import UserService from '../services/user.service';
import ProductService from '../services/product.service';
import CategoryService from '../services/category.service';
import { IUser } from '../interfaces/IUser';
import MessageScreenComponent from '../components/message-screen/message-screen.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    NavBarComponent,
    ProductItemBriefComponent,
    FooterComponent,
    HttpClientModule,
    MessageScreenComponent,
  ],
  providers: [HttpClientModule, UserService, ProductService, CategoryService],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export default class AppComponent implements OnInit {
  currentUser: IUser | null = null;

  constructor(private readonly userService: UserService) {}

  ngOnInit(): void {
    // Refresh session
    const expiration = this.userService.getExpiration();
    if (expiration !== null) {
      if (expiration <= new Date()) {
        this.userService.refreshSession().subscribe();
      }
    }
  }
}
