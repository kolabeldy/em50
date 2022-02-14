namespace em50.Models;
public class Cause
{
    public int IdUse { get; set; }
    public int IdCause { get; set; }
    public double Diff { get; set; }
    public string Note { get; set; }



    public static List<Cause> Get(List<Cause> sourceData, int id)
    {
        List<Cause> result = new List<Cause>();

        result = (from o in sourceData
                  where o.IdUse == id 
                  orderby o.IdCause
                  select new Cause()
                  {
                      IdUse = id,
                      IdCause = o.IdCause,
                      Diff = o.Diff,
                      Note = o.Note
                  }).ToList();

        return result;
    }
}