import { Routes } from '@angular/router';
import { Products } from './user/pages/products/products';

export const routes: Routes = [
    { path: 'products', component:Products },
    { path: '', redirectTo: '/products', pathMatch: 'full' },
    { path: '**', redirectTo: '/products' }

];
