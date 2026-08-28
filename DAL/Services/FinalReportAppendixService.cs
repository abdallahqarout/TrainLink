using DAL.Entities;
namespace DAL.Services
{
    public class FinalReportAppendixService : IFinalReportAppendixService
    {
        private readonly AppDbContext _context;
        public FinalReportAppendixService(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<FinalReportAppendix> GetAll()
        {
            return _context.FinalReportAppendices;
        }
        public IQueryable<FinalReportAppendix> GetById(int finalReportAppendixId)
        {
            return _context.FinalReportAppendices
                .Where(x => x.FinalReportAppendixId == finalReportAppendixId);
        }
        public IQueryable<FinalReportAppendix> GetByFinalReportId(int finalReportId)
        {
            return _context.FinalReportAppendices
                .Where(x => x.FinalReportId == finalReportId);
        }
        public async Task Add(FinalReportAppendix finalReportAppendix)
        {
            await _context.FinalReportAppendices.AddAsync(finalReportAppendix);
            await _context.SaveChangesAsync();
        }
        public async Task Update(FinalReportAppendix finalReportAppendix)
        {
            _context.FinalReportAppendices.Update(finalReportAppendix);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int finalReportAppendixId)
        {
            var finalReportAppendix = await _context.FinalReportAppendices.FindAsync(finalReportAppendixId);
            if (finalReportAppendix != null)
            {
                _context.FinalReportAppendices.Remove(finalReportAppendix);
                await _context.SaveChangesAsync();
            }
        }
    }
}
