import { Injectable } from '@angular/core';
import{HttpClient} from '@angular/common/http'
import { StockDetails } from './stock-details.model';



@Injectable({
  providedIn: 'root'
})
export class StockDetailsService {

  constructor(private http:HttpClient) { }

  formData:StockDetails = new StockDetails();

  public list: StockDetails[];

  readonly baseUrl = 'https://localhost:44361/api/StockDetails'

  postStockDetails(){
    return this.http.post(this.baseUrl,this.formData);
  }

  putStockDetails(){
    return this.http.put(`${this.baseUrl}/${this.formData.stockId}`,this.formData);
  }

  deleteStock(id:number){
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

  refreshList(){
    this.http.get(this.baseUrl)
    .toPromise()
    .then(res=> this.list = res as StockDetails[]);
  }
}
