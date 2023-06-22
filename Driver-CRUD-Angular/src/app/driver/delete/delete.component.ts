import { Component, OnInit } from '@angular/core';
import { DriverService } from '../driver.service';
import { ActivatedRoute, Router } from '@angular/router';
import { GetDriver } from '../DriverModel';
import { FormGroup, FormControl, Validators} from '@angular/forms';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.css']
})
export class DeleteComponent implements OnInit {
       
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
     

  submit(){
    this.driverService.delete(this.id).subscribe((res:any) => {
         console.log('Deleted successfully!');
         this.router.navigateByUrl('driver/index');
    })
  }
    
}
