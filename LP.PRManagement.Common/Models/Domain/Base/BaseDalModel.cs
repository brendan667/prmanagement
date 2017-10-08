using LP.PRManagement.Common.Models.Domain.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LP.PRManagement.Common.Models.Domain.Base
{
    public class BaseDalModel : IBaseDalModel
    {
        public BaseDalModel()
        {
            CreatedDate = DateTime.Now;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get ; set ; }
        public DateTime CreatedDate { get; set; }
    }
}
