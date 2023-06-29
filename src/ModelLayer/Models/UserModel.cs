using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class UserModel
    {
        // TODO mistake in example 'surename' file
        public int id { get; set; }
        public string name { get; set; }
        public string surename { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
}
