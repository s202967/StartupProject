namespace StartupProject.Core.Domain.Enum
{
    public enum AssetStatus
    {
        /// <summary>
        /// ReDraft ie. send back for review to data entry from L1 approval
        /// </summary>
        ReDraft = -1,

        /// <summary>
        /// Initially saved as a draft ie. send for approval
        /// </summary>
        PendingApproval = 0,

        /// <summary>
        /// L1 Approved
        /// </summary>
        L1Approved = 1,

        /// <summary>
        /// L2 Approved
        /// </summary>
        L2Approved = 2,

        /// <summary>
        /// L3 Approved
        /// </summary>
        L3Approved = 3
    }
}
