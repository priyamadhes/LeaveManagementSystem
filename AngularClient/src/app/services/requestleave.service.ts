import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ResponseModel } from '../Models/responseModel';
import {map} from 'rxjs/operators';
import { ResponseCode } from '../enums/responseCode';



@Injectable({
  providedIn: 'root'
})

export class RequestleaveService {

  private readonly LeaveURL:string="https://localhost:44346/api/Leave/";

  constructor(private httpClient:HttpClient) { }

  public CreateLeaveRequest(email:string,fromDate:string,toDate:string,leavetype:string,LeaveDetail:string)
  {
        const body ={
          Email:email,
          FromDate:fromDate,
          ToDate:toDate,
          LeaveType:leavetype,
          LeaveDetail:LeaveDetail,
        }

       return this.httpClient.post<ResponseModel>(this.LeaveURL+"RequestLeave",body);
  }

}
