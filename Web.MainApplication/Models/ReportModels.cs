using Repository.Application.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainApplication.ReportModels
{
    public class ReportModels
    {
    }
    public class DeliveryRequests
    {
        public string DeliveryRequestNumber { get; set; }
        public long ContractId { get; set; }
        public DateTime DeliveryRequestTime { get; set; }
        public string ContractNumber { get; set; }

    }
}