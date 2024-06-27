using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Interfaces;

public interface IUserService
{
    Task<TokensDto> Login(LoginRequestDto loginRequestDto);
    Task<TokensDto> Refresh(RefreshTokenDto refreshTokenDto);
}