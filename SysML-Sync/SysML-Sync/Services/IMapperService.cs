namespace SysML_Sync.Services
{
    public interface IMapperService
    {
        Task<List<Mapper>> GetAllAsync();

    }
}
