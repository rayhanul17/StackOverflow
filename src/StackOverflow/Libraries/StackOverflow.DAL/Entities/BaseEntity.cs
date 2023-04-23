using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.DAL.Entities;

public abstract class BaseEntity
{
    public virtual int VoteCount { get; set; }
    public virtual DateTime TimeStamp { get; set; }

}
