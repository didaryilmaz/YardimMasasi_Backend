namespace YardimMasasi_Backend.Services
{
    public interface IReportService
    {
        Task<object> GetCategoryFrequencyAsync();
        Task<object> GetPriorityFrequencyAsync();
    }
}
