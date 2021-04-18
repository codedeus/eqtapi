using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using equipment_lease_api.Entities;
using equipment_lease_api.Models;
using static equipment_lease_api.Helpers.Constants.Strings;

namespace equipment_lease_api.Services
{
    public static class LeaseInvoiceQuery
    {
        public static object GetLeaseInvoices(string leaseId)
        {
            using(var dbContext = new AppDataContext())
            {
                var invoices = (from invoice in dbContext.LeaseInvoices
                                where invoice.IsDeleted == false && invoice.AssetLeaseId == leaseId
                                select new
                                {
                                    invoice.InvoicePeriod,
                                    invoice.Id,
                                    invoice.InvoiceNumber,
                                    invoice.TotalAmount,
                                    InvoiceDate = invoice.CreationDate
                                }).ToList();
                return invoices;
            }
        }

        public static object GetLeaseDetailsForInvoicing(string leaseId, DateTime startDate, DateTime endDate)
        {
            using (var dbContext = new AppDataContext())
            {
                var leaseUpdates = (from ntr in dbContext.AssetLeaseUpdateEntries
                                    where ntr.IsDeleted == false
                                    /*&& ntr.AssetLeaseUpdate.InvoiceRaised == false*/
                                    && ntr.AssetLeaseEntry.IsDeleted == false
                                    && ntr.AssetLeaseUpdate.AssetLeaseId == leaseId
                                    && ntr.UpdateDate.Date >= startDate.Date
                                    && ntr.UpdateDate.Date <= endDate.Date
                                    //group ntr by ntr.AssetLeaseEntryId
                                    select new
                                    {
                                        ntr.Id,
                                        ntr.Comment,
                                        ntr.AssetStatus,
                                        ntr.AssetLeaseEntryId,
                                        ntr.AssetLeaseEntry.LeaseCost,
                                        AssetItem = ntr.AssetLeaseEntry.AssetItem.Asset.Name,
                                        AssetBrand = ntr.AssetLeaseEntry.AssetItem.AssetBrand.Name,
                                        AssetGroup = ntr.AssetLeaseEntry.AssetItem.AssetGroup.Name,
                                        AssetModel = ntr.AssetLeaseEntry.AssetItem.AssetModel.Name,
                                        AssetSubGroup = ntr.AssetLeaseEntry.AssetItem.AssetSubGroup.Name,
                                        AssetType = ntr.AssetLeaseEntry.AssetItem.AssetType.Name,
                                        AssetCapacity = ntr.AssetLeaseEntry.AssetItem.Capacity.Name,
                                        AssetCode = ntr.AssetLeaseEntry.AssetItem.Code,
                                        AssetDimension = ntr.AssetLeaseEntry.AssetItem.Dimension.Name,
                                        AssetSerialNumber = ntr.AssetLeaseEntry.AssetItem.SerialNumber,
                                        ntr.AssetLeaseEntry.AssetItemId,
                                        DownTime = ntr.AssetStatus != AssetStatus.OPERATIONAL ? 1 : 0,
                                        UpTime = ntr.AssetStatus == AssetStatus.OPERATIONAL ? 1 : 0
                                    } into main
                                    group main by main.AssetLeaseEntryId into s
                                    select new
                                    {
                                        AssetLeaseEntryId = s.Key,
                                        AssetItemId = s.Max(s => s.AssetItemId),
                                        LeaseCost = s.Max(s => s.LeaseCost),
                                        DownTime = s.Sum(d => d.DownTime),
                                        Description = s.Max(s => s.AssetItem),
                                        AssetCode = s.Max(s => s.AssetCode),
                                        AssetBrand = s.Max(s => s.AssetBrand),
                                        AssetGroup = s.Max(s => s.AssetGroup),
                                        AssetModel = s.Max(s => s.AssetModel),
                                        AssetSubGroup = s.Max(s => s.AssetSubGroup),
                                        AssetType = s.Max(s => s.AssetType),
                                        AssetCapacity = s.Max(s => s.AssetCapacity),
                                        AssetDimension = s.Max(s => s.AssetDimension),
                                        AssetSerialNumber = s.Max(s => s.AssetSerialNumber),
                                        UpTime = s.Sum(d => d.UpTime)
                                    }).ToList();
                return leaseUpdates;
            }
        }
        public static object SearchLeaseInvoice(string searchText)
        {
            using (var dbContext = new AppDataContext())
            {
                var invoices = (from invoice in dbContext.LeaseInvoices
                                where invoice.IsDeleted == false && invoice.InvoiceNumber.Contains(searchText.Trim().ToLower())
                                select new
                                {
                                    invoice.InvoicePeriod,
                                    invoice.Id,
                                    invoice.InvoiceNumber,
                                    invoice.TotalAmount,
                                    InvoiceDate = invoice.CreationDate,
                                    Project = invoice.AssetLease.Project.Name,
                                    Location = invoice.AssetLease.Project.Location.Name,
                                    invoice.AssetLease.LeaseNumber
                                }).ToList();
                return invoices;
            }
        }
        public static InvoiceDetail GetInvoiceDetail(string invoiceId)
        {
            using(var dbContext = new AppDataContext())
            {

                var detail = (from invoice in dbContext.LeaseInvoices
                              where invoice.IsDeleted == false
                              && invoice.Id == invoiceId
                              select new InvoiceDetail
                              {
                                  Id = invoice.Id,
                                  InvoiceNumber = invoice.InvoiceNumber,
                                  InvoicePeriod = invoice.InvoicePeriod,
                                  AssetLeaseId = invoice.AssetLeaseId,
                                  Project = invoice.AssetLease.Project.Name,
                                  Location = invoice.AssetLease.Project.Location.Name,
                                  InvoiceDate = invoice.CreationDate,
                                  Subsidiary = invoice.AssetLease.Project.Subsidiary.Name
                              }).FirstOrDefault();

                var result = (from ntr in dbContext.AssetLeaseUpdateEntries
                where ntr.IsDeleted == false
                && ntr.AssetLeaseEntry.IsDeleted == false
                && ntr.AssetLeaseUpdate.LeaseInvoiceId == invoiceId
                                             //group ntr by ntr.AssetLeaseEntryId
                                             select new
                                             {
                                                 ntr.Id,
                                                 ntr.Comment,
                                                 ntr.AssetStatus,
                                                 ntr.AssetLeaseEntryId,
                                                 ntr.AssetLeaseEntry.LeaseCost,
                                                 AssetItem = ntr.AssetLeaseEntry.AssetItem.Asset.Name,
                                                 AssetBrand = ntr.AssetLeaseEntry.AssetItem.AssetBrand.Name,
                                                 AssetGroup = ntr.AssetLeaseEntry.AssetItem.AssetGroup.Name,
                                                 AssetModel = ntr.AssetLeaseEntry.AssetItem.AssetModel.Name,
                                                 AssetSubGroup = ntr.AssetLeaseEntry.AssetItem.AssetSubGroup.Name,
                                                 AssetType = ntr.AssetLeaseEntry.AssetItem.AssetType.Name,
                                                 AssetCapacity = ntr.AssetLeaseEntry.AssetItem.Capacity.Name,
                                                 AssetCode = ntr.AssetLeaseEntry.AssetItem.Code,
                                                 AssetDimension = ntr.AssetLeaseEntry.AssetItem.Dimension.Name,
                                                 AssetSerialNumber = ntr.AssetLeaseEntry.AssetItem.SerialNumber,
                                                 ntr.AssetLeaseEntry.AssetItemId,
                                                 DownTime = ntr.AssetStatus != AssetStatus.OPERATIONAL ? 1 : 0,
                                                 UpTime = ntr.AssetStatus == AssetStatus.OPERATIONAL ? 1 : 0
                                             } into main
                                             group main by main.AssetLeaseEntryId into s
                                             select new InvoiceEntry
                                             {
                                                 AssetLeaseEntryId = s.Key,
                                                 AssetItemId = s.Max(s => s.AssetItemId),
                                                 LeaseCost = s.Max(s => s.LeaseCost),
                                                 DownTime = s.Sum(d => d.DownTime),
                                                 Description = s.Max(s => s.AssetItem),
                                                 AssetCode = s.Max(s => s.AssetCode),
                                                 AssetBrand = s.Max(s => s.AssetBrand),
                                                 AssetGroup = s.Max(s => s.AssetGroup),
                                                 AssetModel = s.Max(s => s.AssetModel),
                                                 AssetSubGroup = s.Max(s => s.AssetSubGroup),
                                                 AssetType = s.Max(s => s.AssetType),
                                                 AssetCapacity = s.Max(s => s.AssetCapacity),
                                                 AssetDimension = s.Max(s => s.AssetDimension),
                                                 AssetSerialNumber = s.Max(s => s.AssetSerialNumber),
                                                 UpTime = s.Sum(d => d.UpTime)
                                             }).ToList();
                detail.InvoiceEntries = result;
                return detail;
            }
        }

        public static InvoiceDetail CreateInvoice(string loggedInUser, LeaseInvoiceRequest invoiceRequest)
        {
            using(var dbContext = new AppDataContext())
            {
                var today = DateTime.Now;
                bool invoiceAlreadyExists = true;
                var invoice = dbContext.LeaseInvoices.FirstOrDefault(d => d.IsDeleted == false && d.LeaseEndDate == invoiceRequest.UpdateEndDate && d.LeaseStartDate == invoiceRequest.UpdateStartDate && d.AssetLeaseId == invoiceRequest.AssetLeaseId);
                if (invoice == null)
                {
                    invoiceAlreadyExists = false;
                    invoice = new LeaseInvoice
                    {
                        DateDeleted = null,
                        CreationDate = today,
                        IsDeleted = false,
                        LeaseEndDate = invoiceRequest.UpdateEndDate,
                        LeaseStartDate = invoiceRequest.UpdateStartDate,
                        AssetLeaseId = invoiceRequest.AssetLeaseId,
                        DeletedById = null,
                        CreatedById = loggedInUser,
                        InvoiceNumber = $"INV/AST/{today:yy}",
                        InvoicePeriod = invoiceRequest.InvoicePeriod,
                        TotalAmount = invoiceRequest.TotalAmount
                    };
                }

                var affectedUpdates = dbContext.AssetLeaseUpdates.Where(d => /*d.InvoiceRaised == false &&*/ d.AssetLeaseId == invoiceRequest.AssetLeaseId && d.IsDeleted == false && d.UpdateDate.Date >= invoiceRequest.UpdateStartDate.Date && d.UpdateDate.Date <= invoiceRequest.UpdateEndDate.Date).ToList();
                if (affectedUpdates.Count > 0)
                {
                    foreach(var update in affectedUpdates)
                    {
                        update.LeaseInvoice = invoice;
                        update.InvoiceRaised = true;
                    }
                    if (invoiceAlreadyExists)
                    {
                        invoice.TotalAmount = invoiceRequest.TotalAmount;
                    }
                    dbContext.SaveChanges();
                    if (!invoiceAlreadyExists)
                    {
                        invoice.InvoiceNumber = $"{invoice.InvoiceNumber}/{invoice.NativeId}";
                        dbContext.SaveChanges();
                    }
                }
                return GetInvoiceDetail(invoice.Id);
            }
        }
    }
}
