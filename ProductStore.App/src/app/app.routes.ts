import { type Routes } from '@angular/router';
import OrdersComponent from './pages/orders/orders.component';
import AuthGuard from './core/guards/auth.guard';
import UnAuthGuard from './core/guards/unauth.guard';
import AboutUsComponent from './pages/about-us/about-us.component';
import CategoryComponent from './pages/category/category.component';
import DeliveryComponent from './pages/delivery/delivery.component';
import HomeComponent from './pages/home/home.component';
import LoginComponent from './pages/login/login.component';
import ProductComponent from './pages/product/product.component';
import ProfileComponent from './pages/profile/profile.component';
import RegistrationComponent from './pages/registration/registration.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' }, // Redirect from root to '/home'

  { path: 'home', component: HomeComponent },
  { path: 'category/:categoryId', component: CategoryComponent },
  { path: 'product/:productId', component: ProductComponent },
  { path: 'delivery', component: DeliveryComponent },
  { path: 'about-us', component: AboutUsComponent },
  { path: 'login', component: LoginComponent, canActivate: [AuthGuard] },
  {
    path: 'registration',
    component: RegistrationComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'orders',
    component: OrdersComponent,
    canActivate: [UnAuthGuard],
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [UnAuthGuard],
  },
];

export default routes;
