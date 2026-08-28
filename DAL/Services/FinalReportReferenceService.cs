using DAL.Entities;

namespace DAL.Services
{
    public class FinalReportReferenceService : IFinalReportReferenceService
    {
        private readonly AppDbContext _context;
        public FinalReportReferenceService(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<FinalReportReference> GetAll()
        {
            return _context.FinalReportReferences;
        }
        public IQueryable<FinalReportReference> GetById(int finalReportReferenceId)
        {
            return _context.FinalReportReferences
                .Where(x => x.FinalReportReferenceId == finalReportReferenceId);
        }
        public IQueryable<FinalReportReference> GetByFinalReportId(int finalReportId)
        {
            return _context.FinalReportReferences
                .Where(x => x.FinalReportId == finalReportId);
        }
        public async Task Add(FinalReportReference reference)
        {
            await _context.FinalReportReferences.AddAsync(reference);
            await _context.SaveChangesAsync();
        }
        public async Task Update(FinalReportReference reference)
        {
            _context.FinalReportReferences.Update(reference);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int finalReportReferenceId)
        {
            var reference = await _context.FinalReportReferences.FindAsync(finalReportReferenceId);
            if (reference != null)
            {
                _context.FinalReportReferences.Remove(reference);
                await _context.SaveChangesAsync();
            }
        }
    }
}
