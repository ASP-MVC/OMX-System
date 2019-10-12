namespace OMX.Domain
{
    using System;

    public class Comment : BaseEntity<int>
    {
        public string Content { get; set; }

        public DateTime CommentedAt { get; set; }

        public bool IsRead { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        public string RecipientId { get; set; }

        public User Recipient { get; set; }

        public int? AdId { get; set; }

        #nullable enable
        public Ad? Ad { get; set; }
    }
}
