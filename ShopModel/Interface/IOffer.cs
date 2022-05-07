namespace ShopModel.Interface
{
    public interface IOffer
    {
        int id { get; set; }
        string name { get; set; }
        string description { get; set; }
        int price { get; set; }
    }
}
