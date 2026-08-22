using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.Domain.Common;
public abstract class AuditableEntity:BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get;set; }
    public string? Notes {  get; set; }
    public bool IsActive { get; set; }=true;


}
