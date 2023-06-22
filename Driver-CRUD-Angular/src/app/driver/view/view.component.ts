import { Component, OnInit } from '@angular/core';
import { DriverService } from '../driver.service';
import { ActivatedRoute, Router } from '@angular/router';
import { GetDriver } from '../DriverModel';
     
@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css']
})
export class ViewComponent implements OnInit {
      
  id!: string;
  getDriver!: GetDriver;
     
 
  constructor(
    public driverService: DriverService,
    private route: ActivatedRoute,
    private router: Router
   ) { }
     

  ngOnInit(): void {
    this.id = this.route.snapshot.params['Id'];
         
    this.driverService.find(this.id).subscribe((data: GetDriver)=>{
      this.getDriver = data;
    });
  }
     
}