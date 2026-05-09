using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ea_API.Models
{
    [PrimaryKey(nameof(ParentId), nameof(ChildId))]
    public class Connection
    {
        [ForeignKey(nameof(Account.Id))]
        public int ParentId { get; set; }

        [ForeignKey(nameof(Account.Id))]
        public int ChildId { get; set; }

        public TimeOnly TimeLimit { get; set; }

        public Connection()
        {
            //ParentId = parent;
            //ChildId = child;
        }
    }
}
