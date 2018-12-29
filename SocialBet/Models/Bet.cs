using System;
namespace SocialBet.Models
{
    public class Bet
    {
        /// <summary>
        /// The ID of the bet.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Bet's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The ID of the creator.
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// The ID of the rival.
        /// </summary>
        public string RivalId { get; set; }

        /// <summary>
        /// The ID of the referree.
        /// </summary>
        public string ReferreeId { get; set; }

        /// <summary>
        /// The starting date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The ending date.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The bet category id.
        /// </summary>
        public int BetCategoryId { get; set; }

        /// <summary>
        /// The bet description.
        /// </summary>
        public string BetDescription { get; set; }

        /// <summary>
        /// The prize category id.
        /// </summary>
        public int PrizeCategoryId { get; set; }

        /// <summary>
        /// The bet description.
        /// </summary>
        public string PrizeDescription { get; set; }

        /// <summary>
        /// The bet state.
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// The ID of the winner.
        /// </summary>
        public string WinnerId { get; set; }
    }
}
