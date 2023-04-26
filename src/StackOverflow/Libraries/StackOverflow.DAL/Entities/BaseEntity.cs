using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.DAL.Entities;

public abstract class BaseEntity : IEntity<Guid>
{
    public virtual Guid Id { get; set; }
    public virtual Guid OwnerId { get; set; }
    public virtual int VoteCount { get; set; }
    public virtual DateTime TimeStamp { get; set; }
}
