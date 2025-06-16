namespace YardimMasasi_Backend.Services
{
    public interface IReportService
    {
        Task<IEnumerable<object>> GetCategoryFrequencyAsync();
        Task<IEnumerable<object>> GetPriorityFrequencyAsync();
    }
}
