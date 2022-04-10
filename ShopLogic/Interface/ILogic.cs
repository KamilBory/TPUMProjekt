namespace ShopLogic.Interface
{
    public interface ILogic
    {
        // TODO functions throw exceptions on failure

        IClientLogic GetClientLogic(int clientId, string password);

        int RegisterClient(string name, string surname, string password);

        // potential extension
        // IOwnerLogic GetOwnerLogic();
    }
}
