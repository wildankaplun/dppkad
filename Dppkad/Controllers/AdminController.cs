using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dppkad.DAL;
using Dppkad.Models;
using System.Linq.Expressions;
using Dppkad.Models.Entities;
using System.IO;
using System.Threading;
using System.Data.Entity.SqlServer;

namespace Dppkad.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UsersInfo()
        {
            var model = new UserDppkadUpdate();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UsersInfo(UserDppkadUpdate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return RedirectToAction("UsersInfo", "Admin");
        }

        public ActionResult BannerInfo()
        {
            var model = new BannerSkpdUpdate();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BannerInfo(IEnumerable<HttpPostedFileBase> files)
        {
            try
            {
                if (files.Any(c => c.FileName.Any()))
                {
                    var model = new BannerSkpdUpdate();
                    var bannerOpt = _service.GetBannerInfoById(true);
                    try
                    {
                        foreach (var item in bannerOpt)
                        {
                            _service.DeleteBannerInfo(item);
                        }
                        _service.SaveContext();

                        int index = 1;
                        foreach (var file in files)
                        {
                            if (file.ContentLength > 0)
                            {
                                model.BannerName = Path.GetFileName(file.FileName);
                                model.BannerPath = Server.MapPath("~/Content/images");
                                model.SortOrder = index++;
                                model.ActiveFlag = true;

                                TBanner banner = new TBanner()
                                {
                                    BannerName = model.BannerName,
                                    BannerPath = string.Format("/{0}/{1}/", "Content", "images"),
                                    SortOrder = model.SortOrder,
                                    ActiveFlag = model.ActiveFlag
                                };

                                _service.AddBannerInfo(banner);

                                FileInfo fileInfo = new FileInfo(Path.Combine(model.BannerPath, model.BannerName));
                                if (!fileInfo.Exists)
                                {
                                    file.SaveAs(Path.Combine(model.BannerPath, model.BannerName));
                                }
                            }
                        }
                        //
                        _service.SaveContext();

                        ViewBag.Success = "Picture Files has successfully uploaded.";
                    }
                    catch (System.IO.IOException ex)
                    {
                        Console.WriteLine(ex.Message);
                        return View(model);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return View(model);
                    }
                    return View();
                }
            }
            catch(Exception ex)
            {
                return View();
            }
            return View();
        }

        public ActionResult NewsInfo()
        {
            var model = new BeritaSkpdUpdate();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewsInfo(BeritaSkpdUpdate model)
        {
            if (ModelState.IsValid)
            {
                TBerita berita = new TBerita()
                {
                    News = model.News,
                    CreatedDate = DateTime.Now,
                    ActiveFlag = true
                };
                _service.AddBeritaInfo(berita);
                _service.SaveContext();

                ViewBag.Success = "Berita has successfully saved.";

                return RedirectToAction("NewsInfo", "Admin");
            }
            return View(model);
        }

        public ActionResult AgendaInfo()
        {
            var model = new AgendaSkpdUpdate();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AjaxAgendaHandler(JQueryDataTableParamModel param)
        {
            Expression<Func<AgendaSkpdInfo, bool>> filter = null;

            try
            {
                #region Set Filter

                var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
                Expression<Func<AgendaSkpdInfo, string>> ordering = (c => sortColumnIndex == 0 ? c.No.ToString() :
                                                                                        sortColumnIndex == 1 ? c.Hari :
                                                                                            c.Jam);

                var sortDirection = Request["sSortDir_0"]; // asc or desc

                #endregion

                var responses = _service.GetAgendaInfoList(filter, param.iDisplayLength, param.iDisplayStart, ordering, sortDirection);

                var rowIndex = 1;
                var result = (from x in responses
                              select new
                              {
                                  No = rowIndex++,
                                  x.Hari,
                                  x.Tanggal,
                                  x.Jam,
                                  x.Uraian,
                                  x.Keterangan
                              });

                // Return Json
                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = _service.TotalAgendaInfo(),
                    iTotalDisplayRecords = _service.TotalAgendaInfo(filter),
                    aaData = result.ToList()
                },
               JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                // Return Json
                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = _service.TotalAgendaInfo(),
                    iTotalDisplayRecords = _service.TotalAgendaInfo(filter),
                    aaData = new AgendaSkpdInfo()
                },
               JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AgendaInfo(AgendaSkpdUpdate model)
        {
            if (ModelState.IsValid)
            {
                TAgenda agenda = new TAgenda()
                {
                    Hari = model.Hari,
                    Tanggal = model.Tanggal,
                    Jam = model.Jam,
                    Uraian = model.Uraian,
                    Keterangan = model.Keterangan
                };
                _service.AddAgendaInfo(agenda);
                _service.SaveContext();

                ViewBag.Success = "Agenda has successfully saved.";

                return RedirectToAction("AgendaInfo", "Admin");
            }
            return View(model);
        }

        public ActionResult GraphInfo()
        {
            var model = new ChartSkpdUpdate();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AjaxGraphHandler(JQueryDataTableParamModel param)
        {
            Expression<Func<ChartSkpdInfo, bool>> filter = null;

            try
            {
                #region Set Filter

                var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
                Expression<Func<ChartSkpdInfo, string>> ordering = (c => sortColumnIndex == 0 ? c.TahunBudget.ToString() :
                                                                                        sortColumnIndex == 1 ? SqlFunctions.StringConvert((decimal)c.Budget) :
                                                                                            SqlFunctions.StringConvert((decimal)c.Realisasi));

                var sortDirection = Request["sSortDir_0"]; // asc or desc

                #endregion

                var responses = _service.GetGraphInfoList(filter, param.iDisplayLength, param.iDisplayStart, ordering, sortDirection);

                var rowIndex = 1;
                var result = (from x in responses
                              select new
                              {
                                  No = rowIndex++,
                                  x.TahunBudget,
                                  Budget = string.Format("{0:#,##0}", x.Budget),
                                  Realisasi = string.Format("{0:#,##0}", x.Realisasi)
                              });

                // Return Json
                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = _service.TotalGraphInfo(),
                    iTotalDisplayRecords = _service.TotalGraphInfo(filter),
                    aaData = result.ToList()
                },
               JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                // Return Json
                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = _service.TotalGraphInfo(),
                    iTotalDisplayRecords = _service.TotalGraphInfo(filter),
                    aaData = new ChartSkpdInfo()
                },
               JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GraphInfo(ChartSkpdUpdate model)
        {
            if (ModelState.IsValid)
            {
                TGrafik graph = new TGrafik()
                {
                    TahunBudget = model.TahunBudget,
                    Budget = model.Budget ?? 0,
                    Realisasi = model.Realisasi ?? 0
                };
                _service.AddGraphInfo(graph);
                _service.SaveContext();

                ViewBag.Success = "Grafik has successfully saved.";

                return View();
            }
            return View(model);
        }

        public ActionResult RealisasiSkpd()
        {
            var model = new RealisasiSkpdModelUpdate();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RealisasiSkpd(RealisasiSkpdModelUpdate model)
        {
            if (ModelState.IsValid)
            {
                TRealisasiSkpd realisasi = new TRealisasiSkpd()
                {
                    UnitSkpd = model.UnitSkpd,
                    TotalBudget = model.TotalBudget,
                    TotalRealisasi = model.TotalRealisasi,
                    SisaBudget = (model.TotalBudget - model.TotalRealisasi),
                    Persentase = (int?)Math.Round((model.TotalRealisasi / model.TotalBudget) * 100) ?? 0,
                    Status = CheckStatus(model.TotalRealisasi, model.TotalBudget),
                };
                _service.AddRealisasiInfo(realisasi);
                _service.SaveContext();

                ViewBag.Success = "Realisasi has successfully saved.";
                return View();
            }
            return View(model);
        }

        private string CheckStatus(decimal totalRea, decimal totalBudget)
        {
            var checkResult = (int?)Math.Round((totalRea / totalBudget) * 100);
            if (checkResult >= 85 && checkResult <= 100)
            {
                return "SANGAT BAIK";
            }
            if (checkResult >= 71 && checkResult <= 84)
            {
                return "BAIK";
            }
            if (checkResult >= 65 && checkResult <= 70)
            {
                return "SEDANG";
            }
            if (checkResult >= 50 && checkResult <= 64)
            {
                return "KURANG";
            }
            return "SANGAT KURANG";
        }
    }
}