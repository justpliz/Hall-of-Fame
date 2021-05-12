using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Hall_of_Fame.Models.Data
{
    public class Skill
    {

        public string Name { get; set; }

        public byte Level { get; set; }

        public long PersonId { get; set; }

        public virtual Person Person { get; set; }
        public long Id { get; set; }
    }
}
