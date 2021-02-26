import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { StockDetails } from 'src/app/Shared/stock-details.model';
import { StockDetailsService } from 'src/app/Shared/stock-details.service';


@Component({
  selector: 'app-stock-profit-single',
  templateUrl: './stock-profit-single.component.html',
})
export class StockProfitSingleComponent implements OnInit {

  constructor(public service:StockDetailsService,private router:Router,private http:HttpClient) { }

  ngOnInit(): void {
  }

  newList: StockDetails[];

  readonly baseUrl = 'https://localhost:44361/api/StockDetails'

  private i:number;
  public buySum:number=0;
  public saleSum:number=0;
  public totalUnits:number=0;
  public currentAmount:number=0;
  public total:number=0;
  stockName= new FormControl('');

  onSubmit(){
    this.http.get(this.baseUrl)
    .toPromise()
    .then(res=> this.newList = res as StockDetails[]);

    
    for(this.i=0;this.i<this.newList.length;this.i++){
      if(this.stockName.value==this.newList[this.i].stockName){
        if(this.newList[this.i].transactionType=="buy"){
          this.buySum+=this.newList[this.i].amount*this.newList[this.i].quantity;
          this.totalUnits+=this.newList[this.i].quantity;
        }else{
          this.saleSum+=this.newList[this.i].amount*this.newList[this.i].quantity;
          this.totalUnits+=this.newList[this.i].quantity;
        }

      }
    
    }
    this.currentAmount=this.newList[this.newList.length -1].amount;
    this.total=this.buySum-this.saleSum;
  }

}
