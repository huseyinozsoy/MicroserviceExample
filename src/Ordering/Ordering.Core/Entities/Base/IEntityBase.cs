using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Core.Entities.Base
{
    interface IEntityBase<TId>
    {
        TId Id { get; }
    }
}
