import { Component, OnInit } from '@angular/core';
import { DriverService } from '../driver.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators} from '@angular/forms';
      
@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {
     
  form!: FormGroup;
     
 
  constructor(
    public driverService: DriverService,
    private router: Router
  ) { }
     

  ngOnInit(): void {
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
    this.driverService.create(this.form.value).subscribe((res:any) => {
         console.log('Driver created successfully!');
         this.router.navigateByUrl('driver/index');
    })
  }
   
}