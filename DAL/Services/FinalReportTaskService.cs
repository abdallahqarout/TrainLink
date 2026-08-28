using DAL.Entities;

namespace DAL.Services
{
    public class FinalReportTaskService : IFinalReportTaskService
    {
        private readonly AppDbContext _context;
        public FinalReportTaskService(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<FinalReportTask> GetAll()
        {
            return _context.FinalReportTasks;
        }
        public IQueryable<FinalReportTask> GetById(int finalReportTaskId)
        {
            return _context.FinalReportTasks
                .Where(x => x.FinalReportTaskId == finalReportTaskId);
        }
        public IQueryable<FinalReportTask> GetByFinalReportId(int finalReportId)
        {
            return _context.FinalReportTasks
                .Where(x => x.FinalReportId == finalReportId);
        }
        public async Task Add(FinalReportTask task)
        {
            await _context.FinalReportTasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }
        public async Task Update(FinalReportTask task)
        {
            _context.FinalReportTasks.Update(task);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int finalReportTaskId)
        {
            var task = await _context.FinalReportTasks.FindAsync(finalReportTaskId);
            if (task != null)
            {
                _context.FinalReportTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
