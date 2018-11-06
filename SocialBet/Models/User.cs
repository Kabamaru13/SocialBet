namespace SocialBet.Models
{
    public class User
    {
        /// <summary>
        /// The ID of the user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The user's name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The user's email
        /// </summary>
        public string Email { get; set; }
    }
}
