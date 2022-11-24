namespace StartupProject.Services.Helpers.CustomDate
{
    public class DateFactory : IDateFactory
    {
        public DateFactory()
        {

        }

        public DateHelper GetDateHelper()
        {
            return new NepaliDateService();
        }
    }
}
