using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
namespace VH.jsonDB
{
    public class JsonBD_NetResponse
    {
        public string sReadData = "";
        public WebExceptionStatus errStatus = WebExceptionStatus.Success;
        public jsonDB_NetError NetStatus = jsonDB_NetError.Ok;
        public string errMesage = "";
    }
}
