using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GachaGame.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public int Level { get; set; }

        public ICollection<GachaRecord> GachaRecords { get; set; } = new List<GachaRecord>();
    }
    public class GachaRecord
    {

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public string ItemName { get; set; }

        public string Rarity { get; set; }

        public DateTime ObtainedAt { get; set; }
    }
}
