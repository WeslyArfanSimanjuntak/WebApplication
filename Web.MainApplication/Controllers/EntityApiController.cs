using Repository.Application.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using Web.MainApplication.Models;

namespace Web.MainApplication.Controllers
{
    public class EntityApiController : BaseController
    {
        private DB_TritsurEntities db;

        // GET: EntityApi

        public ActionResult GetData()
        {
            string entitas = Request.Params["entitas"];
            string filter = Request.Params["filter"];
            return PopulateData(entitas, filter);

        }
        public EntityApiController()
        {
            this.db = new DB_TritsurEntities();
            this.db.Configuration.ProxyCreationEnabled = false;
        }
        public EntityApiController(DB_TritsurEntities db)
        {
            this.db = db;
            this.db.Configuration.ProxyCreationEnabled = false;
        }

        [NonAction]
        private JsonResult PopulateData(string entitas, string filter = "true")
        {
            var result = new JsonResult().JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                if (entitas == "ARMADA")
                {
                    return this.Json(new
                    {
                        data = db.ARMADA.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }

                else if (entitas == "AspNetGroups")
                {
                    return this.Json(new
                    {
                        data = db.AspNetGroups.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "AspNetGroupUser")
                {
                    return this.Json(new
                    {
                        data = db.AspNetGroupUser.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "AspNetRoleGroup")
                {
                    return this.Json(new
                    {
                        data = db.AspNetRoleGroup.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "AspNetUsers")
                {
                    return this.Json(new
                    {
                        data = db.AspNetUsers.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "BATCH")
                {
                    return this.Json(new
                    {
                        data = db.BATCH.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "BatchProduct")
                {
                    db.Configuration.ProxyCreationEnabled = true;
                    var data = db.BatchProduct.Where(filter).ToList();
                    var populateData = new List<BatchProduct>();
                    data.ForEach(x =>
                    {
                        populateData.Add(new BatchProduct()
                        {
                            PRODUCT = new PRODUCT()
                            {
                                PRODUCTNAME = x.PRODUCT.PRODUCTNAME
                            },
                            BatchCode = x.BatchCode,
                            ProductPercentage = x.ProductPercentage,
                            ProductId = x.ProductId
                        });
                    });
                    return this.Json(new
                    {
                        data = populateData
                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "CLIENT")
                {
                    return this.Json(new
                    {
                        data = db.CLIENT.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "CONTRACT")
                {
                    return this.Json(new
                    {
                        data = db.CONTRACT.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }else if(entitas == "ContractProduct")
                {
                    return this.Json(new
                    {
                        data = db.ContractProduct.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                
                else if (entitas == "DeliveryRequest")
                {
                    return this.Json(new
                    {
                        data = db.DeliveryRequest.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "DeliveryRequestProductDetailTransaction")
                {
                    return this.Json(new
                    {
                        data = db.DeliveryRequestProductDetailTransaction.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "EMPLOYEE")
                {
                    return this.Json(new
                    {
                        data = db.EMPLOYEE.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "PRODUCT")
                {
                    return this.Json(new
                    {
                        data = db.PRODUCT.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "ProductAdjustment")
                {
                    return this.Json(new
                    {
                        data = db.ProductAdjustment.Where(filter).ToList()


                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "ProductConvertion")
                {
                    return this.Json(new
                    {
                        data = db.ProductConvertion.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "ProductSite")
                {
                    return this.Json(new
                    {
                        data = db.ProductSite.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "RITASE")
                {
                    return this.Json(new
                    {
                        data = db.RITASE.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "SITE")
                {
                    return this.Json(new
                    {
                        data = db.SITE.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "SOURCE")
                {
                    return this.Json(new
                    {
                        data = db.SOURCE.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "StockProduct")
                {
                    return this.Json(new
                    {
                        data = db.StockProduct.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "TransactionProduct")
                {
                    return this.Json(new
                    {
                        data = db.TransactionProduct.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "UserClientMapping")
                {
                    return this.Json(new
                    {
                        data = db.UserClientMapping.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "AspNetRoles")
                {
                    return this.Json(new
                    {
                        data = db.AspNetRoles.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "FinanceBalance")
                {
                    return this.Json(new
                    {
                        data = db.FinanceBalance.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "FinanceTransaction")
                {
                    return this.Json(new
                    {
                        data = db.FinanceTransaction.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "FinanceTransactionNostro")
                {
                    return this.Json(new
                    {
                        data = db.FinanceTransactionNostro.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "NostroBank")
                {
                    return this.Json(new
                    {
                        data = db.NostroBank.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "TransactionCode")
                {
                    return this.Json(new
                    {
                        data = db.TransactionCode.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "Menu")
                {
                    return this.Json(new
                    {
                        data = db.Menu.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "DeliveryOrder")
                {
                    return this.Json(new
                    {
                        data = db.DeliveryOrder.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "DeliveryOrderInvoice")
                {
                    return this.Json(new
                    {
                        data = db.DeliveryOrderInvoice.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "TableSequence")
                {
                    return this.Json(new
                    {
                        data = db.TableSequence.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                else if (entitas == "ParameterSetup")
                {
                    return this.Json(new
                    {
                        data = db.ParameterSetup.Where(filter).ToList()

                    }, JsonRequestBehavior.AllowGet);

                }
                //else if (entitas == "SOURCE")
                //{
                //    return this.Json(new
                //    {
                //        data = db..Where(filter).ToList()

                //    }, JsonRequestBehavior.AllowGet);

                //}
                else
                {
                    return this.Json(new
                    {
                        data = "Null",
                        message = "entitas \"" + entitas + "\" not found"

                    }, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {
                return this.Json(new
                {
                    data = "Null",
                    message = ex.Message

                }, JsonRequestBehavior.AllowGet);
            }


        }

        [HttpPost]
        public ActionResult Search(string entity, string filter = null, int rowSkip = 0, int rowToTake = 10, string orderByField = "1", string orderByType = "ASC", string draw = "1")
        {

            if (Request.Params["start"] != null)
            {
                rowSkip = Convert.ToInt32(Request.Params["start"]);
            }
            if (Request.Params["length"] != null)
            {
                rowToTake = Convert.ToInt32(Request.Params["length"]);
            }
            if (Request.Params["draw"] != null)
            {
                draw = Request.Params["draw"];
            }
            if (Request.Params["columns[" + Request.Params["order[0][column]"] + "][name]"] != null)
            {
                orderByField = Request.Params["columns[" + Request.Params["order[0][column]"] + "][name]"];
            }
            if (Request.Params["order[0][dir]"] != null)
            {
                orderByType = Request.Params["order[0][dir]"];
            }

            return PopulateDataSearch(entity, filter, rowSkip, rowToTake, orderByField, orderByType, draw);
        }
        [NonAction]
        private JsonResult PopulateDataSearch(string entity, string filter = null, int rowSkip = 0, int rowToTake = 10, string orderByField = null, string orderByType = "ASC", string draw = "1")
        {
            JsonResult result = new JsonResult();

            try
            {
                if (entity == "CLIENT")
                {
                    List<string> listAllObjectProperties = new List<string>();
                    string propertiesOrderByField;
                    PagedResult<CLIENT> pagedResult = new PagedResult<CLIENT>(0, rowToTake);
                    typeof(CLIENT).GetProperties().ToList().ForEach(x =>
                    {
                        listAllObjectProperties.Add(x.Name.ToLower());
                    });

                    if (orderByField != null)
                    {
                        if (listAllObjectProperties.Contains(orderByField.ToLower()))
                        {
                            propertiesOrderByField = orderByField.ToUpper();
                        }
                        else
                        {
                            propertiesOrderByField = listAllObjectProperties.FirstOrDefault();
                        }
                    }
                    else
                    {
                        propertiesOrderByField = listAllObjectProperties.FirstOrDefault();
                    }


                    try
                    {
                        if (rowToTake != -1)
                        {
                            if (filter != null)
                            {

                                if (orderByType.ToUpper() == "ASC")
                                {
                                    pagedResult.Items = db.CLIENT.Where(filter).OrderBy(propertiesOrderByField).Skip(rowSkip).Take(rowToTake).ToList();
                                    pagedResult.TotalItems = db.CLIENT.Where(filter).Count();
                                    pagedResult.ItemsPerPage = rowToTake;

                                }
                                else if (orderByType.ToUpper() == "DESC")
                                {
                                    pagedResult.Items = db.CLIENT.Where(filter).OrderBy(propertiesOrderByField + " DESC").Skip(rowSkip).Take(rowToTake).ToList();
                                    pagedResult.TotalItems = db.CLIENT.Where(filter).Count();
                                    pagedResult.ItemsPerPage = rowToTake;

                                }
                            }
                            else
                            {
                                if (orderByType.ToUpper() == "ASC")
                                {
                                    pagedResult.Items = db.CLIENT.OrderBy(propertiesOrderByField).Skip(rowSkip).Take(rowToTake).ToList();
                                    pagedResult.TotalItems = db.CLIENT.Count();
                                    pagedResult.ItemsPerPage = rowToTake;

                                }
                                else if (orderByType.ToUpper() == "DESC")
                                {
                                    pagedResult.Items = db.CLIENT.OrderBy(propertiesOrderByField + " DESC").Skip(rowSkip).Take(rowToTake).ToList();
                                    pagedResult.TotalItems = db.CLIENT.Count();
                                    pagedResult.ItemsPerPage = rowToTake;

                                }
                            }
                        }
                        else
                        {
                            if (filter != null)
                            {

                                if (orderByType.ToUpper() == "ASC")
                                {
                                    pagedResult.Items = db.CLIENT.Where(filter).OrderBy(propertiesOrderByField).Skip(rowSkip).ToList();
                                    pagedResult.TotalItems = db.CLIENT.Where(filter).Count();
                                    pagedResult.ItemsPerPage = rowToTake;

                                }
                                else if (orderByType.ToUpper() == "DESC")
                                {
                                    pagedResult.Items = db.CLIENT.Where(filter).OrderBy(propertiesOrderByField + " DESC").Skip(rowSkip).ToList();
                                    pagedResult.TotalItems = db.CLIENT.Where(filter).Count();
                                    pagedResult.ItemsPerPage = rowToTake;

                                }
                            }
                            else
                            {
                                if (orderByType.ToUpper() == "ASC")
                                {
                                    pagedResult.Items = db.CLIENT.OrderBy(propertiesOrderByField).Skip(rowSkip).ToList();
                                    pagedResult.TotalItems = db.CLIENT.Count();
                                    pagedResult.ItemsPerPage = rowToTake;

                                }
                                else if (orderByType.ToUpper() == "DESC")
                                {
                                    pagedResult.Items = db.CLIENT.OrderBy(propertiesOrderByField + " DESC").Skip(rowSkip).ToList();
                                    pagedResult.TotalItems = db.CLIENT.Count();
                                    pagedResult.ItemsPerPage = rowToTake;

                                }
                            }
                        }

                        result = this.Json(new
                        {
                            draw = Convert.ToInt32(draw),
                            recordsTotal = pagedResult.TotalItems,
                            recordsFiltered = pagedResult.TotalItems,
                            data = pagedResult.Items.ToList()
                        }, JsonRequestBehavior.AllowGet);

                        return result;
                    }

                    catch (Exception e)
                    {
                        return this.Json(new
                        {
                            data = "Null",
                            message = e.Message

                        }, JsonRequestBehavior.AllowGet);
                    }

                }
                else if (entity == "DeliveryRequest")
                {
                    List<string> listAllObjectProperties = new List<string>();
                    string propertiesOrderByField;
                    PagedResult<DeliveryRequest> pagedResult = new PagedResult<DeliveryRequest>(0, rowToTake);
                    typeof(DeliveryRequest).GetProperties().ToList().ForEach(x =>
                    {
                        listAllObjectProperties.Add(x.Name.ToLower());
                    });

                    if (orderByField != null)
                    {
                        if (listAllObjectProperties.Contains(orderByField.ToLower()))
                        {
                            propertiesOrderByField = orderByField.ToUpper();
                        }
                        else
                        {
                            propertiesOrderByField = listAllObjectProperties.FirstOrDefault();
                        }
                    }
                    else
                    {
                        propertiesOrderByField = listAllObjectProperties.FirstOrDefault();
                    }


                    try
                    {
                        if (rowToTake != -1)
                        {
                            if (filter != null)
                            {

                                if (orderByType.ToUpper() == "ASC")
                                {
                                    pagedResult.Items = db.DeliveryRequest.Where(filter).OrderBy(propertiesOrderByField).Skip(rowSkip).Take(rowToTake).ToList();
                                    pagedResult.TotalItems = db.DeliveryRequest.Where(filter).Count();
                                    pagedResult.ItemsPerPage = rowToTake;

                                }
                                else if (orderByType.ToUpper() == "DESC")
                                {
                                    pagedResult.Items = db.DeliveryRequest.Where(filter).OrderBy(propertiesOrderByField + " DESC").Skip(rowSkip).Take(rowToTake).ToList();
                                    pagedResult.TotalItems = db.DeliveryRequest.Where(filter).Count();
                                    pagedResult.ItemsPerPage = rowToTake;

                                }
                            }
                            else
                            {
                                if (orderByType.ToUpper() == "ASC")
                                {
                                    pagedResult.Items = db.DeliveryRequest.OrderBy(propertiesOrderByField).Skip(rowSkip).Take(rowToTake).ToList();
                                    pagedResult.TotalItems = db.DeliveryRequest.Count();
                                    pagedResult.ItemsPerPage = rowToTake;

                                }
                                else if (orderByType.ToUpper() == "DESC")
                                {
                                    pagedResult.Items = db.DeliveryRequest.OrderBy(propertiesOrderByField + " DESC").Skip(rowSkip).Take(rowToTake).ToList();
                                    pagedResult.TotalItems = db.DeliveryRequest.Count();
                                    pagedResult.ItemsPerPage = rowToTake;

                                }
                            }
                        }
                        else
                        {
                            if (filter != null)
                            {

                                if (orderByType.ToUpper() == "ASC")
                                {
                                    pagedResult.Items = db.DeliveryRequest.Where(filter).OrderBy(propertiesOrderByField).Skip(rowSkip).ToList();
                                    pagedResult.TotalItems = db.DeliveryRequest.Where(filter).Count();
                                    pagedResult.ItemsPerPage = rowToTake;

                                }
                                else if (orderByType.ToUpper() == "DESC")
                                {
                                    pagedResult.Items = db.DeliveryRequest.Where(filter).OrderBy(propertiesOrderByField + " DESC").Skip(rowSkip).ToList();
                                    pagedResult.TotalItems = db.DeliveryRequest.Where(filter).Count();
                                    pagedResult.ItemsPerPage = rowToTake;

                                }
                            }
                            else
                            {
                                if (orderByType.ToUpper() == "ASC")
                                {
                                    pagedResult.Items = db.DeliveryRequest.OrderBy(propertiesOrderByField).Skip(rowSkip).ToList();
                                    pagedResult.TotalItems = db.DeliveryRequest.Count();
                                    pagedResult.ItemsPerPage = rowToTake;

                                }
                                else if (orderByType.ToUpper() == "DESC")
                                {
                                    pagedResult.Items = db.DeliveryRequest.OrderBy(propertiesOrderByField + " DESC").Skip(rowSkip).ToList();
                                    pagedResult.TotalItems = db.DeliveryRequest.Count();
                                    pagedResult.ItemsPerPage = rowToTake;

                                }
                            }
                        }

                        result = this.Json(new
                        {
                            draw = Convert.ToInt32(draw),
                            recordsTotal = pagedResult.TotalItems,
                            recordsFiltered = pagedResult.TotalItems,
                            data = pagedResult.Items.ToList()
                        }, JsonRequestBehavior.AllowGet);

                        return result;
                    }

                    catch (Exception e)
                    {
                        return this.Json(new
                        {
                            data = "Null",
                            message = e.Message

                        }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return this.Json(new
                    {
                        data = "Null",
                        message = "entitas \"" + entity + "\" not found"

                    }, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {
                return this.Json(new
                {
                    data = "Null",
                    message = ex.Message

                }, JsonRequestBehavior.AllowGet);
            }


        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}