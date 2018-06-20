import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
{path: 'foodstore', loadChildren: './food-store/food-store.module#FoodStoreModule'},
{path: '', redirectTo: 'index', pathMatch: 'full'},
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }