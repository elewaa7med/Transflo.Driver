import { Component, OnInit } from '@angular/core';
import { DriverService } from '../driver.service';
import { Router } from '@angular/router';
import { ListResponseViewMode, GetDriver } from '../DriverModel';
       
@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  GetDriver: GetDriver[] = [];
  itemCount :number = 0;
  constructor(
    public driverService: DriverService,
    private router: Router
  ) { }
  ngOnInit(): void {
    this.driverService.getAll().subscribe((responce: ListResponseViewMode)=>{
      console.log(responce);
      this.GetDriver = responce.data;
      this.itemCount = responce.ItemCount
      console.log(responce);
    })  
  }
  submit(){
    this.driverService.create100().subscribe((res:any) => {
         console.log('100 Driver created successfully!');
         window.location.reload();
    })
  }
}