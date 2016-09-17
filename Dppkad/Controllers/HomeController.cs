using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dppkad.DAL;
using Dppkad.Models;
using System.Linq.Expressions;
using System.Data.Entity.SqlServer;

namespace Dppkad.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRealisasiBelanjaService _service;

        public HomeController(IRealisasiBelanjaService service)
        {
            _service = service;
        }

        // GET: Home
        public ActionResult Index()
        {
            var model = new RealisasiBelanjaModelView();

            //var bannerResponses = _service.GetBannerInfo();

            //model.BannerViewInfo = bannerResponses as IList<BannerInfo> ?? bannerResponses.ToList();

            //ViewBag.BannerViewInfo = model.BannerViewInfo;

            var beritaResponses = _service.GetBeritaInfo();

            model.BeritaViewInfo = beritaResponses as IList<BeritaInfo> ?? beritaResponses.ToList();

            ViewBag.BeritaViewInfo = model.BeritaViewInfo;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            Expression<Func<RealisasiBelanjaModelInfo, bool>> filter = null;

            try
            {
                #region Set Filter

                if (param.sSearch != null)
                    filter = (c => c.NoSPM.Contains(param.sSearch)
                                                    || c.NoSP2D.Contains(param.sSearch)
                                                    || c.Unit.Contains(param.sSearch)
                                                    || c.Kegiatan.Contains(param.sSearch)
                                                    || c.Pihak3.Contains(param.sSearch)
                                                    || c.Kota.Contains(param.sSearch)
                                                    || SqlFunctions.StringConvert((decimal)c.NilaiKontrak).Contains(param.sSearch)
                                                    || c.Status.Contains(param.sSearch));

                var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
                Expression<Func<RealisasiBelanjaModelInfo, string>> ordering = (c => sortColumnIndex == 0 ? c.NoSPM :
                                                                                        sortColumnIndex == 1 ? c.NoSP2D :
                                                                                            c.Pihak3);

                var sortDirection = Request["sSortDir_0"]; // asc or desc

                #endregion

                var responses = _service.GetRealisasiBelanjaList(filter, param.iDisplayLength, param.iDisplayStart, ordering, sortDirection);

                var result = (from x in responses
                              select new
                              {
                                  x.No,
                                  x.NoSPM,
                                  x.NoSP2D,
                                  TglSP2D = string.Format("{0:dd/MM/yyyy}", x.TglSP2D),
                                  x.Unit,
                                  x.Kegiatan,
                                  x.Pihak3,
                                  x.Kota,
                                  NilaiKontrak = string.Format("{0:#,##0}", x.NilaiKontrak),
                                  x.Status
                              });

                // Return Json
                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = _service.TotalRealisasiBelanja(),
                    iTotalDisplayRecords = _service.TotalRealisasiBelanja(filter),
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
                    iTotalRecords = _service.TotalRealisasiBelanja(),
                    iTotalDisplayRecords = _service.TotalRealisasiBelanja(filter),
                    aaData = new RealisasiBelanjaModelInfo()
                },
               JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetBannerInfo()
        {
            var model = new RealisasiBelanjaModelView();

            var bannerResponses = _service.GetBannerInfo();

            model.BannerViewInfo = bannerResponses as IList<BannerInfo> ?? bannerResponses.ToList();

            return Json(model.BannerViewInfo, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AjaxAgendaHandler(JQueryDataTableParamModel param)
        {
            Expression<Func<AgendaInfo, bool>> filter = null;

            try
            {
                #region Set Filter

                var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
                Expression<Func<AgendaInfo, string>> ordering = (c => sortColumnIndex == 0 ? c.No.ToString() :
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
                    aaData = new AgendaInfo()
                },
               JsonRequestBehavior.AllowGet);
            }
        }
    }
}