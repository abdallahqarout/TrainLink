using DAL.Entities;

namespace DAL.Services
{
    public class FinalReportService : IFinalReportService
    {
        private readonly AppDbContext _context;
        public FinalReportService(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<FinalReport> GetAll()
        {
            return _context.FinalReports;
        }
        public IQueryable<FinalReport> GetById(int finalReportId)
        {
            return _context.FinalReports
                .Where(x => x.FinalReportId == finalReportId);
        }
        public IQueryable<FinalReport> GetByTrainingId(int trainingId)
        {
            return _context.FinalReports
                .Where(x => x.TrainingId == trainingId);
        }
        public async Task Add(FinalReport report)
        {
            _context.FinalReports.Add(report);
            await _context.SaveChangesAsync();
        }
        public async Task Update(FinalReport report)
        {
            _context.FinalReports.Update(report);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int finalReportId)
        {
            var report = await _context.FinalReports.FindAsync(finalReportId);
            if (report != null)
            {
                _context.FinalReports.Remove(report);
                await _context.SaveChangesAsync();
            }
        }
    }
}
