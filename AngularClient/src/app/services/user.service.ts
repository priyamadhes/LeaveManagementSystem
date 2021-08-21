import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ResponseModel } from '../Models/responseModel';
import {map} from 'rxjs/operators';
import { ResponseCode } from '../enums/responseCode';
import { User } from '../Models/user';


@Injectable({
  providedIn: 'root'
})
export class UserService {

   private readonly baseURL:string="https://localhost:44346/api/User/";

  
  constructor(private httpClient:HttpClient) { }

  public login(email:string,password:string)
  {
        const body ={
          Email:email,
          Password:password
        }

       return this.httpClient.post<ResponseModel>(this.baseURL+"Login",body);
  }

 

  public getAlluser()
  {
       let x = localStorage.getItem("userInfo");
        let userinfo = JSON.parse(localStorage.getItem("userInfo"));

        const headers = new HttpHeaders({

          'Authorization': `Bearer ${userinfo?.token}`
          });
        return this.httpClient.get<ResponseModel>(this.baseURL + "GetAllUser",{headers:headers}).pipe(map(res=>
          {
            let userList = new Array<User>();
            if(res.ResponseCode=1)
            {
              if(res.dataset)
              {
                res.dataset.map((x:User)=>
                {
                  userList.push(new User(x.fullName,x.email,x.userName));
                })
              }
              
            }

            return userList;
        }));
  }





}
