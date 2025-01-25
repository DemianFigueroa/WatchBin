namespace WatchBin.Domain.Repositories
{
    public interface ILoadDataRepository
    {
        Task LoadDataFromJson(string filePath);
    }
}