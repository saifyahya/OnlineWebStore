using Microsoft.AspNetCore.Identity;
using OnlineWebStore.Dto;
using OnlineWebStore.entity;

namespace OnlineWebStore.service
{
    public interface IAccountService
    {
        Task<IdentityResult> signUp(SignupDto signupDto);
        Task<SignInResult> login(SigninDto signinDto);
        Task<IList<string>> getUserRoles(string username);
        Task<List<UserDto>> getUsers();
        Task<UserDto> getUser(string username);


    }
}