using ClosedXML.Excel;
using FinApp.Entities.Finance;
using FinApp.service.ifaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Mvc;

namespace FinApp.Controllers
{
    [Authorize]
    public class ExportController : Controller
    {
        private IDepositoryService depositoryService;
        public ExportController(IDepositoryService depositoryService)
        {
            this.depositoryService = depositoryService;
        }

        public FileResult ExportById(int idDepository)
        {
            var idUser = User.Identity.GetUserId();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Date"),
                                            new DataColumn("Category"),
                                            new DataColumn("Comment"),
                                            new DataColumn("Value") });
            List<FinanceOperation> history = depositoryService.historyById(idDepository, idUser);
            foreach (FinanceOperation fp in history)
            {
                dt.Rows.Add(fp.created, Enum.GetName(typeof(Category), fp.category), fp.comment, fp.isSpending ? ("-" + fp.amountOfMoney) : ("+" + fp.amountOfMoney));
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