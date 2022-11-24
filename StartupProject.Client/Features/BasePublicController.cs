namespace StartupProject.Client.Features
{
    /// <summary>
    /// This class will be inherited by all public controllers.
    /// </summary>
    public abstract class BasePublicController : BaseController
    {
        /// <summary>
        /// Inject common services
        /// </summary>
        public BasePublicController()
        {
        }
    }
}