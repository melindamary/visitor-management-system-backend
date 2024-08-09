using Microsoft.Extensions.Logging; // Add this using directive
using VMS.Models.DTO;
using VMS.Repository.IRepository;
using VMS.Services.IServices;

namespace VMS.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IVisitorRepository _visitorRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<VisitorService> _logger;

        public VisitorService(IVisitorRepository visitorRepository, IUserRepository userRepository, ILogger<VisitorService> logger)
        {
            _visitorRepository = visitorRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<int> GetActiveVisitorsCountToday()
        {
            try
            {
                _logger.LogInformation("Fetching active visitors count for today.");
                var count = await _visitorRepository.GetVisitorCount(v => v.Where(visitor => visitor.CheckInTime != null && visitor.CheckOutTime == null));
                _logger.LogInformation("Active visitors count for today: {Count}", count);
                return count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching active visitors count for today.");
                throw;
            }
        }

        public async Task<int> GetTotalVisitorsCountToday()
        {
            try
            {
                _logger.LogInformation("Fetching total visitors count for today.");
                var count = await _visitorRepository.GetVisitorCount(v => v.Where(visitor => visitor.CheckInTime != null || visitor.CheckOutTime != null));
                _logger.LogInformation("Total visitors count for today: {Count}", count);
                return count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching total visitors count for today.");
                throw;
            }
        }

        public async Task<int> GetCheckedOutVisitorsCountToday()
        {
            try
            {
                _logger.LogInformation("Fetching checked-out visitors count for today.");
                var count = await _visitorRepository.GetVisitorCount(v => v.Where(visitor => visitor.CheckOutTime != null));
                _logger.LogInformation("Checked-out visitors count for today: {Count}", count);
                return count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching checked-out visitors count for today.");
                throw;
            }
        }

        public async Task<IEnumerable<VisitorLogDTO>> GetVisitorDetailsToday()
        {
            try
            {
                _logger.LogInformation("Fetching visitor details for today.");
                var details = await _visitorRepository.GetVisitorLogs(v => v.Where(visitor => visitor.CheckInTime != null || visitor.CheckOutTime != null));
                _logger.LogInformation("Fetched {Count} visitor details for today.", details.Count());
                return details;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching visitor details for today.");
                throw;
            }
        }

        public async Task<IEnumerable<VisitorLogDTO>> GetUpcomingVisitorsToday()
        {
            try
            {
                _logger.LogInformation("Fetching upcoming visitors for today.");
                var upcomingVisitors = await _visitorRepository.GetVisitorLogs(v => v.Where(visitor => visitor.CheckInTime == null));
                _logger.LogInformation("Fetched {Count} upcoming visitors for today.", upcomingVisitors.Count());
                return upcomingVisitors;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching upcoming visitors for today.");
                throw;
            }
        }

        public async Task<IEnumerable<VisitorLogDTO>> GetActiveVisitorsToday()
        {
            try
            {
                _logger.LogInformation("Fetching active visitors for today.");
                var activeVisitors = await _visitorRepository.GetVisitorLogs(v => v.Where(visitor => visitor.CheckInTime != null && visitor.CheckOutTime == null));
                _logger.LogInformation("Fetched {Count} active visitors for today.", activeVisitors.Count());
                return activeVisitors;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching active visitors for today.");
                throw;
            }
        }

        public async Task<IEnumerable<VisitorLogDTO>> GetCheckedOutVisitorsToday()
        {
            try
            {
                _logger.LogInformation("Fetching checked-out visitors for today.");
                var checkedOutVisitors = await _visitorRepository.GetVisitorLogs(v => v.Where(visitor => visitor.CheckInTime != null && visitor.CheckOutTime != null));
                _logger.LogInformation("Fetched {Count} checked-out visitors for today.", checkedOutVisitors.Count());
                return checkedOutVisitors;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching checked-out visitors for today.");
                throw;
            }
        }

        public async Task<VisitorLogDTO> UpdateCheckInTimeAndCardNumber(int id, UpdateVisitorPassCodeDTO updateVisitorPassCode)
        {
            try
            {
                var exisitingVisitor = await _visitorRepository.GetVisitorByIdAsync(id);

                if (exisitingVisitor == null)
                {
                    _logger.LogWarning("Visitor with id {VisitorId} not found during check-in update.", id);
                    return null;
                }

                var user = await _userRepository.GetUserByUsernameAsync(updateVisitorPassCode.Username);
                if (user == null)
                {
                    _logger.LogWarning("User with username {Username} not found.", updateVisitorPassCode.Username);
                    return null;
                }

                exisitingVisitor.UpdatedBy = user.Id;
                exisitingVisitor.CheckedInBy = user.Id;
                var checkedInVisitor = await _visitorRepository.UpdateCheckInTimeAndCardNumber(id, updateVisitorPassCode);
                
                _logger.LogInformation("Visitor with id {VisitorId} check-in time and pass code updated by user with id {UserId}.", id, user.Id);

                return checkedInVisitor; // Return the result
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating check-in time and pass code for visitor with id {VisitorId}.", id);
                throw;
            }
        }
        public async Task<VisitorLogDTO> UpdateCheckOutTime(int id)
        {
            try
            {
                _logger.LogInformation("Updating check-out time for visitor ID {VisitorId}.", id);
                var checkedOutVisitor = await _visitorRepository.UpdateCheckOutTime(id);

                if (checkedOutVisitor == null)
                {
                    _logger.LogWarning("Visitor ID {VisitorId} not found during check-out update.", id);
                }
                else
                {
                    _logger.LogInformation("Successfully updated check-out time for visitor ID {VisitorId}.", id);
                }

                return checkedOutVisitor;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating check-out time for visitor ID {VisitorId}.", id);
                throw;
            }
        }
    }
}
