import { Routes } from '@angular/router';
import { AboutUsComponent } from '../about-us/about-us.component';
import { DeliveryComponent } from '../delivery/delivery.component';
import { MainComponent } from '../main/main.component';

export const routes: Routes = [
    { path: '', component: MainComponent },
    { path: 'delivery', component: DeliveryComponent },
    { path: 'about-us', component: AboutUsComponent }
];
