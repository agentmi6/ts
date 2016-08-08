using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Domain
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
              
        public virtual ICollection<Ticket> _Tickets { get; set; }
    }
}
