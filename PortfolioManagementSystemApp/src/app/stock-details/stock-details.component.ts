import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { StockDetails } from '../Shared/stock-details.model';
import { StockDetailsService } from '../Shared/stock-details.service';

@Component({
  selector: 'app-stock-details',
  templateUrl: './stock-details.component.html',
})
export class StockDetailsComponent implements OnInit {

  constructor(public service:StockDetailsService,private toastr:ToastrService,private router:Router) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord:StockDetails){
    this.service.formData = Object.assign({},selectedRecord);
    this.router.navigate(['add']);
  }

  onDelete(id:number){
    if(confirm('Are you sure to delete this record?'))
    {
    this.service.deleteStock(id)
    .subscribe(
      res=>{
        this.service.refreshList();
        this.toastr.error("Deleted Successfully",'Stock Detail Registration');
      },
      err=>{console.log(err)}
    )
    }
  }

}
