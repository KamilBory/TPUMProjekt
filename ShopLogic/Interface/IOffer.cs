namespace ShopLogic.Interface
{
    public interface IOffer
    {
        int id { get; set; }
        int sellPrice { get; set; }
        string name { get; set; }
        string description { get; set; }
        int count { get; set; }
    }
}
