import { Component } from '@angular/core';
import { ProductCard } from "../product-card/product-card";

@Component({
  selector: 'app-products',
  imports: [ProductCard],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products {
  activeTab: string = 'starters'; // Default active tab

  setActive(tabName: string) {
    this.activeTab = tabName;
  }


}
