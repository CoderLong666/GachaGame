
namespace GachaGame.Domain.Models
{
    public class User : Entity
    {
        public string Username { get; set; } = string.Empty;

        public int Level { get; set; }

        public virtual ICollection<GachaRecord> GachaRecords { get; set; } = new HashSet<GachaRecord>();
    }
    public class GachaRecord : Entity
    {
        public int UserId { get; set; }
        public virtual User? User { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public string Rarity { get; set; } = string.Empty;

        public DateTime ObtainedAt { get; set; }
    }
}
