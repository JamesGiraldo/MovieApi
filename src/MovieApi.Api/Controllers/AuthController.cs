using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Application.Features.Auth.Commands.LoginUser;
using MovieApi.Application.Features.Auth.Commands.RegisterUser;

namespace MovieApi.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }
}