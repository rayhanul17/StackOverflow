namespace StackOverflow.Services.Services;

public class TimeService : ITimeService
{
    public DateTime Now => DateTime.UtcNow;
}
