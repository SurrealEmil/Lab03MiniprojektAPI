namespace Lab03MiniprojektAPI.Models
{
    public class WebsiteLink
    {
        public int Id { get; set; }
        public string Link { get; set; }

        public virtual Interest Interests { get; set; }
        public virtual Person Persons { get; set; }
    }
}
