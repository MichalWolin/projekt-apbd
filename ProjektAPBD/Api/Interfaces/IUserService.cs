using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Interfaces;

public interface IUserService
{
    Task<TokensDto> Login(LoginRequestDto loginRequestDto, CancellationToken cancellationToken);
    Task<TokensDto> Refresh(RefreshTokenDto refreshTokenDto, CancellationToken cancellationToken);
}