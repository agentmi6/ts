namespace TicketSystem.Domain
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public string TicketTitle { get; set; }       

        public string ScreenshotURL { get; set; }
        public string Description { get; set; }
        
        public string ApplicationUserID { get; set; }           
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public int PriorityID { get; set; }
        public virtual Priority Priority { get; set; }
    }
}