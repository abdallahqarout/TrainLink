using DAL.Entities;
namespace DAL.Services
{
    public class ReportReviewService : IReportReviewService
    {
        private readonly AppDbContext _context;
        public ReportReviewService(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<ReportReview> GetAll()
        {
            return _context.ReportReviews;
        }
        public IQueryable<ReportReview> GetById(int reportReviewId)
        {
            return _context.ReportReviews
                .Where(x => x.ReportReviewId == reportReviewId);
        }
        public IQueryable<ReportReview> GetByWeeklyReportId(int weeklyReportId)
        {
            return _context.ReportReviews
                .Where(x => x.WeeklyReportId == weeklyReportId);
        }
        public IQueryable<ReportReview> GetByFinalReportId(int finalReportId)
        {
            return _context.ReportReviews
                .Where(x => x.FinalReportId == finalReportId);
        }
        public IQueryable<ReportReview> GetByReviewerUserId(int reviewerUserId)
        {
            return _context.ReportReviews
                .Where(x => x.ReviewerUserId == reviewerUserId);
        }
        public async Task Add(ReportReview reportReview)
        {
            _context.ReportReviews.Add(reportReview);
            await _context.SaveChangesAsync();
        }
        public async Task Update(ReportReview reportReview)
        {
            _context.ReportReviews.Update(reportReview);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int reportReviewId)
        {
            var reportReview = await _context.ReportReviews.FindAsync(reportReviewId);
            if (reportReview != null)
            {
                _context.ReportReviews.Remove(reportReview);
                await _context.SaveChangesAsync();
            }
        }
    }
}
