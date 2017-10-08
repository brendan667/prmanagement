using System;

namespace LP.PRManagement.Common.Models.Domain.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBaseDalModel
    {
        int Id { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
