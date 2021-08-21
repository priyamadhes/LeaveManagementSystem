import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators,FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginForm=this.formBuilder.group(
    {
      emailid: new FormControl(''),
      password: new FormControl('')
    //emailid:['',[Validators.email,Validators.required]],
    //password:['',Validators.required]
     //emailid:[],password:[]
    }
  )

 

  constructor(private formBuilder:FormBuilder,private userServcie:UserService,private router:Router) { }

  ngOnInit(): void {
  }
  onSubmit()
  {
    console.log(this.loginForm.value);
    
    
    let email = this.loginForm.controls["emailid"].value;

    let password = this.loginForm.controls["password"].value;

    this.userServcie.login(email,password).subscribe((data:any)=>{
        console.log("response",data);
        if(data.responseCode=1)
        {
          localStorage.setItem("userInfo",JSON.stringify(data.dataset));
          this.router.navigate(["/user-management"]);
        }

       
    },error=>{


    }   
    
    );
  }

}
