using System;

namespace api.Models
{
    public abstract class Resource
    {
        public int Id { get; set; }
        public DateTime Creation_dt { get; set; }
    }
}