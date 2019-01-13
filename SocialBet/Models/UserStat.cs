namespace SocialBet.Models
{
    // bets = wins + losses + draws + cancelled + (pending + inprogress)
    // on bet creation bets++ for both users
    // on bet cancellation cancels++ for both users
    // on bet completion 
    // |-> referreed++ only the referree user
    // |-> in case of draw - draws++ for both users
    // |-> in case of winner - wins++ for winner, losses++ for loser
    // |-> winner awarded with exp points, loser gets nothing, draw also nothing
    // -------------------------------------------------------------------------
    // Leveling system - For every lvl gained xp zeroed - xp awarded for a win is depended on opponents lvl
    // lvl1 -> lvl2: 100xp  (0 + 100)
    // lvl2 -> lvl3: 250xp  (100 + 150)
    // lvl3 -> lvl4: 550xp  (350 + 200)
    // lvl4 -> lvl5: 1150xp (900 + 250)
    // getXPForLvl(x) { int base=100; int ret=0; for (i=1; i<x; i++) { ret+=base; base+=50; } return ret; }
    public class UserStat
    {
        public string Id { get; set; }
        public int NumOfBets { get; set; }
        public int NumOfWins { get; set; }
        public int NumOfLosses { get; set; }
        public int NumOfDraws { get; set; }
        public int NumOfReferreed { get; set; }
        public int NumOfCancelled { get; set; }
        public int BadgesEarned { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public string Title { get; set; }

        public UserStat(string id)
        {
            Id = id;
            NumOfBets = 0;
            NumOfWins = 0;
            NumOfLosses = 0;
            NumOfDraws = 0;
            NumOfReferreed = 0;
            NumOfCancelled = 0;
            BadgesEarned = 0;
            Level = 1;
            Experience = 0;
            Title = "Newbie";
        }
    }
}
