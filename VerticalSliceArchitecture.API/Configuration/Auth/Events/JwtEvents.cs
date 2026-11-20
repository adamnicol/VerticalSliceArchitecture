using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace VerticalSliceArchitecture.API.Configuration.Auth.Providers;

public class JwtEvents : JwtBearerEvents
{
    public override Task TokenValidated(TokenValidatedContext context)
    {
        throw new NotImplementedException();
    }
}
