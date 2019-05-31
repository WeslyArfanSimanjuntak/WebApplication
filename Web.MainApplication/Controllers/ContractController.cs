using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Repository.Application.DataModel;

namespace Web.MainApplication.Controllers
{
    public class ContractController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: Contract
        public ActionResult Index()
        {
            var cONTRACT = db.CONTRACT;
            return View(cONTRACT.ToList());
        }

        public ActionResult IndexClientContract()
        {
            var userMapping = db.UserClientMapping.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            if (userMapping == null)
            {
                WarningMessagesAdd("User Mapping is not found");
                return View(new List<CONTRACT>());
            }
            var contract = db.CONTRACT.Where(x => x.ClientIdPK == userMapping.ClientId);
            return View(contract.ToList());
        }

        // GET: Contract/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTRACT cONTRACT = db.CONTRACT.Find(id);
            if (cONTRACT == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParamSetupTonToM3 = db.ParamSetupTonToM3();
            return View(cONTRACT);
        }

        // GET: Contract/Create
        public ActionResult Create()
        {
            var contract = new CONTRACT();
            var lastContract = db.CONTRACT.OrderByDescending(x => x.ContractId).FirstOrDefault();


            contract.ContractNumber = WebAppUtility.ContractIdGenerator(lastContract != null ? lastContract.ContractId + 1 : 1);
            contract.Line = 0;
            contract.MaxExpiredDR = 0;
            var selectListSite = new List<SelectListItem>();
            selectListSite.AddBlank();
            db.SITE.ToList().ForEach(x =>
            {
                selectListSite.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
            });
            ViewBag.SiteName = selectListSite.ToSelectList();
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem()
            {
                Value = "",
                Text = "---"
            });
            db.CLIENT.OrderBy(x => x.ClientName).ToList().ForEach(x =>
              {
                  selectList.Add(new SelectListItem()
                  {
                      Value = x.Id.ToString(),
                      Text = x.ClientType == "individual" ? x.ClientId + " - " + x.ClientName : x.ClientId + " - " + x.ClientCompanyName
                  });

              });
            ViewBag.ClientIdPK = new SelectList(selectList, "Value", "Text");
            new List<ContractProduct>().Add(new ContractProduct()
            {
                Amount = 0,
                PricePerTon = 0
            });
            contract.ContractProduct = new List<ContractProduct>();
            var lsiProduct = new List<SelectListItem>();
            lsiProduct.AddBlank();
            db.PRODUCT.ToList().ForEach(x =>
            {
                lsiProduct.AddItemValText(x.PRODUCTID, x.PRODUCTID + " - " + x.PRODUCTNAME);
            });
            ViewBag.Product = lsiProduct.ToSelectList();
            float convertTonToM3;
            float.TryParse((string)db.ParameterSetup.Where(x => x.Name == "Convertion(ton to m3)").FirstOrDefault().Value, out convertTonToM3);
            ViewBag.TonUnitToM3Unit = convertTonToM3;
            var sliQuantityBasedOn = new List<SelectListItem>();
            sliQuantityBasedOn.AddBlank();
            sliQuantityBasedOn.AddItemValText("m3","M3");
            sliQuantityBasedOn.AddItemValText("ton", "Ton");
            ViewBag.sliQuantityBasedOn = sliQuantityBasedOn;
            var sliPpn = new List<SelectListItem>();
            sliPpn.AddBlank();
            ViewBag.IsActive = WebAppUtility.SelectListIsActive(contract.IsActive);

            sliPpn.AddItemValText("Y", "Yes");
            sliPpn.AddItemValText("N", "No");
            ViewBag.sliQuantityBasedOn = sliQuantityBasedOn;
            ViewBag.sliPpn = sliPpn;
            return View(contract);
        }

        // POST: Contract/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContractId,ContractNumber,Line,StartPeriode,EndPeriode,ClientIdPK,EffectiveDate,MaxExpiredDR,SiteName,Remark,Value")] CONTRACT cONTRACT)
        {
            var totalProduct = Request.Params["totalProduct"];
            if (totalProduct == null)
            {
                WarningMessagesAdd("Total Product Must Be Greater Than 0.");
            }
            if (cONTRACT.StartPeriode > cONTRACT.EndPeriode)
            {
                WarningMessagesAdd("End Periode Must Be Greater Than Start Periode");
            }
            if (cONTRACT.Line < 0)
            {
                WarningMessagesAdd("Line Must Be Equals Greater Than 0");
            }
            if (cONTRACT.MaxExpiredDR < 0)
            {
                WarningMessagesAdd("Delivery Request Expired Date Must Be Uquals Greater Than 0");
            }

            var listOfProduct = new List<ContractProduct>();
            cONTRACT.Value = 0;
            for (int a = 0; a < Convert.ToInt32(totalProduct); a++)
            {
                var contractProduct = new ContractProduct();
                contractProduct.ContractId = cONTRACT.ContractId;
                contractProduct.ProductId = Request.Params["ProductId_" + (a + 1).ToString()];
                contractProduct.Amount = Convert.ToDouble(Request.Params["Amount_" + (a + 1).ToString()]);
                contractProduct.PricePerTon = Convert.ToDecimal(Request.Params["PricePerTon_" + (a + 1).ToString()]);
                listOfProduct.Add(contractProduct);

                cONTRACT.Value = cONTRACT.Value + ((decimal)contractProduct.Amount * contractProduct.PricePerTon);
            }

            if (cONTRACT.Value < cONTRACT.Line)
            {
                WarningMessagesAdd("Contract Credit Line Can Not Be Greater Than Contract Value <br />" +
                    "Contract Value = " + cONTRACT.Value + " <br/>" +
                    "Contract Credit Line = " + cONTRACT.Line);
            }

            // Checking for duplicate product
            if (listOfProduct.Select(x => new { x.ProductId }).GroupBy(z => z).Any(y => y.Count() > 1))
            {
                WarningMessagesAdd("Duplicate Product.");
            }
            if (ModelState.IsValid && WarningMessages().Count == 0)
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        cONTRACT.SetPropertyCreate();
                        db.CONTRACT.Add(cONTRACT);
                        db.SaveChanges();
                        listOfProduct.ForEach(x =>
                        {
                            x.ContractId = cONTRACT.ContractId;
                            x.SetPropertyCreate();
                        });
                        db.ContractProduct.AddRange(listOfProduct);
                        var newFinanceBalance = new FinanceBalance();
                        newFinanceBalance.ContractId = cONTRACT.ContractId;
                        newFinanceBalance.AvailableAmount = cONTRACT.Line;
                        newFinanceBalance.ActualAmount = 0;
                        newFinanceBalance.SetPropertyCreate();
                        db.FinanceBalance.Add(newFinanceBalance);
                        db.SaveChanges();
                        transaction.Commit();
                        SuccessMessagesAdd("Inserting Contract " + cONTRACT.ContractNumber + " Success");
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        WarningMessagesAdd(e.Message);
                        transaction.Rollback();

                    }

                }

            }
            var lastContract = db.CONTRACT.OrderByDescending(x => x.ContractId).FirstOrDefault();


            cONTRACT.ContractNumber = WebAppUtility.ContractIdGenerator(lastContract != null ? lastContract.ContractId + 1 : 1);

            var selectListSite = new List<SelectListItem>();
            selectListSite.AddBlank();
            db.SITE.ToList().ForEach(x =>
            {
                selectListSite.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
            });
            ViewBag.SiteName = selectListSite.ToSelectList(cONTRACT.SiteName);
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem()
            {
                Value = "",
                Text = "---"
            });
            db.CLIENT.OrderBy(x => x.ClientName).ToList().ForEach(x =>
            {
                selectList.Add(new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.ClientType == "individual" ? x.ClientId + " - " + x.ClientName : x.ClientId + " - " + x.ClientCompanyName

                });

            });
            ViewBag.ClientIdPK = new SelectList(selectList, "Value", "Text");

            cONTRACT.ContractProduct = listOfProduct;
            var lsiProduct = new List<SelectListItem>();
            lsiProduct.AddBlank();
            db.PRODUCT.ToList().ForEach(x =>
            {
                lsiProduct.AddItemValText(x.PRODUCTID, x.PRODUCTID + " - " + x.PRODUCTNAME);
            });
            ViewBag.Product = lsiProduct.ToSelectList();
            float convertTonToM3;
            float.TryParse((string)db.ParameterSetup.Where(x => x.Name == "Convertion(ton to m3)").FirstOrDefault().Value, out convertTonToM3);
            ViewBag.TonUnitToM3Unit = convertTonToM3;
            ViewBag.IsActive = WebAppUtility.SelectListIsActive(cONTRACT.IsActive);

            return View(cONTRACT);
        }

        // GET: Contract/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTRACT cONTRACT = db.CONTRACT.Find(id);
            if (cONTRACT == null)
            {
                return HttpNotFound();
            }
            var selectListProduct = new List<SelectListItem>();
            selectListProduct.AddBlank();
            db.PRODUCT.OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
            {
                selectListProduct.Add(new SelectListItem()
                {
                    Value = x.PRODUCTID,
                    Text = x.PRODUCTID + " - " + x.PRODUCTNAME
                });

            });
            var selectList = new List<SelectListItem>();
            selectList.AddBlank();

            db.CLIENT.OrderBy(x => x.ClientName).ToList().ForEach(x =>
            {
                selectList.Add(new SelectListItem()
                {
                    Value = x.ClientId,
                    Text = x.ClientType == "individual" ? x.ClientId + " - " + x.ClientName : x.ClientId + " - " + x.ClientCompanyName

                });

            });

            ViewBag.ClientId = selectList.ToSelectList(cONTRACT.ClientIdPK);
            ViewBag.ProductId = selectListProduct.ToSelectList(cONTRACT.ContractProduct);
            ViewBag.IsActive = WebAppUtility.SelectListIsActive(cONTRACT.IsActive);

            return View(cONTRACT);
        }

        // POST: Contract/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContractId,ContractNumber,Line,ProductId,StartPeriode,EndPeriode,ClientId")] CONTRACT cONTRACT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONTRACT).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cONTRACT);
        }

        // GET: Contract/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTRACT cONTRACT = db.CONTRACT.Find(id);
            if (cONTRACT == null)
            {
                return HttpNotFound();
            }
            return View(cONTRACT);
        }

        // POST: Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CONTRACT cONTRACT = db.CONTRACT.Find(id);
            db.CONTRACT.Remove(cONTRACT);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UserClientCreate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTRACT cONTRACT = db.CONTRACT.Find(id);
            if (cONTRACT == null)
            {
                return HttpNotFound();
            }
            var newUSerMapping = new UserClientMapping();
            var suggestedUser = new AspNetUsers();

            var suggestedUserName = cONTRACT.CLIENT.ClientName.Replace(" ", "").Count() > 10 ? cONTRACT.CLIENT.ClientName.Replace(" ", "").Substring(0, 10) : cONTRACT.CLIENT.ClientName.Replace(" ", "");
            suggestedUser.Username = suggestedUserName;

            newUSerMapping.AspNetUsers = suggestedUser;
            newUSerMapping.UserName = suggestedUserName;
            var sliGroup = new List<SelectListItem>();
            sliGroup.AddBlank();

            db.AspNetGroups.OrderBy(x => x.GroupName).ToList().ForEach(x =>
            {
                sliGroup.Add(new SelectListItem()
                {
                    Value = x.GroupName,
                    Text = x.GroupName + " - " + x.GroupDescription

                });

            });

            ViewBag.sliGroup = sliGroup.ToSelectList(cONTRACT.ClientIdPK);
            ViewBag.ContracId = cONTRACT.ContractId;
            return View(newUSerMapping);
        }
        [HttpPost]
        public ActionResult UserClientCreate(UserClientMapping userClientMapping)
        {
            if (Request.Params["ContractId"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var password = Request.Params["DefaultPassword"];
            var reTypePassword = Request.Params["RetypeDefaultPassword"];
            long contractId;
            var test = Request.Params["test"];
            long.TryParse(Request.Params["Contractid"], out contractId);
            CONTRACT cONTRACT = db.CONTRACT.Find(contractId);
            if (cONTRACT == null)
            {
                return HttpNotFound();
            }

            if (password != reTypePassword)
            {
                WarningMessagesAdd("Password and Retypepassword is not match.");
            }
            if (string.IsNullOrWhiteSpace(userClientMapping.UserName))
            {
                WarningMessagesAdd("Username can not be null or whitespace");
            }
            if (password.Length < 10)
            {
                WarningMessagesAdd("Password Length must be greater than 10 character");
            }
            if (Request.Params["sliGroup"] == null) {

                WarningMessagesAdd("Group User can be blank");
            }
            if (ModelState.IsValid && WarningMessages().Count == 0)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var aspNetUser = new AspNetUsers();
                        aspNetUser.Username = userClientMapping.UserName;
                        aspNetUser.Password = Service.System.Encriptor.SHA1(password);
                        aspNetUser.CreatedDate = DateTime.Now;
                        aspNetUser.CreatedBy = User.Identity.Name;
                        aspNetUser.FullName = cONTRACT.CLIENT.ClientType == "individual" ? cONTRACT.CLIENT.ClientName : cONTRACT.CLIENT.ClientCompanyName;
                        aspNetUser.Email = cONTRACT.CLIENT.ClientEmail;
                        db.AspNetUsers.Add(aspNetUser);
                        db.SaveChanges();

                        userClientMapping.UserName = aspNetUser.Username;
                        userClientMapping.ClientId = Convert.ToInt32(cONTRACT.ClientIdPK);
                        userClientMapping.SetPropertyCreate();

                        db.UserClientMapping.Add(userClientMapping);

                        var newUserGroup = new AspNetGroupUser();
                        newUserGroup.GroupName = Request.Params["sliGroup"];
                        newUserGroup.Username = userClientMapping.UserName;
                        newUserGroup.SetPropertyCreate();
                        db.AspNetGroupUser.Add(newUserGroup);

                        db.SaveChanges();
                        transaction.Commit();
                        SuccessMessagesAdd("Creating User Success");
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        WarningMessagesAdd(e.Message);
                        transaction.Rollback();
                    }

                }

            }

            var newUSerMapping = new UserClientMapping();
            var suggestedUser = new AspNetUsers();

            var suggestedUserName = cONTRACT.CLIENT.ClientName.Replace(" ", "").Count() > 10 ? cONTRACT.CLIENT.ClientName.Replace(" ", "").Substring(0, 10) : cONTRACT.CLIENT.ClientName.Replace(" ", "");
            suggestedUser.Username = suggestedUserName;

            userClientMapping.AspNetUsers = suggestedUser;
            userClientMapping.UserName = suggestedUserName;
            ViewBag.ContracId = cONTRACT.ContractId;
            var sliGroup = new List<SelectListItem>();
            sliGroup.AddBlank();

            db.AspNetGroups.OrderBy(x => x.GroupName).ToList().ForEach(x =>
            {
                sliGroup.Add(new SelectListItem()
                {
                    Value = x.GroupName,
                    Text = x.GroupName + " - " + x.GroupDescription

                });

            });

            ViewBag.sliGroup = sliGroup.ToSelectList(cONTRACT.ClientIdPK);
            return View(userClientMapping);
        }
        [HttpGet]
        public ActionResult UserClientDetails(long? clientId)
        {
            if (clientId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userClientMaping = db.UserClientMapping.Where(x => x.ClientId == clientId).FirstOrDefault();
            if (userClientMaping == null)
            {
                return HttpNotFound();
            }

            var aspNetUser = db.AspNetUsers.Where(x => x.Username == userClientMaping.UserName).FirstOrDefault();
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }

            return View(aspNetUser);

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
