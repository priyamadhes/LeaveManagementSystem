using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using LmsWebApi.Enums;

namespace LmsWebApi.Models
{
    public class ResponseModel
    {
        public ResponseModel(ResponseCode responsecode,string responseMessage,object dataSet)
        {
            ResponseCode = responsecode;
            ResponseMessage = responseMessage;
            Dataset = dataSet;
        }
        public ResponseCode ResponseCode { get; set; }

        public string ResponseMessage { get; set; }

        public object Dataset { get; set; }



    }
}
