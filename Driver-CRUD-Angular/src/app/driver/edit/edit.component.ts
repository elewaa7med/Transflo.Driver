import { Component, OnInit } from '@angular/core';
import { DriverService } from '../driver.service';
import { ActivatedRoute, Router } from '@angular/router';
import { GetDriver } from '../DriverModel';
import { FormGroup, FormControl, Validators} from '@angular/forms';
      
@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
       
  id!: string;
  getDriver!: GetDriver;
  form!: FormGroup;
     
 
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
       
    this.form = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl(''),
      email: new FormControl(''),
      phoneNumber: new FormControl('')
    });
  }
     

  get f(){
    return this.form.controls;
  }
     

  submit(){
    console.log(this.form.value);
    this.driverService.update(this.id, this.form.value).subscribe((res:any) => {
         console.log('Post updated successfully!');
         this.router.navigateByUrl('driver/index');
    })
  }
    
}