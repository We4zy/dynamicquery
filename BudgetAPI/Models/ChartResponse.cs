using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BudgetAPI.Models
{
    public class ColumnChartResponse
    {
        public string[] Catagories { get; set; }
        public SeriesItem[] Series { get; set; }
        public IGrouping<string, decimal[]> SeriesGrouped {get;set;}
        public BudgetGridItem[] GridData { get; set; }
    }

    public class BudgetGridItem
    {
        public string ProgramDesc { get; set; }
        public decimal Budget { get; set; }
        public decimal BudgetYTD { get; set; }
    }

    public class SeriesItem
    {
        public string name { get; set; }
        public decimal[] data { get; set; }
    }
}