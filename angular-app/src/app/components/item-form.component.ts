import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ItemsService, ItemRequestDto } from '../services/items.service';

/**
 * Component for creating or updating items by name.
 */
@Component({
  standalone: true,
  imports: [CommonModule, FormsModule],
  selector: 'app-item-form',
  template: `
    <h2>Create or Update an Item</h2>
    <div>
      <label for="itemName">Name:</label>
      <input id="itemName" [(ngModel)]="name" />
    </div>
    <div>
      <label for="itemQuantity">Quantity:</label>
      <input id="itemQuantity" type="number" [(ngModel)]="quantity" />
    </div>
    <button (click)="createOrUpdate()">Submit</button>
    <p *ngIf="message">{{ message }}</p>
  `
})
export class ItemFormComponent {
  name = '';
  quantity = 1;
  message = '';

  /**
   * Initializes the ItemFormComponent with the ItemsService.
   * @param itemsService Service for communicating with the .NET API.
   */
  constructor(private itemsService: ItemsService) {}

  /**
   * Calls createOrUpdateItem in the .NET API. If the item name exists,
   * quantity is increased; otherwise, a new item is created.
   */
  createOrUpdate(): void {
    if (!this.name.trim() || this.quantity < 1) {
      this.message = 'Invalid name or quantity.';
      return;
    }

    const dto: ItemRequestDto = { name: this.name, quantity: this.quantity };
    this.itemsService.createOrUpdateItem(dto).subscribe({
      next: (item) => {
        this.message = `Success: ${item.name} (x${item.quantity})`;
      },
      error: (err) => {
        this.message = `Error: ${err.message}`;
      }
    });
  }
}
