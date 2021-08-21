import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators,FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { RequestleaveService } from '../services/requestleave.service';


@Component({
  selector: 'app-request-leave',
  templateUrl: './request-leave.component.html',
  styleUrls: ['./request-leave.component.scss']
})
export class RequestLeaveComponent implements OnInit {

  minDate: Date;
  leaverequestForm:FormGroup;
  
  // Leave Type
  LeaveType: any = ['Sick Leave', 'Annual Leave', 'Maternity Leave', 'Settling Leave'];
  

  constructor(private formBuilder:FormBuilder,private requestleave:RequestleaveService,private router:Router) { 
    this.minDate = new Date();
  }

  ngOnInit() {
    this.leaverequestForm = new FormGroup({
      fromDate : new FormControl(),
      toDate:new FormControl(),
      leavetype:new FormControl(),
      LeaveDetail:new FormControl()
     
    });
  }

 

  changeSuit(e) {
    this.leaverequestForm.get('leavetype').setValue(e.target.value, {
       onlySelf: true
    })
  }

  onSubmit()
  {
    console.log(this.leaverequestForm.value);
    let fromDate = this.leaverequestForm.get('fromDate').value;
    let toDate = this.leaverequestForm.get('toDate').value;
    let leavetype = this.leaverequestForm.get('leavetype').value;
    let LeaveDetail = this.leaverequestForm.get('LeaveDetail').value;

    let userinfo = JSON.parse(localStorage.getItem("userInfo"));

    let email = userinfo.email;

    this.requestleave.CreateLeaveRequest(email,fromDate,toDate,leavetype,LeaveDetail).subscribe((data:any)=>{
      console.log("response",data);
      if(data.responseCode=1)
      {
       // localStorage.setItem("userInfo",JSON.stringify(data.dataset));
        //this.router.navigate(["/user-management"]);
      }

     
  },error=>{


  }   
  
  );

  }

}
