using equipment_lease_api.DTO;
using equipment_lease_api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using static equipment_lease_api.Helpers.Constants.Strings;

namespace equipment_lease_api.Services
{
    public static class QueryRunner
    {
        public static SubsidiaryDTO AddSubsidiary(SubsidiaryDTO subsidiaryDTO,string loggedInUser)
        {
            using(var dbContext = new AppDataContext())
            {
                var subsidiary = new Subsidiary
                {
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    IsActive = true,
                    Name = subsidiaryDTO.Name,
                    CreatedById = loggedInUser,
                    Code = subsidiaryDTO.Code
                };

                dbContext.Subsidiaries.Add(subsidiary);
                dbContext.SaveChanges();

                return new SubsidiaryDTO
                {
                    Id = subsidiary.Id,
                    Name = subsidiary.Name
                };
            }
        }
        public static List<SubsidiaryDTO> GetSubsidiaries()
        {
            using(var dbContex = new AppDataContext())
            {
                var subsidiaries = dbContex.Subsidiaries.Where(s => s.IsDeleted == false).Select(s => new SubsidiaryDTO { Id = s.Id, Name = s.Name,Code = s.Code }).ToList();
                return subsidiaries;
            }
        }
        public static LocationDTO CreateLocation(LocationDTO locationDTO, string loggedInUser)
        {
            using(var dbContext = new AppDataContext())
            {
                
                var location = new Location
                {
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    CreatedById = loggedInUser,
                    IsActive = true,
                    Name = locationDTO.Name,
                    Code = locationDTO.Code
                };

                dbContext.Locations.Add(location);
                dbContext.SaveChanges();
                return new LocationDTO
                {
                    Id = location.Id,
                    Name = location.Name
                };
            }
        }
        public static LocationResult GetLocations(int skip, int limit)
        {
            using (var dbContex = new AppDataContext())
            {
                var subsidiaries = dbContex.Locations.Where(s => s.IsDeleted == false).Select(s => new LocationDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code
                }).AsQueryable();
                int totalSize = subsidiaries.Count();
                limit = limit == 0 ? totalSize : limit;

                var res = new LocationResult
                {
                    Locations = subsidiaries.Skip(skip).Take(limit).ToList(),
                    TotalCount = totalSize
                };
                return res;
            }
        }
        public static ProjectResult GetProjects(string locationId, string subsidiaryId, int skip, int limit)
        {
            using (var dbContext = new AppDataContext())
            {
                var projects = dbContext.Projects
                    .Where(s => (subsidiaryId == null || s.SubsidiaryId == subsidiaryId) && (locationId == null || s.LocationId == locationId) && s.IsDeleted == false)
                    .Select(s => new ProjectDTO
                    {
                        Description = s.Description,
                        Id = s.Id,
                        Location = s.Location.Name,
                        LocationId = s.LocationId,
                        Subsidiary = s.Subsidiary.Name,
                        SubsidiaryId = s.SubsidiaryId,
                        Name = s.Name,
                        Code = s.Code,
                        ProjectSites = s.ProjectSites.Where(s => s.IsDeleted == false)
                                    .Select(s => new ProjectSiteDTO
                                    {
                                        Id = s.Id,
                                        SiteName = s.Name,
                                        SiteCode = s.Code,
                                        Project = s.Project.Name,
                                        ProjectId = s.ProjectId
                                    }).ToList()
                    }).OrderBy(d => d.Name).AsQueryable();

                int totalCount = projects.Count();

                if (limit == 0)
                {
                    limit = totalCount == 0 ? 10 : totalCount;
                }

                var res = new ProjectResult
                {
                    Projects = projects.Skip(skip).Take(limit).ToList(),
                    TotalCount = totalCount
                };
                return res;
            }
        }
        public static List<ProjectDTO> GetProjectsBySubsidiary(string subsidiaryId)
        {
            using (var dbContext = new AppDataContext())
            {
                var projects = dbContext.Projects.Where(s => s.SubsidiaryId == subsidiaryId && s.IsDeleted == false)
                    .Select(s => new ProjectDTO
                    {
                        Description = s.Description,
                        Id = s.Id,
                        Location = s.Location.Name,
                        LocationId = s.LocationId,
                        Subsidiary = s.Subsidiary.Name,
                        SubsidiaryId = s.SubsidiaryId,
                        Name = s.Name,
                        Code = s.Code
                    }).ToList();
                return projects;
            }
        }
        public static ProjectDTO CreateNewProject(ProjectDTO projectDTO, string userId)
        {
            using(var dbContext = new AppDataContext())
            {
                var affectedLocation = dbContext.Locations.FirstOrDefault(s => s.Id == projectDTO.LocationId && s.IsDeleted == false);

                var affectedSubsidiary = dbContext.Subsidiaries.FirstOrDefault(s => s.Id == projectDTO.SubsidiaryId && s.IsDeleted == false);
                if(affectedLocation !=null && affectedSubsidiary != null)
                {
                    var project = new Project
                    {
                        Description = projectDTO.Description,
                        CreationDate = DateTime.Now,
                        IsDeleted = false,
                        StartDate = projectDTO.StartDate,
                        CreatedById = userId,
                        IsActive = true,
                        LocationId = projectDTO.LocationId,
                        Name = projectDTO.Name,
                        SubsidiaryId = projectDTO.SubsidiaryId,
                        Code = projectDTO.Code
                    };
                    dbContext.Projects.Add(project);
                    dbContext.SaveChanges();
                    return new ProjectDTO
                    {
                        Description = project.Description,
                        StartDate = project.StartDate,
                        Id = project.Id,
                        LocationId = project.LocationId,
                        Location = affectedLocation.Name,
                        Name = project.Name,
                        Subsidiary = affectedSubsidiary.Name,
                        SubsidiaryId = affectedSubsidiary.Id,
                        Code = project.Code
                    };
                }
                return null;
            }
        }
        public static AssetItemDTO SaveAssetItem(AssetItemDTO assetItemDTO, string loggedInUser)
        {
            using(var dbContext = new AppDataContext())
            {
                var assetItem = new AssetItem
                {
                    CreationDate = DateTime.Now,
                    DeletedById = null,
                    IsDeleted = false,
                    DimensionId = assetItemDTO.DimensionId,
                    SerialNumber = assetItemDTO.SerialNumber,
                    AssetSubGroupId = assetItemDTO.AssetSubGroupId,
                    AcquisitionYear = assetItemDTO.AcquisitionYear,
                    CurrentStatus = AssetStatus.AVAILABLE,
                    AssetBrandId = assetItemDTO.AssetBrandId,
                    AssetGroupId = assetItemDTO.AssetGroupId,
                    AssetId = assetItemDTO.AssetId,
                    AssetModelId = assetItemDTO.AssetModelId,
                    AssetTypeId = assetItemDTO.AssetTypeId,
                    AssetValue = assetItemDTO.AssetValue,
                    CapacityId = assetItemDTO.CapacityId,
                    Code = assetItemDTO.Code,
                    CreatedById = loggedInUser,
                    CurrentLocationId = assetItemDTO.CurrentLocationId,
                    EngineModelId = assetItemDTO.EngineModelId,
                    EngineNumber = assetItemDTO.EngineNumber,
                    EngineTypeId = assetItemDTO.EngineTypeId,
                    LeaseCost = assetItemDTO.LeaseCost,
                    ManufactureYear = assetItemDTO.ManufactureYear,
                    RegistrationNumber = assetItemDTO.RegistrationNumber,
                    Remark = assetItemDTO.Remark
                };

                dbContext.AssetItems.Add(assetItem);
                dbContext.SaveChanges();

                return new AssetItemDTO
                {
                    Id = assetItem.Id,
                    DimensionId = assetItem.DimensionId,
                    SerialNumber = assetItem.SerialNumber,
                    AssetSubGroupId = assetItem.AssetSubGroupId,
                    AcquisitionYear = assetItem.AcquisitionYear,
                    CurrentStatus = assetItem.CurrentStatus,
                    AssetBrandId = assetItem.AssetBrandId,
                    AssetGroupId = assetItem.AssetGroupId,
                    AssetId = assetItem.AssetId,
                    AssetModelId = assetItem.AssetModelId,
                    AssetTypeId = assetItem.AssetTypeId,
                    AssetValue = assetItem.AssetValue,
                    CapacityId = assetItem.CapacityId,
                    Code = assetItem.Code,
                    CurrentLocationId = assetItem.CurrentLocationId,
                    EngineModelId = assetItem.EngineModelId,
                    EngineNumber = assetItem.EngineNumber,
                    EngineTypeId = assetItem.EngineTypeId,
                    LeaseCost = assetItem.LeaseCost,
                    ManufactureYear = assetItem.ManufactureYear,
                    RegistrationNumber = assetItem.RegistrationNumber,
                    Remark = assetItem.Remark
                };
            }
        }
        public static AssetListResult GetAssetItems(int skip, int limit, 
            string assetGroupId = null, 
            string assetSubGroupId = null, 
            string assetBrandId = null,
            string assetTypeId = null,
            string assetModelId = null,
            string assetCapacityId = null,
            string assetDimensionId = null,
            string engineTypeId = null,
            string engineModelId = null,
            string assetDescriptionId = null)
        {
            using (var dbContext = new AppDataContext())
            {
                var assets = dbContext.AssetItems.Where(s => s.IsDeleted == false
                && (assetGroupId == null || s.AssetGroupId == assetGroupId)
                && (assetSubGroupId == null || s.AssetSubGroupId == assetSubGroupId)
                && (assetBrandId == null || s.AssetBrandId == assetBrandId)
                && (assetTypeId == null || s.AssetTypeId == assetTypeId)
                && (assetModelId == null || s.AssetModelId == assetModelId)
                && (assetCapacityId == null || s.CapacityId == assetCapacityId)
                && (assetDimensionId == null || s.DimensionId == assetDimensionId)
                && (engineTypeId == null || s.EngineTypeId == engineTypeId)
                && (engineModelId == null || s.EngineModelId == engineModelId)
                && (assetDescriptionId == null || s.AssetId == assetDescriptionId))
                    .Select(s => new AssetItemView
                    {
                        SerialNumber = s.SerialNumber,
                        Id = s.Id,
                        Code = s.Code,
                        CurrentStatus = s.CurrentStatus,
                        AcquisitionYear = s.AcquisitionYear,
                        LeaseCost = s.LeaseCost,
                        AssetGroupId = s.AssetGroupId,
                        AssetBrandId = s.AssetBrandId,
                        AssetId = s.AssetId,
                        AssetModelId = s.AssetModelId,
                        AssetSubGroupId = s.AssetSubGroupId,
                        AssetTypeId = s.AssetTypeId,
                        CapacityId = s.CapacityId,
                        CurrentLocationId = s.CurrentLocationId,
                        DimensionId = s.DimensionId,
                        EngineModelId = s.EngineModelId,
                        EngineTypeId = s.EngineTypeId,
                        Dimension = s.Dimension.Name,
                        Asset = s.Asset.Name,
                        AssetBrand = s.AssetBrand.Name,
                        AssetGroup = s.AssetGroup.Name,
                        AssetSubGroup = s.AssetSubGroup.Name,
                        AssetModel = s.AssetModel.Name,
                        AssetType = s.AssetType.Name,
                        AssetValue = s.AssetValue,
                        Capacity = s.Capacity.Name,
                        CurrentLocation = s.CurrentLocation.Name,
                        EngineModel = s.EngineModel.Name,
                        EngineNumber = s.EngineNumber,
                        EngineType = s.EngineType.Name,
                        ManufactureYear = s.ManufactureYear,
                        RegistrationNumber = s.RegistrationNumber,
                        Remark = s.Remark
                    }).OrderBy(d => d.Asset).AsQueryable();

                int totalSize = assets.Count();
                limit = limit == 0 ? 1000 : limit;

                return new AssetListResult
                {
                    Items = assets.Skip(skip).Take(limit).ToList(),
                    TotalCount = totalSize
                };
            }
        }
        public static AssetItemDTO UpdateAssetItem(AssetItemDTO assetItem,string assetId)
        {
            using(var dbContext = new AppDataContext())
            {

                var affectedAsset = dbContext.AssetItems.FirstOrDefault(a => a.Id == assetId && a.IsDeleted == false);
                if (affectedAsset != null)
                {
                    affectedAsset.DimensionId = assetItem.DimensionId;
                    affectedAsset.SerialNumber = assetItem.SerialNumber;
                    affectedAsset.AssetSubGroupId = assetItem.AssetSubGroupId;
                    affectedAsset.AcquisitionYear = assetItem.AcquisitionYear;
                    affectedAsset.CurrentStatus = assetItem.CurrentStatus;
                    affectedAsset.AssetBrandId = assetItem.AssetBrandId;
                    affectedAsset.AssetGroupId = assetItem.AssetGroupId;
                    affectedAsset.AssetId = assetItem.AssetId;
                    affectedAsset.AssetModelId = assetItem.AssetModelId;
                    affectedAsset.AssetTypeId = assetItem.AssetTypeId;
                    affectedAsset.AssetValue = assetItem.AssetValue;
                    affectedAsset.CapacityId = assetItem.CapacityId;
                    affectedAsset.Code = assetItem.Code;
                    affectedAsset.CurrentLocationId = assetItem.CurrentLocationId;
                    affectedAsset.EngineModelId = assetItem.EngineModelId;
                    affectedAsset.EngineNumber = assetItem.EngineNumber;
                    affectedAsset.EngineTypeId = assetItem.EngineTypeId;
                    affectedAsset.LeaseCost = assetItem.LeaseCost;
                    affectedAsset.ManufactureYear = assetItem.ManufactureYear;
                    affectedAsset.RegistrationNumber = assetItem.RegistrationNumber;
                    affectedAsset.Remark = assetItem.Remark;
                    dbContext.SaveChanges();

                    assetItem.Id = assetId;
                    return assetItem;
                }
                return null;
            }
        }
        public static void DeleteAssetItems(string[] itemIds, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                foreach (var id in itemIds)
                {
                    //return list of error messages for assets not found or assets that are currently leased out
                    var affectedAsset = dbContext.AssetItems.FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
                    if (affectedAsset != null && affectedAsset.CurrentStatus != AssetStatus.LEASED_OUT)
                    {
                        affectedAsset.IsDeleted = true;
                        affectedAsset.DeletedById = loggedInUser;
                    }
                }
                dbContext.SaveChanges();
            }
        }
        public static void DeleteSubsidiaries(string[] itemIds, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                foreach (var id in itemIds)
                {
                    var affectedAsset = dbContext.Subsidiaries.FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
                    if (affectedAsset != null)
                    {
                        affectedAsset.IsDeleted = true;
                        affectedAsset.DeletedById = loggedInUser;
                    }
                }
                dbContext.SaveChanges();
            }
        }
        public static SubsidiaryDTO UpdateSubsidiary(SubsidiaryDTO subsidiaryDTO, string subsidiaryId)
        {
            using (var dbContext = new AppDataContext())
            {
                var affectedSubsidiary = dbContext.Subsidiaries.FirstOrDefault(a => a.Id == subsidiaryId && a.IsDeleted == false);
                if (affectedSubsidiary != null)
                {
                    affectedSubsidiary.Name = subsidiaryDTO.Name;
                    affectedSubsidiary.Code = subsidiaryDTO.Code;
                    dbContext.SaveChanges();
                    subsidiaryDTO.Id = subsidiaryId;
                    return subsidiaryDTO;
                }
                return null;
            }
        }
        public static void DeleteLocations(string[] itemIds, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                foreach (var id in itemIds)
                {
                    var affectedLocation = dbContext.Locations.FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
                    if (affectedLocation != null)
                    {
                        affectedLocation.IsDeleted = true;
                        affectedLocation.DeletedById = loggedInUser;
                        affectedLocation.DateDeleted = DateTime.Now;
                    }
                }
                dbContext.SaveChanges();
            }
        }
        public static List<AssetItemDTO> SearchForItems(string searchText)
        {
            using(var dbContext = new AppDataContext())
            {
                searchText = searchText?.ToLower()?.Trim();

                var items = dbContext.AssetItems.Where(s => s.IsDeleted == false &&
                (s.Asset.Name.ToLower().Contains(searchText) 
                || s.Code.ToLower().Contains(searchText)
                || s.RegistrationNumber.ToLower().Contains(searchText)
                || s.SerialNumber.ToLower().Contains(searchText)))
                    .Select(s => new AssetItemDTO
                    {
                        SerialNumber = s.SerialNumber,
                        Id = s.Id,
                        Code = s.Code,
                        CurrentStatus = s.CurrentStatus,
                        AcquisitionYear = s.AcquisitionYear,
                        LeaseCost = s.LeaseCost,
                        AssetGroupId = s.AssetGroupId,
                        AssetBrandId = s.AssetBrandId,
                        AssetId = s.AssetId,
                        AssetModelId = s.AssetModelId,
                        AssetSubGroupId = s.AssetSubGroupId,
                        AssetTypeId = s.AssetTypeId,
                        CapacityId = s.CapacityId,
                        CurrentLocationId = s.CurrentLocationId,
                        DimensionId = s.DimensionId,
                        EngineModelId = s.EngineModelId,
                        EngineTypeId = s.EngineTypeId,
                        AssetName = s.Asset.Name
                    }).ToList();
                return items;
            }
        }
        public static List<AssetDTO> SearchForAssets(string searchText)
        {
            using (var dbContext = new AppDataContext())
            {
                searchText = searchText?.ToLower()?.Trim();

                var items = dbContext.Assets.Where(s => s.IsDeleted == false &&
                (s.Name.ToLower().Contains(searchText) || s.Code.ToLower().Contains(searchText)))
                    .Select(s => new AssetDTO
                    {
                        Code = s.Code,
                        Id = s.Id,
                        Name = s.Name
                    }).ToList();
                return items;
            }
        }
        public static LocationDTO UpdateLocation(LocationDTO location)
        {
            using (var dbContext = new AppDataContext())
            {
                var affectedLocation = dbContext.Locations.FirstOrDefault(a => a.Id == location.Id && a.IsDeleted == false);
                if (affectedLocation != null)
                {
                    affectedLocation.Name = location.Name;
                    affectedLocation.Code = location.Code;
                    dbContext.SaveChanges();
                    return location;
                }
                return null;
            }
        }
        public static void DeleteProjects(string[] itemIds, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                foreach (var id in itemIds)
                {
                    var affectedAsset = dbContext.Projects.FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
                    if (affectedAsset != null)
                    {
                        affectedAsset.IsDeleted = true;
                        affectedAsset.DeletedById = loggedInUser;
                    }
                }
                dbContext.SaveChanges();
            }
        }
        public static ProjectDTO UpdateProject(ProjectDTO projectDTO, string projectId)
        {
            using (var dbContext = new AppDataContext())
            {
                var affectedProject = dbContext.Projects.FirstOrDefault(a => a.Id == projectId && a.IsDeleted == false);
                if (affectedProject != null)
                {
                    var affectedLocation = dbContext.Locations.FirstOrDefault(s => s.Id == projectDTO.LocationId && s.IsDeleted == false);

                    var affectedSubsidiary = dbContext.Subsidiaries.FirstOrDefault(s => s.Id == projectDTO.SubsidiaryId && s.IsDeleted == false);
                    //should we check if project have been used for any pending lease before subsidiary/location reassignment?
                    //if we should not, it means that all exisiting lease will use the new setup
                    if (affectedLocation != null && affectedSubsidiary != null)
                    {
                        affectedProject.Name = projectDTO.Name;
                        affectedProject.SubsidiaryId = affectedSubsidiary.Id;
                        affectedProject.LocationId = affectedLocation.Id;
                        affectedProject.Code = projectDTO.Code;
                        dbContext.SaveChanges();
                        projectDTO.Id = projectId;
                        return new ProjectDTO
                        {
                            Id = affectedProject.Id,
                            Description = affectedProject.Description,
                            LocationId = affectedProject.LocationId,
                            Location = affectedLocation.Name,
                            Subsidiary = affectedSubsidiary.Name,
                            SubsidiaryId = affectedSubsidiary.Id,
                            Name = affectedProject.Name,
                            Code = affectedProject.Code
                        };
                    }
                }
                return null;
            }
        }
        public static AssetGroupDTO CreateAssetGroup(AssetGroupDTO assetGroupDTO, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                AssetGroup parentGroup = null;
                if (assetGroupDTO.IsSubGroup)
                {
                    parentGroup = dbContext.AssetGroups.FirstOrDefault(d => d.Id == assetGroupDTO.ParentId);
                    if(parentGroup == null)
                    {
                        return null;
                    }
                }

                var assetGroup = new AssetGroup
                {
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    CreatedById = loggedInUser,
                    IsActive = true,
                    Name = assetGroupDTO.Name,
                    Description = assetGroupDTO.Description,
                    Code = assetGroupDTO.Code,
                    ParentGroupId = assetGroupDTO.ParentId
                };

                dbContext.AssetGroups.Add(assetGroup);
                dbContext.SaveChanges();

                return new AssetGroupDTO
                {
                    Id = assetGroup.Id,
                    Name = assetGroup.Name,
                    Description = assetGroup.Description,
                    Code = assetGroupDTO.Code,
                    IsSubGroup = assetGroupDTO.IsSubGroup,
                    ParentGroup = parentGroup?.Name,
                    ParentGroupCode = parentGroup?.Code,
                    ParentId = parentGroup?.Id
                };
            }
        }
        public static AssetGroupResult GetAssetGroups(int skip, int limit)
        {
            using (var dbContex = new AppDataContext())
            {
                var assetGroups = dbContex.AssetGroups.Where(s => s.IsDeleted == false && s.ParentGroupId == null).Select(s => new AssetGroupDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Code = s.Code,
                    ParentGroup = s.ParentGroup.Name,
                    ParentGroupCode = s.ParentGroup.Code,
                    ParentId = s.ParentGroup.Id,
                    IsSubGroup = s.ParentGroupId != null
                }).AsQueryable();
                int totalSize = assetGroups.Count();
                limit = limit == 0 ? totalSize : limit;

                var res = new AssetGroupResult
                {
                    AssetGroups = assetGroups.Skip(skip).Take(limit).ToList(),
                    TotalCount = totalSize
                };
                return res;
            }
        }
        public static void DeleteAssetGroups(string[] groupIds, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                foreach (var id in groupIds)
                {
                    var affectedLocation = dbContext.AssetGroups.FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
                    if (affectedLocation != null)
                    {
                        affectedLocation.IsDeleted = true;
                        affectedLocation.DeletedById = loggedInUser;
                        affectedLocation.DateDeleted = DateTime.Now;
                    }
                }
                dbContext.SaveChanges();
            }
        }
        public static AssetGroupResult GetAssetSubGroups(int skip, int limit, string parentId)
        {
            using (var dbContex = new AppDataContext())
            {
                var assetGroups = dbContex.AssetGroups.Where(s => s.IsDeleted == false && s.ParentGroupId == parentId).Select(s => new AssetGroupDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Code = s.Code,
                    ParentGroup = s.ParentGroup.Name,
                    ParentGroupCode = s.ParentGroup.Code,
                    ParentId = s.ParentGroup.Id,
                    IsSubGroup = true
                }).AsQueryable();
                int totalSize = assetGroups.Count();
                limit = limit == 0 ? totalSize : limit;

                var res = new AssetGroupResult
                {
                    AssetGroups = assetGroups.Skip(skip).Take(limit).ToList(),
                    TotalCount = totalSize
                };
                return res;
            }
        }
        public static AssetGroupDTO UpdateAssetGroup(AssetGroupDTO assetGroup)
        {
            using (var dbContext = new AppDataContext())
            {
                var affectedAssetGroup = dbContext.AssetGroups.FirstOrDefault(a => a.Id == assetGroup.Id && a.IsDeleted == false);
                if (affectedAssetGroup != null)
                {
                    AssetGroup parentGroup = null;
                    if (assetGroup.IsSubGroup)
                    {
                        parentGroup = dbContext.AssetGroups.FirstOrDefault(d => d.Id == assetGroup.ParentId);
                        if (parentGroup == null)
                        {
                            return null;
                        }
                    }

                    affectedAssetGroup.Name = assetGroup.Name;
                    affectedAssetGroup.Code = assetGroup.Code;
                    affectedAssetGroup.Description = assetGroup.Description;
                    affectedAssetGroup.ParentGroupId = assetGroup.ParentId;
                    dbContext.SaveChanges();
                    return assetGroup;
                }
                return null;
            }
        }
        static AssetGroup BuildAssetGroup(AssetData data, string loggedInUser)
        {
            var newGroup = new AssetGroup
            {
                DeletedById = null,
                DateDeleted = null,
                IsDeleted = false,
                Code = null,
                CreationDate = DateTime.Now,
                CreatedById = loggedInUser,
                IsActive = true,
                Name = data.Name,
                Description = null,
                ParentGroupId = null
            };
            return newGroup;
        }
        static AssetGroup BuildAssetSubGroup(AssetData data, string loggedInUser, AssetGroup parentGroup)
        {
            var newGroup = new AssetGroup
            {
                DeletedById = null,
                DateDeleted = null,
                IsDeleted = false,
                Code = null,
                CreationDate = DateTime.Now,
                CreatedById = loggedInUser,
                IsActive = true,
                Name = data.Name,
                Description = null,
                ParentGroup = parentGroup
            };
            return newGroup;
        }
        static Asset BuildAsset(AssetData data, string loggedInUser)
        {
            var newAsset = new Asset
            {
                DeletedById = null,
                DateDeleted = null,
                IsDeleted = false,
                Code = null,
                CreationDate = DateTime.Now,
                CreatedById = loggedInUser,
                IsActive = true,
                Name = data.Name
            };
            return newAsset;
        }
        static AssetType BuildAssetType(AssetData data, string loggedInUser)
        {
            var newAssetType = new AssetType
            {
                DeletedById = null,
                DateDeleted = null,
                IsDeleted = false,
                Code = null,
                CreationDate = DateTime.Now,
                CreatedById = loggedInUser,
                IsActive = true,
                Name = data.Name
            };
            return newAssetType;
        }
        static AssetBrand BuildAssetBrand(AssetData data, string loggedInUser)
        {
            var newBrand = new AssetBrand
            {
                DeletedById = null,
                DateDeleted = null,
                IsDeleted = false,
                Code = null,
                CreationDate = DateTime.Now,
                CreatedById = loggedInUser,
                IsActive = true,
                Name = data.Name
            };
            return newBrand;
        }
        static AssetModel BuildAssetModel(AssetData data, string loggedInUser)
        {
            var newModel = new AssetModel
            {
                DeletedById = null,
                DateDeleted = null,
                IsDeleted = false,
                Code = null,
                CreationDate = DateTime.Now,
                CreatedById = loggedInUser,
                IsActive = true,
                Name = data.Name
            };
            return newModel;
        }
        static AssetCapacity BuildAssetCapacity(AssetData data, string loggedInUser)
        {
            var newData = new AssetCapacity
            {
                DeletedById = null,
                DateDeleted = null,
                IsDeleted = false,
                Code = null,
                CreationDate = DateTime.Now,
                CreatedById = loggedInUser,
                IsActive = true,
                Name = data.Name
            };
            return newData;
        }
        static AssetDimension BuildAssetDimension(AssetData data, string loggedInUser)
        {
            var newData = new AssetDimension
            {
                DeletedById = null,
                DateDeleted = null,
                IsDeleted = false,
                Code = null,
                CreationDate = DateTime.Now,
                CreatedById = loggedInUser,
                IsActive = true,
                Name = data.Name
            };
            return newData;
        }
        static EngineType BuildEngineType(AssetData data, string loggedInUser)
        {
            var newData = new EngineType
            {
                DeletedById = null,
                DateDeleted = null,
                IsDeleted = false,
                Code = null,
                CreationDate = DateTime.Now,
                CreatedById = loggedInUser,
                IsActive = true,
                Name = data.Name
            };
            return newData;
        }
        static EngineModel BuildEngineModel(AssetData data, string loggedInUser)
        {
            var newData = new EngineModel
            {
                DeletedById = null,
                DateDeleted = null,
                IsDeleted = false,
                Code = null,
                CreationDate = DateTime.Now,
                CreatedById = loggedInUser,
                IsActive = true,
                Name = data.Name
            };
            return newData;
        }
        public static BatchUploadResponse BatchUploadAssetItems(List<AssetBatchUploadDTO> assetBatches, string loggedInUser)
        {
            var errorList = new List<AssetBatchUploadDTO>();
            using (var dbContext = new AppDataContext())
            {
                var today = DateTime.Now;
                var itemList = new List<AssetItem>();

                var newGroups = new List<AssetGroup>();
                var newSubGroups = new List<AssetGroup>();
                var newAssets = new List<Asset>();

                var newAssetTypes = new List<AssetType>();
                var newAssetBrands = new List<AssetBrand>();
                var newAssetModels = new List<AssetModel>();

                var newAssetCapacities = new List<AssetCapacity>();
                var newAssetDimensions = new List<AssetDimension>();
                var newEngineModels = new List<EngineModel>();
                var newEngineTypes = new List<EngineType>();

                foreach (var item in assetBatches)
                {

                    bool itemValid = true;
                    if (string.IsNullOrWhiteSpace(item.Description))
                    {
                        itemValid = false;
                        item.ErrorComment += "Asset Description cannot be empty\n";
                    }

                    if (string.IsNullOrWhiteSpace(item.SubGroup))
                    {
                        itemValid = false;
                        item.ErrorComment += "Asset Sub-group cannot be empty\n";
                    }

                    if (string.IsNullOrWhiteSpace(item.Group))
                    {
                        itemValid = false;
                        item.ErrorComment += "Asset Group cannot be empty\n";
                    }


                    if (!itemValid)
                    {
                        errorList.Add(item);
                    }

                    if (itemValid)
                    {
                        var affectedAssetGroup = dbContext.AssetGroups.FirstOrDefault(g => g.Name.ToLower() == item.Group.Trim().ToLower() && g.ParentGroupId == null);
                        var affectedAssetSubGroup = dbContext.AssetGroups.FirstOrDefault(g => g.Name.ToLower() == item.SubGroup.Trim().ToLower() && g.ParentGroupId != null);
                        var affectedAsset = dbContext.Assets.FirstOrDefault(g => g.Name.ToLower() == item.Description.Trim().ToLower());

                        AssetType affectedAssetType = null;
                        AssetBrand affectedAssetBrand = null;
                        AssetModel affectedAssetModel = null;
                        AssetCapacity affectedAssetCapacity = null;
                        AssetDimension affectedAssetDimension = null;
                        EngineModel affectedEngineModel = null;
                        EngineType affectedEngineType = null;

                        if (affectedAssetGroup == null)
                        {
                            affectedAssetGroup = newGroups.FirstOrDefault(g => g.Name.ToLower() == item.Group.Trim().ToLower());
                            if (affectedAssetGroup == null)
                            {
                                affectedAssetGroup = BuildAssetGroup(new AssetData { Name = item.Group.Trim() }, loggedInUser);
                                newGroups.Add(affectedAssetGroup);
                            }
                        }

                        if (affectedAssetSubGroup == null)
                        {
                            affectedAssetSubGroup = newSubGroups.FirstOrDefault(g => g.Name.ToLower() == item.SubGroup.Trim().ToLower());
                            if (affectedAssetSubGroup == null)
                            {
                                affectedAssetSubGroup = BuildAssetSubGroup(new AssetData { Name = item.SubGroup.Trim() }, loggedInUser, affectedAssetGroup);
                                newSubGroups.Add(affectedAssetSubGroup);
                            }
                        }

                        if (affectedAsset == null)
                        {
                            affectedAsset = newAssets.FirstOrDefault(g => g.Name.ToLower() == item.Description.Trim().ToLower());
                            if (affectedAsset == null)
                            {
                                affectedAsset = BuildAsset(new AssetData { Name = item.Description.Trim() }, loggedInUser);
                                newAssets.Add(affectedAsset);
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(item.AssetType))
                        {
                            affectedAssetType = dbContext.AssetTypes.FirstOrDefault(g => g.Name.ToLower() == item.AssetType.Trim().ToLower());
                            if (affectedAssetType == null)
                            {
                                affectedAssetType = newAssetTypes.FirstOrDefault(g => g.Name.ToLower() == item.AssetType.Trim().ToLower());
                                if (affectedAssetType == null)
                                {
                                    affectedAssetType = BuildAssetType(new AssetData { Name = item.AssetType.Trim() }, loggedInUser);
                                    newAssetTypes.Add(affectedAssetType);
                                }
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(item.AssetBrand))
                        {
                            affectedAssetBrand = dbContext.AssetBrands.FirstOrDefault(g => g.Name.ToLower() == item.AssetBrand.Trim().ToLower());
                            if (affectedAssetBrand == null)
                            {
                                affectedAssetBrand = newAssetBrands.FirstOrDefault(g => g.Name.ToLower() == item.AssetBrand.Trim().ToLower());
                                if (affectedAssetBrand == null)
                                {
                                    affectedAssetBrand = BuildAssetBrand(new AssetData { Name = item.AssetBrand.Trim() }, loggedInUser);
                                    newAssetBrands.Add(affectedAssetBrand);
                                }
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(item.AssetModel))
                        {
                            affectedAssetModel = dbContext.AssetModels.FirstOrDefault(g => g.Name.ToLower() == item.AssetModel.Trim().ToLower());
                            if (affectedAssetModel == null)
                            {
                                affectedAssetModel = newAssetModels.FirstOrDefault(g => g.Name.ToLower() == item.AssetModel.Trim().ToLower());
                                if (affectedAssetModel == null)
                                {
                                    affectedAssetModel = BuildAssetModel(new AssetData { Name = item.AssetModel.Trim() }, loggedInUser);
                                    newAssetModels.Add(affectedAssetModel);
                                }
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(item.AssetCapacity))
                        {
                            affectedAssetCapacity = dbContext.AssetCapacities.FirstOrDefault(g => g.Name.ToLower() == item.AssetCapacity.Trim().ToLower());
                            if (affectedAssetCapacity == null)
                            {
                                affectedAssetCapacity = newAssetCapacities.FirstOrDefault(g => g.Name.ToLower() == item.AssetCapacity.Trim().ToLower());
                                if (affectedAssetCapacity == null)
                                {
                                    affectedAssetCapacity = BuildAssetCapacity(new AssetData { Name = item.AssetCapacity.Trim() }, loggedInUser);
                                    newAssetCapacities.Add(affectedAssetCapacity);
                                }
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(item.AssetDimension))
                        {
                            affectedAssetDimension = dbContext.AssetDimensions.FirstOrDefault(g => g.Name.ToLower() == item.AssetDimension.Trim().ToLower());
                            if (affectedAssetDimension == null)
                            {
                                affectedAssetDimension = newAssetDimensions.FirstOrDefault(g => g.Name.ToLower() == item.AssetDimension.Trim().ToLower());
                                if (affectedAssetDimension == null)
                                {
                                    affectedAssetDimension = BuildAssetDimension(new AssetData { Name = item.AssetDimension.Trim() }, loggedInUser);
                                    newAssetDimensions.Add(affectedAssetDimension);
                                }
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(item.EngineModel))
                        {
                            affectedEngineModel = dbContext.EngineModels.FirstOrDefault(g => g.Name.ToLower() == item.EngineModel.Trim().ToLower());
                            if (affectedEngineModel == null)
                            {
                                affectedEngineModel = newEngineModels.FirstOrDefault(g => g.Name.ToLower() == item.EngineModel.Trim().ToLower());
                                if (affectedEngineModel == null)
                                {
                                    affectedEngineModel = BuildEngineModel(new AssetData { Name = item.EngineModel.Trim() }, loggedInUser);
                                    newEngineModels.Add(affectedEngineModel);
                                }
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(item.EngineType))
                        {
                            affectedEngineType = dbContext.EngineTypes.FirstOrDefault(g => g.Name.ToLower() == item.EngineType.Trim().ToLower());
                            if (affectedEngineType == null)
                            {
                                affectedEngineType = newEngineTypes.FirstOrDefault(g => g.Name.ToLower() == item.EngineType.Trim().ToLower());
                                if (affectedEngineType == null)
                                {
                                    affectedEngineType = BuildEngineType(new AssetData { Name = item.EngineType.Trim() }, loggedInUser);
                                    newEngineTypes.Add(affectedEngineType);
                                }
                            }
                        }
                        bool validAcquisitionYear = int.TryParse(item.AcquisitionYear, out int acquisitionYear);
                        bool validManufactureYear = int.TryParse(item.ManufactorYear, out int manufactorYear);
                        
                        var assetItem = new AssetItem
                        {
                            Code = item.AssetCode?.Trim(),
                            RegistrationNumber = item.RegistrationNumber?.Trim() ?? "N/A",
                            SerialNumber = item.SerialNumber?.Trim() ?? "N/A",
                            EngineNumber = item.EngineNumber?.Trim() ?? "N/A",
                            AcquisitionYear = acquisitionYear,
                            ManufactureYear = manufactorYear,
                            AssetValue = item.AssetValue,
                            Asset = affectedAsset,
                            Dimension = affectedAssetDimension,
                            AssetBrand = affectedAssetBrand,
                            AssetGroup = affectedAssetGroup,
                            AssetModel = affectedAssetModel,
                            AssetSubGroup = affectedAssetSubGroup,
                            Capacity = affectedAssetCapacity,
                            CreationDate = today,
                            DeletedById = null,
                            IsDeleted =false,
                            AssetType = affectedAssetType,
                            CreatedById = loggedInUser,
                            CurrentLocationId = null,
                            CurrentStatus = AssetStatus.AVAILABLE,
                            EngineModel = affectedEngineModel,
                            EngineType = affectedEngineType,
                            LeaseCost = item.LeaseCost
                        };

                        itemList.Add(assetItem);
                    }
                }

                if (itemList.Count > 0)
                {
                    dbContext.AssetItems.AddRange(itemList);
                    dbContext.SaveChanges();
                }

                return new BatchUploadResponse
                {
                    ErrorList = errorList,
                    IsSuccessful = errorList.Count == 0
                };
            }
        }
        public static AssetTypeDTO CreateAssetType(AssetTypeDTO assetTypeDTO, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                var assetType = new AssetType
                {
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    CreatedById = loggedInUser,
                    IsActive = true,
                    Name = assetTypeDTO.Name,
                    Code = assetTypeDTO.Code
                };

                dbContext.AssetTypes.Add(assetType);
                dbContext.SaveChanges();
                return new AssetTypeDTO
                {
                    Id = assetType.Id,
                    Name = assetType.Name,
                    Code = assetType.Code
                };
            }
        }
        public static AssetTypeResult GetAssetTypes(int skip, int limit)
        {
            using (var dbContex = new AppDataContext())
            {
                var assetTypes = dbContex.AssetTypes.Where(s => s.IsDeleted == false).Select(s => new AssetTypeDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code
                }).AsQueryable();
                int totalSize = assetTypes.Count();
                limit = limit == 0 ? totalSize : limit;

                var res = new AssetTypeResult
                {
                    AssetTypes = assetTypes.Skip(skip).Take(limit).ToList(),
                    TotalCount = totalSize
                };
                return res;
            }
        }
        public static void DeleteAssetTypes(string[] groupIds, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                foreach (var id in groupIds)
                {
                    var affectedLocation = dbContext.AssetTypes.FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
                    if (affectedLocation != null)
                    {
                        affectedLocation.IsDeleted = true;
                        affectedLocation.DeletedById = loggedInUser;
                        affectedLocation.DateDeleted = DateTime.Now;
                    }
                }
                dbContext.SaveChanges();
            }
        }
        public static AssetTypeDTO UpdateAssetType(AssetTypeDTO assetType)
        {
            using (var dbContext = new AppDataContext())
            {
                var affectedAssetType = dbContext.AssetTypes.FirstOrDefault(a => a.Id == assetType.Id && a.IsDeleted == false);
                if (affectedAssetType != null)
                {
                    affectedAssetType.Name = assetType.Name;
                    affectedAssetType.Code = assetType.Code;
                    dbContext.SaveChanges();
                    return assetType;
                }
                return null;
            }
        }
        public static AssetBrandDTO CreateAssetBrand(AssetBrandDTO assetBrandDTO, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                var assetBrand = new AssetBrand
                {
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    CreatedById = loggedInUser,
                    IsActive = true,
                    Name = assetBrandDTO.Name,
                    Code = assetBrandDTO.Code
                };

                dbContext.AssetBrands.Add(assetBrand);
                dbContext.SaveChanges();
                return new AssetBrandDTO
                {
                    Id = assetBrand.Id,
                    Name = assetBrand.Name,
                    Code = assetBrand.Code
                };
            }
        }
        public static AssetBrandResult GetAssetBrands(int skip, int limit)
        {
            using (var dbContex = new AppDataContext())
            {
                var assetBrands = dbContex.AssetBrands.Where(s => s.IsDeleted == false).Select(s => new AssetBrandDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code
                }).AsQueryable();
                int totalSize = assetBrands.Count();
                limit = limit == 0 ? totalSize : limit;

                var res = new AssetBrandResult
                {
                    AssetBrands = assetBrands.Skip(skip).Take(limit).ToList(),
                    TotalCount = totalSize
                };
                return res;
            }
        }
        public static void DeleteAssetBrands(string[] groupIds, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                foreach (var id in groupIds)
                {
                    var affectedLocation = dbContext.AssetBrands.FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
                    if (affectedLocation != null)
                    {
                        affectedLocation.IsDeleted = true;
                        affectedLocation.DeletedById = loggedInUser;
                        affectedLocation.DateDeleted = DateTime.Now;
                    }
                }
                dbContext.SaveChanges();
            }
        }
        public static AssetBrandDTO UpdateAssetBrand(AssetBrandDTO assetBrand)
        {
            using (var dbContext = new AppDataContext())
            {
                var affectedAssetBrand = dbContext.AssetBrands.FirstOrDefault(a => a.Id == assetBrand.Id && a.IsDeleted == false);
                if (affectedAssetBrand != null)
                {
                    affectedAssetBrand.Name = assetBrand.Name;
                    affectedAssetBrand.Code = assetBrand.Code;
                    dbContext.SaveChanges();
                    return assetBrand;
                }
                return null;
            }
        }
        public static AssetDTO CreateAsset(AssetDTO assetBrandDTO, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                var asset = new Asset
                {
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    CreatedById = loggedInUser,
                    IsActive = true,
                    Name = assetBrandDTO.Name,
                    Code = assetBrandDTO.Code
                };

                dbContext.Assets.Add(asset);
                dbContext.SaveChanges();
                return new AssetDTO
                {
                    Id = asset.Id,
                    Name = asset.Name,
                    Code = asset.Code
                };
            }
        }
        public static AssetResult GetAssets(int skip, int limit)
        {
            using (var dbContex = new AppDataContext())
            {
                var assets = dbContex.Assets.Where(s => s.IsDeleted == false).Select(s => new AssetDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code
                }).OrderBy(d => d.Name).AsQueryable();
                int totalSize = assets.Count();
                limit = limit == 0 ? totalSize : limit;

                var res = new AssetResult
                {
                    Assets = assets.Skip(skip).Take(limit).ToList(),
                    TotalCount = totalSize
                };
                return res;
            }
        }
        public static void DeleteAssets(string[] assetIds, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                foreach (var id in assetIds)
                {
                    var affectedAsset = dbContext.Assets.FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
                    if (affectedAsset != null)
                    {
                        affectedAsset.IsDeleted = true;
                        affectedAsset.DeletedById = loggedInUser;
                        affectedAsset.DateDeleted = DateTime.Now;
                    }
                }
                dbContext.SaveChanges();
            }
        }
        public static AssetDTO UpdateAsset(AssetDTO asset)
        {
            using (var dbContext = new AppDataContext())
            {
                var affectedAsset = dbContext.Assets.FirstOrDefault(a => a.Id == asset.Id && a.IsDeleted == false);
                if (affectedAsset != null)
                {
                    affectedAsset.Name = asset.Name;
                    affectedAsset.Code = asset.Code;
                    dbContext.SaveChanges();
                    return asset;
                }
                return null;
            }
        }
        public static AssetData CreateAssetCapacity(AssetData data, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                var assetcapacity = new AssetCapacity
                {
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    CreatedById = loggedInUser,
                    IsActive = true,
                    Name = data.Name,
                    Code = data.Code
                };

                dbContext.AssetCapacities.Add(assetcapacity);
                dbContext.SaveChanges();
                return new AssetData
                {
                    Id = assetcapacity.Id,
                    Name = assetcapacity.Name,
                    Code = assetcapacity.Code
                };
            }
        }
        public static AssetDataConfig GetAssetCapacities(int skip, int limit)
        {
            using (var dbContex = new AppDataContext())
            {
                var assetCapacities = dbContex.AssetCapacities.Where(s => s.IsDeleted == false).Select(s => new AssetData
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code
                }).AsQueryable();
                int totalSize = assetCapacities.Count();
                limit = limit == 0 ? totalSize : limit;

                var res = new AssetDataConfig
                {
                    Data = assetCapacities.Skip(skip).Take(limit).ToList(),
                    TotalCount = totalSize
                };
                return res;
            }
        }
        public static void DeleteAssetCapacities(string[] capacityIds, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                foreach (var id in capacityIds)
                {
                    var affectedAsset = dbContext.AssetCapacities.FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
                    if (affectedAsset != null)
                    {
                        affectedAsset.IsDeleted = true;
                        affectedAsset.DeletedById = loggedInUser;
                        affectedAsset.DateDeleted = DateTime.Now;
                    }
                }
                dbContext.SaveChanges();
            }
        }
        public static AssetData UpdateAssetCapacity(AssetData data)
        {
            using (var dbContext = new AppDataContext())
            {
                var affectedAsset = dbContext.AssetCapacities.FirstOrDefault(a => a.Id == data.Id && a.IsDeleted == false);
                if (affectedAsset != null)
                {
                    affectedAsset.Name = data.Name;
                    affectedAsset.Code = data.Code;
                    dbContext.SaveChanges();
                    return data;
                }
                return null;
            }
        }
        public static AssetData CreateAssetDimension(AssetData data, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                var assetDimension = new AssetDimension
                {
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    CreatedById = loggedInUser,
                    IsActive = true,
                    Name = data.Name,
                    Code = data.Code
                };

                dbContext.AssetDimensions.Add(assetDimension);
                dbContext.SaveChanges();
                return new AssetData
                {
                    Id = assetDimension.Id,
                    Name = assetDimension.Name,
                    Code = assetDimension.Code
                };
            }
        }
        public static AssetDataConfig GetAssetDimensions(int skip, int limit)
        {
            using (var dbContex = new AppDataContext())
            {
                var assetDimensions = dbContex.AssetDimensions.Where(s => s.IsDeleted == false).Select(s => new AssetData
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code
                }).AsQueryable();
                int totalSize = assetDimensions.Count();
                limit = limit == 0 ? totalSize : limit;

                var res = new AssetDataConfig
                {
                    Data = assetDimensions.Skip(skip).Take(limit).ToList(),
                    TotalCount = totalSize
                };
                return res;
            }
        }
        public static void DeleteAssetDimensions(string[] capacityIds, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                foreach (var id in capacityIds)
                {
                    var affectedAsset = dbContext.AssetDimensions.FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
                    if (affectedAsset != null)
                    {
                        affectedAsset.IsDeleted = true;
                        affectedAsset.DeletedById = loggedInUser;
                        affectedAsset.DateDeleted = DateTime.Now;
                    }
                }
                dbContext.SaveChanges();
            }
        }
        public static AssetData UpdateAssetDimension(AssetData data)
        {
            using (var dbContext = new AppDataContext())
            {
                var affectedAsset = dbContext.AssetDimensions.FirstOrDefault(a => a.Id == data.Id && a.IsDeleted == false);
                if (affectedAsset != null)
                {
                    affectedAsset.Name = data.Name;
                    affectedAsset.Code = data.Code;
                    dbContext.SaveChanges();
                    return data;
                }
                return null;
            }
        }
        public static AssetData CreateAssetModel(AssetData data, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                var assetDimension = new AssetModel
                {
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    CreatedById = loggedInUser,
                    IsActive = true,
                    Name = data.Name,
                    Code = data.Code
                };

                dbContext.AssetModels.Add(assetDimension);
                dbContext.SaveChanges();
                return new AssetData
                {
                    Id = assetDimension.Id,
                    Name = assetDimension.Name,
                    Code = assetDimension.Code
                };
            }
        }
        public static AssetDataConfig GetAssetModels(int skip, int limit)
        {
            using (var dbContex = new AppDataContext())
            {
                var assetModels = dbContex.AssetModels.Where(s => s.IsDeleted == false).Select(s => new AssetData
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code
                }).AsQueryable();
                int totalSize = assetModels.Count();
                limit = limit == 0 ? totalSize : limit;

                var res = new AssetDataConfig
                {
                    Data = assetModels.Skip(skip).Take(limit).ToList(),
                    TotalCount = totalSize
                };
                return res;
            }
        }
        public static void DeleteAssetModels(string[] capacityIds, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                foreach (var id in capacityIds)
                {
                    var affectedAsset = dbContext.AssetModels.FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
                    if (affectedAsset != null)
                    {
                        affectedAsset.IsDeleted = true;
                        affectedAsset.DeletedById = loggedInUser;
                        affectedAsset.DateDeleted = DateTime.Now;
                    }
                }
                dbContext.SaveChanges();
            }
        }
        public static AssetData UpdateAssetModel(AssetData data)
        {
            using (var dbContext = new AppDataContext())
            {
                var affectedData = dbContext.AssetModels.FirstOrDefault(a => a.Id == data.Id && a.IsDeleted == false);
                if (affectedData != null)
                {
                    affectedData.Name = data.Name;
                    affectedData.Code = data.Code;
                    dbContext.SaveChanges();
                    return data;
                }
                return null;
            }
        }
        public static AssetData CreateEngineModel(AssetData data, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                var newData = new EngineModel
                {
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    CreatedById = loggedInUser,
                    IsActive = true,
                    Name = data.Name,
                    Code = data.Code
                };

                dbContext.EngineModels.Add(newData);
                dbContext.SaveChanges();
                return new AssetData
                {
                    Id = newData.Id,
                    Name = newData.Name,
                    Code = newData.Code
                };
            }
        }
        public static AssetDataConfig GetEngineModels(int skip, int limit)
        {
            using (var dbContex = new AppDataContext())
            {
                var engineModels = dbContex.EngineModels.Where(s => s.IsDeleted == false).Select(s => new AssetData
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code
                }).AsQueryable();
                int totalSize = engineModels.Count();
                limit = limit == 0 ? totalSize : limit;

                var res = new AssetDataConfig
                {
                    Data = engineModels.Skip(skip).Take(limit).ToList(),
                    TotalCount = totalSize
                };
                return res;
            }
        }
        public static void DeleteEngineModels(string[] capacityIds, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                foreach (var id in capacityIds)
                {
                    var affectedData = dbContext.EngineModels.FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
                    if (affectedData != null)
                    {
                        affectedData.IsDeleted = true;
                        affectedData.DeletedById = loggedInUser;
                        affectedData.DateDeleted = DateTime.Now;
                    }
                }
                dbContext.SaveChanges();
            }
        }
        public static AssetData UpdateEngineModel(AssetData data)
        {
            using (var dbContext = new AppDataContext())
            {
                var affectedData = dbContext.EngineModels.FirstOrDefault(a => a.Id == data.Id && a.IsDeleted == false);
                if (affectedData != null)
                {
                    affectedData.Name = data.Name;
                    affectedData.Code = data.Code;
                    dbContext.SaveChanges();
                    return data;
                }
                return null;
            }
        }
        public static AssetData CreateEngineType(AssetData data, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                var newData = new EngineType
                {
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    CreatedById = loggedInUser,
                    IsActive = true,
                    Name = data.Name,
                    Code = data.Code
                };

                dbContext.EngineTypes.Add(newData);
                dbContext.SaveChanges();
                return new AssetData
                {
                    Id = newData.Id,
                    Name = newData.Name,
                    Code = newData.Code
                };
            }
        }
        public static AssetDataConfig GetEngineTypes(int skip, int limit)
        {
            using (var dbContex = new AppDataContext())
            {
                var engineTypes = dbContex.EngineTypes.Where(s => s.IsDeleted == false).Select(s => new AssetData
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code
                }).AsQueryable();
                int totalSize = engineTypes.Count();
                limit = limit == 0 ? totalSize : limit;

                var res = new AssetDataConfig
                {
                    Data = engineTypes.Skip(skip).Take(limit).ToList(),
                    TotalCount = totalSize
                };
                return res;
            }
        }
        public static void DeleteEngineTypes(string[] capacityIds, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                foreach (var id in capacityIds)
                {
                    var affectedData = dbContext.EngineTypes.FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
                    if (affectedData != null)
                    {
                        affectedData.IsDeleted = true;
                        affectedData.DeletedById = loggedInUser;
                        affectedData.DateDeleted = DateTime.Now;
                    }
                }
                dbContext.SaveChanges();
            }
        }
        public static AssetData UpdateEngineType(AssetData data)
        {
            using (var dbContext = new AppDataContext())
            {
                var affectedData = dbContext.EngineTypes.FirstOrDefault(a => a.Id == data.Id && a.IsDeleted == false);
                if (affectedData != null)
                {
                    affectedData.Name = data.Name;
                    affectedData.Code = data.Code;
                    dbContext.SaveChanges();
                    return data;
                }
                return null;
            }
        }
        public static ProjectSiteDTO CreateProjectSite(ProjectSiteDTO data, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                var affectedProject = dbContext.Projects.Where(p => p.Id == data.ProjectId).Select(s => new { s.Id, s.Name,s.Subsidiary,s.SubsidiaryId }).FirstOrDefault();
                if (affectedProject!=null)
                {
                    var newData = new ProjectSite
                    {
                        CreationDate = DateTime.Now,
                        IsDeleted = false,
                        CreatedById = loggedInUser,
                        IsActive = true,
                        Name = data.SiteName,
                        Code = data.SiteCode,
                        ProjectId = data.ProjectId
                    };

                    dbContext.ProjectSites.Add(newData);
                    dbContext.SaveChanges();
                    return new ProjectSiteDTO
                    {
                        Id = newData.Id,
                        SiteCode = newData.Name,
                        SiteName = newData.Code,
                        ProjectId = affectedProject.Id,
                        Project = affectedProject.Name
                    };
                }
                return null;
            }
        }
        public static List<ProjectSiteDTO> GetProjectSites(string projectId)
        {
            using (var dbContex = new AppDataContext())
            {
                var projectSites = dbContex.ProjectSites
                    .Where(s => s.IsDeleted == false && (projectId == null || s.ProjectId == projectId))
                    .Select(s => new ProjectSiteDTO
                    {
                        Id = s.Id,
                        SiteName = s.Name,
                        SiteCode = s.Code,
                        ProjectId = s.ProjectId,
                        Project = s.Project.Name
                    }).ToList();
                return projectSites;
            }
        }
        public static void DeleteProjectSites(string[] siteids, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                foreach (var id in siteids)
                {
                    var affectedData = dbContext.ProjectSites.FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
                    if (affectedData != null)
                    {
                        affectedData.IsDeleted = true;
                        affectedData.DeletedById = loggedInUser;
                        affectedData.DateDeleted = DateTime.Now;
                    }
                }
                dbContext.SaveChanges();
            }
        }
        public static ProjectSiteDTO UpdateProjectSite(ProjectSiteDTO data)
        {
            using (var dbContext = new AppDataContext())
            {
                var affectedData = dbContext.ProjectSites.FirstOrDefault(a => a.Id == data.Id && a.IsDeleted == false);
                if (affectedData != null)
                {
                    affectedData.Name = data.SiteName;
                    affectedData.Code = data.SiteCode;
                    affectedData.ProjectId = data.ProjectId;
                    dbContext.SaveChanges();

                    var updatedRecord = dbContext.ProjectSites.Where(a => a.Id == data.Id && a.IsDeleted == false).FirstOrDefault();

                    return new ProjectSiteDTO
                    {
                        Id = updatedRecord.Id,
                        ProjectId = updatedRecord.ProjectId,
                        SiteCode = updatedRecord.Code,
                        SiteName = updatedRecord.Name,
                        Project = updatedRecord.Project.Name
                    };
                }
                return null;
            }
        }
    }
}