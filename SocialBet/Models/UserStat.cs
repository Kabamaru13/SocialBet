namespace SocialBet.Models
{
    public class UserStat
    {
        public int Id { get; set; }
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

        public UserStat(int id)
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
