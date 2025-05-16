namespace KooliProjekt.WpfApp.Api
{
    public interface IApiClient
    {
        Task<List<Artist>> List();
        Task Save(Artist list);
        Task Delete(int id);
    }
}