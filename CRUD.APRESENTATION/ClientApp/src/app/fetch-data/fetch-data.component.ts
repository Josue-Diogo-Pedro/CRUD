import { Component, Inject } from '@angular/core';
 import { HttpClientModule, HttpClient } from '@angular/common/http';
 import { Http, Response, Headers } from '@angular/http';
 import { Router, ActivatedRoute } from '@angular/router';
 import { Observable } from 'rxjs/Observable';
 import 'rxjs/add/operator/map';
 import 'rxjs/add/operator/catch';
 import 'rxjs/add/observable/throw';
 import 'rxjs/Rx';
 
 @Component({
 selector: 'app-fetch-data',
 templateUrl: './fetch-data.component.html'
 })
 
 export class FetchDataComponent {
 public productList: Array<any> = [];
 myAppUrl: string = "";
 
 constructor(public http: Http, private _router: Router, @Inject('BASE_URL') baseUrl: string) {
 this.myAppUrl = baseUrl;
 this.getProductList();
 }
 
 getProductList() {
 let self = this;
 this.http.request(this.myAppUrl + '/api/Produtos')
 .subscribe((res: Response) => {
 self.productList = res.json();
 });
 }
 
 delete(productId: number, code: string) {
 if (confirm("Deseja eliminar o produto com esse Id: " + code)) {
 let self = this;
 let headers = new Headers();
 headers.append('Content-Type', 'application/json; charset=utf-8');
 this.http.delete(this.myAppUrl + "api/Products/"+ productId, { headers: headers })
 .subscribe((res: Response) => {
 self.getProductList();
 });
 }
 }
 }
