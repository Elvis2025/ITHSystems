using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITHSystems.DTOs;

public sealed record class CurrentLoginDto
{
    public UserDto User { get; set; } = new();
    public TenantDto Tenant { get; set; } = new();

}
