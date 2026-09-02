using DAL.Entities;

namespace DAL.Services
{
    public interface IReportReviewService
    {
        IQueryable<ReportReview> GetAll();
        IQueryable<ReportReview> GetById(int reviewId);
        IQueryable<ReportReview> GetByWeeklyReportId(int weeklyReportId);
        IQueryable<ReportReview> GetByFinalReportId(int finalReportId);
        IQueryable<ReportReview> GetByReviewerUserId(int reviewerUserId);
        Task Add(ReportReview reportReview);
        Task Update(ReportReview reportReview);
        Task Delete(int reviewId);
    }
}