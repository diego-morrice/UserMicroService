using System;

namespace Domain.User.Interfaces.Common
{
    public interface IAuditableEntity
    {
        DateTime CreatedDate { get; set; }
        DateTime LastUpdatedDate { get; set; }
        DateTime DeletedDate { get; set; }
    }
}
