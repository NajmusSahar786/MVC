using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test_PartialView.Controllers
{
    public class PolicyController : Controller
    {
        // GET: Policy
        public ActionResult Member()
        {
            return View();
        }
        public PartialViewResult _InsuredDetailPartialNew()
        {
            InsuredDetailNew insuredDetailNew = new InsuredDetailNew();
            int quoteNo = 0;
            string ProposerType = string.Empty, PropType, policyType = string.Empty;
            if (!string.IsNullOrEmpty(Encryption.GetQueryStringValue("QuoteNo")))
            {
                quoteNo = Convert.ToInt32(Encryption.GetQueryStringValue("QuoteNo"));
                insuredDetailNew = _beneficiaryService.GetInsuredDetailNew(quoteNo);
            }
            else
            {
                quoteNo = Convert.ToInt32(Request.QueryString["QuoteNo"]);
                insuredDetailNew = _beneficiaryService.GetInsuredDetailNew(quoteNo);
                ViewBag.QuoteNo = quoteNo;
            }
            if (Convert.ToInt32(TempData["DocumentQuote"]) != 0)
            {
                quoteNo = Convert.ToInt32(TempData["DocumentQuote"]);
                ProposerType = TempData["DocumentProposerType"].ToString();
                PropType = TempData["DocumentPropType"].ToString();
                policyType = TempData["DocumentpolicyType"].ToString();
                ViewBag.QuoteNo = quoteNo;
                insuredDetailNew = _beneficiaryService.GetInsuredDetailNew(quoteNo);
            }
            return PartialView("_InsuredDetailPartialNew", insuredDetailNew);
        }
    }
}