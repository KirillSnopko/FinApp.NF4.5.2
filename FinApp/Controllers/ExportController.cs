using ClosedXML.Excel;
using FinApp.Entities.Finance;
using FinApp.service.ifaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinApp.Controllers
{
    [Authorize]
    public class ExportController : Controller
    {
        private IFinanceService financeService;
        public ExportController(IFinanceService financeService)
        {
            this.financeService = financeService;
        }

        public FileResult ExportById(int idDepository)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Date"),
                                            new DataColumn("Category"),
                                            new DataColumn("Comment"),
                                            new DataColumn("Value") });
            List<FinanceOperation> history = financeService.OperationRepo().getByIdDepository(idDepository);
            foreach (FinanceOperation fp in history)
            {
                dt.Rows.Add(fp.created, Enum.GetName(typeof(Category), fp.category), fp.comment, fp.isSpending? ("-"+ fp.amountOfMoney):("+" + fp.amountOfMoney));
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"History{DateTime.Now.ToString("MM-dd-yyyy")}.xlsx");
                }
            }
        }
    }
}