import {ResponseCode} from "../enums/responseCode";

export class ResponseModel
{
    public ResponseCode : ResponseCode=ResponseCode.Notset;
    public ResponseMessage:string="";
    public dataset:any;
}