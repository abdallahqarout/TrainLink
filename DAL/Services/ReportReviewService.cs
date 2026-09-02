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

        public IQueryable<ReportReview> GetById(int reviewId)
        {
            return _context.ReportReviews
                .Where(x => x.ReviewId == reviewId);
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

        public async Task Delete(int reviewId)
        {
            var review = await _context.ReportReviews.FindAsync(reviewId);

            if (review != null)
            {
                _context.ReportReviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}