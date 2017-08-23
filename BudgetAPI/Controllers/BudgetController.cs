using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BudgetAPI.Models;
using System.Data.Linq;

namespace BudgetAPI.Controllers
{
    public class BudgetController : ApiController
    {
        /// <summary>
        /// A sample method used to proto-type the WebAPI.  Returns all Yearly budgets and YTD usage from SPD test data/statistics
        /// No filter capabilities are contained within the method
        /// </summary>
        /// <returns></returns>  
        [HttpGet]
        public ColumnChartResponse GetBudgetCategories()
        {
            ColumnChartResponse resp = new ColumnChartResponse();
            FMSBudgetDataContext db = new FMSBudgetDataContext();

            resp.Catagories = db.vwFMSBudgets.Select(b => b.ProgramDesc).Distinct().ToArray();
            resp.GridData = db.vwFMSBudgets.Where(b => b.Year == 2016).Select(b => new BudgetGridItem { ProgramDesc = b.ProgramDesc, Budget = (decimal)b.Budget, BudgetYTD = (decimal)b.BudgetUtilized }).ToArray();

            SeriesItem[] series = (from b in db.vwFMSBudgets
                                   group b by b.ProgramDesc into budget
                                   select new SeriesItem { name = budget.Key, data = new decimal[] { (decimal)budget.Sum(b => b.Budget), (decimal)budget.Sum(b => b.BudgetUtilized) } }).ToArray(); 

            List<decimal> Budgets = new List<decimal>(); List<decimal> YTDs = new List<decimal>();
            foreach (SeriesItem item in series)
            {
                Budgets.Add(item.data[0]);
                YTDs.Add(item.data[1]);
            }

            resp.Series = new SeriesItem[] {
                new SeriesItem() { name = "Budget", data = Budgets.ToArray() },
                new SeriesItem() { name = "YTD", data = YTDs.ToArray() }
            };

            return resp;
        }

        public ColumnChartResponse GetBudgetByCategory(string category)
        {

            return null;
        }
    }
}