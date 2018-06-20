import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { IndexComponent } from './index/index.component';
import { HttpModule } from '@angular/http';
import { FoodStoreCreateComponent } from './food-store-create/food-store-create.component';
import { FormsModule } from '@angular/forms';
import { FoodStoreEditComponent } from './food-store-edit/food-store-edit.component';
const routes: Routes = [
  {
    "path": "create",
    "component": FoodStoreCreateComponent
  },
  {
    "path": "edit/:id",
    "component": FoodStoreEditComponent
  },
  {
    "path": "",
    "component": IndexComponent
  }
];
@NgModule({
  imports: [
    CommonModule,RouterModule.forChild(routes),HttpModule ,FormsModule
  ],
  declarations: [IndexComponent, FoodStoreCreateComponent, FoodStoreEditComponent]
})
export class FoodStoreModule { }
