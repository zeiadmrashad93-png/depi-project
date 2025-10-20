import { Component,OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductDetailsService } from '../../../services/product-details';


@Component({
  selector: 'app-product-card',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './product-card.html',
  styleUrl: './product-card.css'
})
export class ProductCard implements OnInit {

  recipes: any[] = [];
Math: any;

  constructor(private recipeService: ProductDetailsService) {}

  ngOnInit(): void {
    this.recipeService.getRecipes().subscribe({
      next: (data) => {
        this.recipes = data.recipes || []; // ✅ ensure it’s not undefined
        console.log(this.recipes);
      },
      error: (err) => console.error('Error fetching recipes:', err)
    });
  }


}
