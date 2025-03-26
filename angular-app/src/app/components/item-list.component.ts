import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ItemsService, Item } from '../services/items.service';

/**
 * Component for displaying a list of shopping items.
 */
@Component({
  standalone: true,
  imports: [CommonModule],
  selector: 'app-item-list',
  template: `
    <h2>Item List</h2>
    <button (click)="refresh()">Refresh</button>
    <ul>
      <li *ngFor="let item of items">
        {{ item.name }} (x{{ item.quantity }})
        <button (click)="deleteItem(item.id)">Delete</button>
      </li>
    </ul>
  `
})
export class ItemListComponent implements OnInit {
  items: Item[] = [];

  /**
   * Initializes the ItemListComponent with the ItemsService.
   * @param itemsService Service for communicating with the .NET API.
   */
  constructor(private itemsService: ItemsService) {}

  /**
   * OnInit lifecycle hook to load items on component initialization.
   */
  ngOnInit(): void {
    this.refresh();
  }

  /**
   * Calls the .NET API to fetch all items.
   */
  refresh(): void {
    this.itemsService.getItems().subscribe({
      next: (data) => {
        this.items = data;
      },
      error: (err) => {
        console.error('Error fetching items:', err);
      }
    });
  }

  /**
   * Deletes an item by its ID, then refreshes the list.
   * @param itemId The ID of the item to delete.
   */
  deleteItem(itemId: number): void {
    this.itemsService.deleteItem(itemId).subscribe({
      next: () => {
        this.refresh();
      },
      error: (err) => {
        console.error('Error deleting item:', err);
      }
    });
  }
}
