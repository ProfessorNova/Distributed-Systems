import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

/**
 * Represents a shopping item returned by the .NET API.
 */
export interface Item {
    id: number;
    name: string;
    quantity: number;
}

/**
 * Represents the request payload for creating or updating an item.
 */
export interface ItemRequestDto {
    name: string;
    quantity: number;
}

@Injectable({
    providedIn: 'root'
})
export class ItemsService {
    private readonly baseUrl = "http://localhost:5035";

  /**
     * Initializes the ItemsService with an HttpClient.
     * @param http The HttpClient used for making requests.
     */
    constructor(private http: HttpClient) {}

    /**
     * Retrieves all shopping items from the .NET API.
     * @returns An Observable array of items.
     */
    getItems(): Observable<Item[]> {
        return this.http.get<Item[]>(`${this.baseUrl}/items`);
    }

    /**
     * Creates or updates an item by name.
     * If the item already exists, the .NET API will update its quantity.
     * @param dto The item payload (name, quantity).
     * @returns An Observable of the newly created or updated item.
     */
    createOrUpdateItem(dto: ItemRequestDto): Observable<Item> {
        return this.http.post<Item>(`${this.baseUrl}/items`, dto);
    }

    /**
     * Retrieves a specific item by its ID.
     * @param itemId The ID of the item.
     * @returns An Observable of the item.
     */
    getItemById(itemId: number): Observable<Item> {
        return this.http.get<Item>(`${this.baseUrl}/items/${itemId}`);
    }

    /**
     * Updates an existing item by ID.
     * @param itemId The ID of the item.
     * @param dto The updated item data (name, quantity).
     * @returns An Observable of the updated item.
     */
    updateItem(itemId: number, dto: ItemRequestDto): Observable<Item> {
        return this.http.put<Item>(`${this.baseUrl}/items/${itemId}`, dto);
    }

    /**
     * Deletes an item by ID.
     * @param itemId The ID of the item.
     * @returns An Observable of void.
     */
    deleteItem(itemId: number): Observable<void> {
        return this.http.delete<void>(`${this.baseUrl}/items/${itemId}`);
    }
}
