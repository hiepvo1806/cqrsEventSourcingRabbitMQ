import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import {Http} from '@angular/http';
@Component({
  selector: 'app-food-store-edit',
  templateUrl: './food-store-edit.component.html',
  styleUrls: ['./food-store-edit.component.css']
})
export class FoodStoreEditComponent implements OnInit {
  private id:string;
  private formData:any = {};
  constructor(protected activeRoute: ActivatedRoute, private _http:Http, private _router:Router) { 
    
    this.activeRoute.params.subscribe((param: Params) => {
      this.id = param['id'].toLowerCase();
      this.getData(this.id);
    });
  }

  ngOnInit() {
  }

  getData(id:string){
    this._http.get("api/foodstore/get/"+id).subscribe(r=> {
      this.formData = r.json();
    })
  }

  submitStore(){
    console.log(this.formData);
    this._http.post("api/foodstore/edit",this.formData).subscribe(r=>{
     console.log(r); 
     this._router.navigate(['foodstore']);
    },
    err=> {
      console.log(err);
    })
  }

}
