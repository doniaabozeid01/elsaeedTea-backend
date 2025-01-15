using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elsaeedTea.data.Entities;

namespace elsaeedTea.service.Services.AuthenticationServices
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(ApplicationUser user);

    }
}
