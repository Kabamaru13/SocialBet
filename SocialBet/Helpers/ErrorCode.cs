namespace SocialBet.Helpers
{
    public enum ErrorCode
    {
        NoError = 0,
        UsernameAvailability = 10,
        InvalidAuthentication = 11,
        AuthenticationGeneric = 12,
        RegistrationGeneric = 13,
        UserGetAll = 14,
        UserGet = 15,
        UserUpdate = 16,
        UserDelete = 17,
        UserStats = 18,
        RegistrationBypass = 19,

        BetGetAll = 20,
        BetGet = 21,
        BetScope = 22,
        BetReferree = 23,
        BetCreate = 24,
        BetUpdate = 25,
        BetDelete = 26,
        BetCancel = 27,
        BetAccept = 28,
        BetComplete = 29,

        States = 31,
        State = 32,
        BetCategories = 33,
        BetCategory = 34,
        PrizeCategories = 35,
        PrizeCategory = 36
    }
}
