namespace ITHSystems.Enums;

public enum Modules
{
    #region Home Modules
    NONE,
    DELIVERIES,
    PICKUPSERVICE,
    #endregion

    #region Deliveries Modules
    PENDINGDELIVERIES,
    DELAYEDDELIVERIES,
    DELIVERESSHIPMENTSNOTSYNCED,
    SYNCHRONIZED,
    SETTINGS,
    #endregion

    #region Pickup Service Modules
    PENDINGSUITCASE,
    RECEIVESUITCASE,
    DELIVERSUITCASE
    #endregion
}
