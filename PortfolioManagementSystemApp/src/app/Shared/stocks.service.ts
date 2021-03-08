import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Stocks } from './stocks.model';

@Injectable({
  providedIn: 'root'
})
export class StocksService {

  constructor(private http:HttpClient) { }

  readonly baseUrl='https://localhost:44361/api/Stocks'

  public list:Stocks[];

  getStocks(){
    this.http.get(this.baseUrl)
    .toPromise()
    .then(res=> this.list = res as Stocks[]);
  }
}
