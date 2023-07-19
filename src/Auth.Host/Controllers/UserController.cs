using Auth.Domain.Entities;
using Auth.Host.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Auth.Host.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public UserController(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> CreateUserAsync(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            return result;
        }

        [Authorize(AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)]
        [HttpGet("~/connect/userinfo")]
        public async Task<IActionResult> UserInfo()
        {
            var user = await _userManager.FindByIdAsync(User.GetClaim(Claims.Subject));

            if (user == null)
            {
                return Challenge(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidToken,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                            "The specified access token is bound to an account that no longer exists."
                    }));
            }

            var claims = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                // Note: the "sub" claim is a mandatory claim and must be included in the JSON response.
                [Claims.Subject] = user.Id
            };

            if (User.HasScope(Scopes.Email))
            {
                claims[Claims.Email] = user.Email;
                claims[Claims.EmailVerified] = user.EmailConfirmed;
            }

            if (User.HasScope(Scopes.Phone))
            {
                claims[Claims.PhoneNumber] = user.PhoneNumber;
                claims[Claims.PhoneNumberVerified] = user.PhoneNumberConfirmed;
            }

            if (User.HasScope(Scopes.Roles))
            {
                claims[Claims.Role] = await _userManager.GetRolesAsync(user);
            }

            // Note: the complete list of standard claims supported by the OpenID Connect specification
            // can be found here: http://openid.net/specs/openid-connect-core-1_0.html#StandardClaims

            return Ok(claims);
        }
    }
}