using FlightPlannerCore.Interfaces;

namespace FlightPlannerCore.Services
{
    public class ServiceResult
    {
        public ServiceResult(bool success)
        {
            Success = success;
        }

        public ServiceResult SetEntity(IEntity entity)
        {
            Entity = entity;
            return this;
        }

        public ServiceResult AddError(string error)
        {
            Error.Add(error);
            return this;
        }

        public bool Success { get; private set; }

        public IEntity Entity { get; private set; } 

        public IList<string> Error { get; private set; }

        public string FormattedErrors => string.Join(",", Error);
    }
}
