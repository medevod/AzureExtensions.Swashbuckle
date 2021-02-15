using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sample2
{
    public class Order
    {
        [Required]
        public string Id { get; set; }        

        public string TenanId { get; set; }

        public List<string> Filters { get; set; } = new List<string>();

        public Dictionary<string,string> MetaDatas { get; set; } = new Dictionary<string, string>();

    }
}
