namespace OsiguSDK.SpecificationTests.Tools
{
    public enum ClaimAmountRange
    {
        LESS_THAN_2800,
        BETWEEN_2800_AND_33600,
        GREATER_THAN_33600,
        EXACT_AMOUNT
    }

    public enum ProviderType
    {
        IsNotRetainingAgent = 0,
        IsRetainingAgent = 1
    }

    public enum SettlementType
    {
        Normal,
        Cashout
    }

    public enum SettlementStatus
    {
        CREATED,
        PRINTED,
        SENT,
        ACCOUNTED
    }
}