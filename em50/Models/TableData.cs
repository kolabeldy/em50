namespace em50.Models;
public class TableData
{
    public int Period { get; set; }
    public string PeriodStr { get; set; }
    public int IdCC { get; set; }
    public string CCName { get; set; }
    public bool IsMainCC { get; set; }
    public int IdER { get; set; }
    public string ERName { get; set; }
    public bool IsMain { get; set; }
    public bool IsPrime { get; set; }
    public int Unit { get; set; }
    public string UnitName { get; set; }

    public double FactUse { get; set; }
    public double PlanUse { get; set; }
    public double DiffUse { get; set; }
    public double FactUseCost { get; set; }
    public double PlanUseCost { get; set; }
    public double DiffUseCost { get; set; }
    public double DiffProc { get; set; }

    public static List<TableData> Get(List<DataUse> sourceData)
    {
        List<TableData> result = new List<TableData>();

        result = (from o in sourceData
                  group o by new { o.Period, o.IdCC, o.IdER } into gr
                  orderby gr.Key.Period, gr.Key.IdCC, gr.Key.IdER
                  select new TableData()
                  {
                      Period = gr.Key.Period,
                      PeriodStr = gr.Key.Period.ToString().Insert(4,"_"),
                      IdCC = gr.Key.IdCC,
                      IdER = gr.Key.IdER,
                      ERName = gr.Max(m => m.ERName),
                      UnitName = gr.Max(m => m.UnitName),
                      FactUse = gr.Sum(m => m.Fact),
                      PlanUse = gr.Sum(m => m.Plan),
                      DiffUse = gr.Sum(m => m.Diff),
                      FactUseCost = gr.Sum(m => m.FactCost),
                      PlanUseCost = gr.Sum(m => m.PlanCost),
                      DiffUseCost = gr.Sum(m => m.DiffCost),
                      DiffProc = Math.Round(gr.Sum(m => m.Diff), 1) * 100 / Math.Round(gr.Sum(m => m.Plan), 1)
                  }).ToList();

        return result;
    }
}