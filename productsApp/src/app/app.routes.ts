import { Routes } from '@angular/router';
import { AboutUsComponent } from '../about-us/about-us.component';
import { DeliveryComponent } from '../delivery/delivery.component';
import { HomeComponent } from '../home/home.component';
import { ProductComponent } from '../product/product.component';

export const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' }, // Redirect from root to '/home'
    
    { path: 'home', component: HomeComponent },
    { path: 'product', component: ProductComponent },
    { path: 'delivery', component: DeliveryComponent },
    { path: 'about-us', component: AboutUsComponent }
];
