import { Injectable } from '@angular/core';
import{HttpClient} from '@angular/common/http'
import { StockDetails } from './stock-details.model';
import { StockDetailsView } from './stock-details-view.model';
import { StockProfit } from './stock-profit.model';
import { SingleStockProfit } from './single-stock-profit.model';



@Injectable({
  providedIn: 'root'
})
export class StockDetailsService {

  constructor(private http:HttpClient) { }

  formData:StockDetails = new StockDetails();

  public list: StockDetailsView[];

  readonly baseUrl = 'https://localhost:44361/api/StockDetails'

  postStockDetails(){
    return this.http.post(this.baseUrl,this.formData);
  }

  putStockDetails(){
    return this.http.put(`${this.baseUrl}/${this.formData.transactionId}`,this.formData);
  }

  deleteStock(id:number){
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

  stockById:StockDetailsView;
  getById(id:number){
    this.http.get(`${this.baseUrl}/${id}`)
    .toPromise()
    .then(res=>this.stockById=res as StockDetailsView);
  }

  refreshList(){
    this.http.get(this.baseUrl)
    .toPromise()
    .then(res=> this.list = res as StockDetailsView[]);
  }


  public profitView: StockProfit;
  readonly profitUrl='https://localhost:44361/api/StockDetails/getprofit'

  getProfit(){
    this.http.get(this.profitUrl)
    .toPromise()
    .then(res => this.profitView=res as StockProfit);
  }


  public singleProfitView:SingleStockProfit[];
  readonly singleProfitUrl='https://localhost:44361/api/StockDetails/getprofitsingle'

  getProfitSingle(){
    this.http.get(this.singleProfitUrl)
    .toPromise()
    .then(res=>this.singleProfitView=res as SingleStockProfit[]);
  }

}
