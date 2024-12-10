using System.ComponentModel.DataAnnotations;

namespace ShijiGroup.Models
{
    public class Sequence
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; }
        public int Number { get; set; } = 0;
    }
}
