using equipment_lease_api.Models;
using equipment_lease_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceController : BaseController
    {
        [HttpGet("leases/{leaseId}/invoices")]
        public ActionResult GetLeaseInvoices(string leaseId)
        {
            return Ok(LeaseInvoiceQuery.GetLeaseInvoices(leaseId));
        }

        [HttpGet("leases/{leaseId}")]
        public ActionResult GetLeaseDetailsForInvoicing(string leaseId, DateTime startDate, DateTime endDate)
        {
            return Ok(LeaseInvoiceQuery.GetLeaseDetailsForInvoicing(leaseId, startDate, endDate));
        }
        

        [HttpGet("search")]
        public ActionResult SearchForInvoices(string searchText)
        {
            return Ok(LeaseInvoiceQuery.SearchLeaseInvoice(searchText));
        }

        [HttpGet("{invoiceId}")]
        public ActionResult GetInvoiceDetail(string invoiceId)
        {
            return Ok(LeaseInvoiceQuery.GetInvoiceDetail(invoiceId));
        }

        [HttpPost]
        public ActionResult CreateInvoice([FromBody] LeaseInvoiceRequest invoiceRequest)
        {
            return Ok(LeaseInvoiceQuery.CreateInvoice(GetUserId(), invoiceRequest));
        }
    }
}
