using System;
namespace SocialBet.Helpers
{
    public enum ErrorCode
    {
        NoError = 0,
        InvalidAuthentication = 11,
        AuthenticationGeneric = 12,
        RegistrationGeneric = 13,
        UserGetAll = 14,
        UserGet = 15,
        UserUpdate = 16,
        UserDelete = 17,

        BetGetAll = 21,
        BetGet = 22,
        BetScope = 23,
        BetReferree = 24,
        BetCreate = 25,
        BetUpdate = 26,
        BetDelete = 27,

        States = 31,
        State = 32,
        BetCategories = 33,
        BetCategory = 34,
        PrizeCategories = 35,
        PrizeCategory = 36
    }
}
