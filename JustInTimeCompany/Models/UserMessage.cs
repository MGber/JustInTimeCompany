namespace JustInTimeCompany.Models
{
    public class UserMessage
    {
        public Guid Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual string Message { get; set; }
    }
}