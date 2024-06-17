namespace FatecSisMed.IdentityServer.SeedDataBase.Interfaces
{
    public interface IDataBaseInitializer
    {
        void InitializeSeedRoles();
        void InitializeSeedUsers();
    }
}
