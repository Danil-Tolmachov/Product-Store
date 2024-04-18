import { type Routes } from '@angular/router';
import AboutUsComponent from '../pages/about-us/about-us.component';
import DeliveryComponent from '../pages/delivery/delivery.component';
import HomeComponent from '../pages/home/home.component';
import ProductComponent from '../pages/product/product.component';
import CategoryComponent from '../pages/category/category.component';
import LoginComponent from '../pages/login/login.component';
import AuthGuard from '../guards/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' }, // Redirect from root to '/home'

  { path: 'home', component: HomeComponent },
  { path: 'category/:categoryId', component: CategoryComponent },
  { path: 'product/:productId', component: ProductComponent },
  { path: 'delivery', component: DeliveryComponent },
  { path: 'about-us', component: AboutUsComponent },
  { path: 'login', component: LoginComponent, canActivate: [AuthGuard] },
];

export default routes;
