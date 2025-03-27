import { Routes } from '@angular/router';
import { ItemListComponent } from './components/item-list.component';
import { ItemFormComponent } from './components/item-form.component';

/**
 * Defines the routes for this application.
 */
export const routes: Routes = [
  { path: '', redirectTo: 'items', pathMatch: 'full' },
  { path: 'items', component: ItemListComponent },
  { path: 'create', component: ItemFormComponent },
];
