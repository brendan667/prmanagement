using LP.PRManagement.Common.Models.Domain.Interface;
using System;

namespace LP.PRManagement.Common.Models.Domain.Base
{
    public class BaseDalModel : IBaseDalModel
    {
        public BaseDalModel()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get ; set ; }
        public DateTime CreatedDate { get; set; }
    }
}
