namespace SysML_Sync.Services
{
    public interface IModdelService
    {
        Task<List<Model>> GetAllAsync();

    }
}
