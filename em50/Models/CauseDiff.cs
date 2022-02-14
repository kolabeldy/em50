namespace em50.Models;
public class CauseDiff
{
    public int Id { get; set; }
    public int IdUse { get; set; }
    public int IdCause { get; set; }
    public string IdCode { get; set; }
    public string Name { get; set; }
    public string Code1Name { get; set; }
    public string Code2Name { get; set; }
    public double Diff { get; set; }
    public string Note { get; set; }

    public static List<CauseDiff> Get(int id = 0)
    {
        var res = Get<CauseDiff>(id);
        return res;
    }
    public static List<T> Get<T>(int id = 0)
    {
        string iduse = id > 0 ? iduse = id.ToString() : "";
        List<T> result = new();
        List<CauseDiff> list = new();
        string sql = "SELECT Id, IdUse, IdCause, IdCode, Name, Code1Name, Code2Name, Diff, Note FROM CauseDiffs WHERE IdUse = " + iduse;
        DataTable dt = new DataTable();
        dt = Sqlite.Select(Global.dbpath, sql);
        list = (from DataRow dr in dt.Rows
                select new CauseDiff()
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    IdUse = Convert.ToInt32(dr["IdUse"]),
                    IdCause = Convert.ToInt32(dr["IdCause"]),
                    IdCode = dr["IdCode"].ToString(),
                    Name = dr["Name"].ToString(),
                    Code1Name = dr["Code1Name"].ToString(),
                    Code2Name = dr["Code2Name"].ToString(),
                    Diff = Convert.ToDouble(dr["Diff"]),
                    Note = dr["Note"].ToString()
                }).ToList();
        result.AddRange((IEnumerable<T>)list);
        return result;
    }
}
