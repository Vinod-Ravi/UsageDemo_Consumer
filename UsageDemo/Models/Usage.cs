using System.ComponentModel.DataAnnotations;

namespace UsageDemo.Models
{
    public class Usage
    {
        [Key]
        public Guid Id { get; set; }
        public string? UsageName { get; set; }
        public string? UsageKey { get; set; }
        public string? UsageServices { get; set; }
        public string? UsageCharge { get; set; }
        public DateTime UsageDate { get; set; }

    }
}
