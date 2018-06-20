import { Component, OnInit } from '@angular/core';
import {Http} from '@angular/http';
import {Router} from '@angular/router';
import { Route } from '@angular/compiler/src/core';
@Component({
  selector: 'app-food-store-create',
  templateUrl: './food-store-create.component.html',
  styleUrls: ['./food-store-create.component.css']
})
export class FoodStoreCreateComponent implements OnInit {
  private formData: any = {};
  constructor(private _http: Http,private _router:Router) { }

  ngOnInit() {
  }
  submitStore(){
    console.log(this.formData);
    this._http.post("api/foodstore/create",this.formData).subscribe(r=>{
     console.log(r); 
     this._router.navigate(['foodstore']);
    },
  err=> {
    console.log(err);
  })
  }
}
