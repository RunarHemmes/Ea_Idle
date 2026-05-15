using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Ea_API.Models
{
    public class Connection
    {
        [Key]
        public int Id { get; set; }

        [AllowNull]
        [ForeignKey(nameof(Account.Id))]
        public int ParentId { get; set; }

        [AllowNull]
        [ForeignKey(nameof(Account.Id))]
        public int ChildId { get; set; }

        public TimeOnly TimeLimit { get; set; }

        public Connection()
        {
            TimeLimit = new TimeOnly(23, 59, 59); 
        }
    }
}
