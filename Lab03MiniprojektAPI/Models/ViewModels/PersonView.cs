namespace Lab03MiniprojektAPI.Models.ViewModels
{
    public class PersonView
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<InterestView> Interests { get; set; }
        public virtual ICollection<WebsiteLinkView> WebsiteLinks { get; set; }
    }
}
