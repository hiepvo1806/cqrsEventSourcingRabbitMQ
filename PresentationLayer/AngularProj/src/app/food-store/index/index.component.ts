import { Component, OnInit } from '@angular/core';
import {Http} from '@angular/http';
import {Router} from '@angular/router';
@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  constructor(private _http: Http, private _router:Router) { }
  private data: Array<any> = [];
  ngOnInit() {
    console.log('ngOnInit');
    this.resetData();
  }

  resetData(){
    this._http.get('api/foodstore/index').subscribe(r=> 
      {
        this.data = r.json();
      },
      err=> {
        console.log(err)});
  }

  delete(id){
    this._http.get('api/foodstore/delete/'+ id).subscribe(r=> 
      {
        this.resetData();
      },
      err=> {
        console.log(err)});
  }

  edit(id){
    this._router.navigate(['foodstore','edit',id]);
  }

}
