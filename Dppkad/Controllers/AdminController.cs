using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dppkad.DAL;
using Dppkad.Models;
using System.Linq.Expressions;

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
            var model = new RealisasiSkpdModelView();

            //var bannerResponses = _service.GetBannerInfo();

            //model.BannerViewInfo = bannerResponses as IList<BannerInfo> ?? bannerResponses.ToList();

            //ViewBag.BannerViewInfo = model.BannerViewInfo;

            var beritaResponses = _service.GetBeritaInfo();
            model.BeritaViewInfo = beritaResponses as IList<BeritaSkpdInfo> ?? beritaResponses.ToList();
            ViewBag.BeritaViewInfo = model.BeritaViewInfo;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            Expression<Func<RealisasiSkpdModelInfo, bool>> filter = null;

            try
            {
                #region Set Filter

                if (param.sSearch != null)
                    filter = (c => c.UnitSkpd.Contains(param.sSearch)
                                                    //|| SqlFunctions.StringConvert((decimal)c.NilaiKontrak).Contains(param.sSearch)
                                                    || c.Status.Contains(param.sSearch));

                var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
                Expression<Func<RealisasiSkpdModelInfo, string>> ordering = (c => sortColumnIndex == 0 ? c.No :
                                                                                        sortColumnIndex == 1 ? c.UnitSkpd :
                                                                                            c.Status);

                var sortDirection = Request["sSortDir_0"]; // asc or desc

                #endregion

                var responses = _service.GetRealisasiSkpdList(filter, param.iDisplayLength, param.iDisplayStart, ordering, sortDirection);

                var rowIndex = 1;
                var result = (from x in responses
                              select new
                              {
                                  No = rowIndex++,
                                  x.UnitSkpd,
                                  TotalBudget = string.Format("{0:#,##0}", x.TotalBudget),
                                  TotalRealisasi = string.Format("{0:#,##0}", x.TotalRealisasi),
                                  x.Persentase,
                                  SisaBudget = string.Format("{0:#,##0}", x.SisaBudget),
                                  x.Status
                              });

                // Return Json
                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = _service.TotalRealisasiSkpd(),
                    iTotalDisplayRecords = _service.TotalRealisasiSkpd(filter),
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
                    iTotalRecords = _service.TotalRealisasiSkpd(),
                    iTotalDisplayRecords = _service.TotalRealisasiSkpd(filter),
                    aaData = new RealisasiSkpdModelInfo()
                },
               JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetBannerInfo()
        {
            var model = new RealisasiSkpdModelView();

            var bannerResponses = _service.GetBannerInfo();

            model.BannerViewInfo = bannerResponses as IList<BannerSkpdInfo> ?? bannerResponses.ToList();

            return Json(model.BannerViewInfo, JsonRequestBehavior.AllowGet);
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

                var result = (from x in responses
                              select new
                              {
                                  x.No,
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

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetChartInfo()
        {
            var model = new RealisasiSkpdModelView();

            var chartResponses = _service.GetChartInfo();

            var result = (from x in chartResponses
                          select new
                          {
                              TahunBudget = x.TahunBudget,
                              Budget = string.Format("{0:#,##0}", x.Budget),
                              Realisasi = string.Format("{0:#,##0}", x.Realisasi)
                          });

            model.ChartViewInfo = chartResponses as IList<ChartSkpdInfo> ?? chartResponses.ToList();

            return Json(model.ChartViewInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RealisasiSkpd()
        {
            var model = new RealisasiSkpdModelUpdate();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RealisasiSkpd(RealisasiSkpdModelUpdate model)
        {
            if (!ModelState.IsValid)
            {

            }
            return View(model);
        }
    }
}