using System.Collections.Generic;

namespace StartupProject.Core.Security.Identity
{
    public class ApplicationIdentityResult
    {
        public List<string> Errors
        {
            get;
            private set;
        }

        public bool Succeeded
        {
            get;
            private set;
        }

        public ApplicationIdentityResult(List<string> errors, bool succeeded)
        {
            Succeeded = succeeded;
            Errors = errors;
        }
    }
}
