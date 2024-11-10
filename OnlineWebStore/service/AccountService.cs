using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineWebStore.config;
using OnlineWebStore.Dto;
using OnlineWebStore.entity;
using System.Collections.Generic;

namespace OnlineWebStore.service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private StoreContext storeContext;

        private IMapper mapper;

        public AccountService(UserManager<ApplicationUser> _userManger,
            SignInManager<ApplicationUser> _signInManager,
            RoleManager<IdentityRole> _rolleMnager,
            StoreContext _context,
            IMapper _mapper)
        {
            userManager = _userManger;
            signInManager = _signInManager;
            roleManager = _rolleMnager;
            storeContext = _context;
            mapper = _mapper;
        }

        public async Task<IdentityResult> signUp(SignupDto signupDto)
        {
         var es =  roleManager.Roles.ToList();
            if(!es.Any(role=>role.Name==signupDto.RoleName))
            {
                throw new Exception($"Role {signupDto.RoleName} Not Exists.");

            }
            ApplicationUser user=null;
            if (signupDto.RoleName=="Employee")
            {
                var store = await storeContext.stores.FirstOrDefaultAsync(s => s.Id == signupDto.StoreId);
                if (store==null)
                {
                    throw new Exception($"Store {signupDto.StoreId} Not Available|Not Exists.");
                }

                 user = new ApplicationUser
                {
                    UserName = signupDto.Username,
                    Email = signupDto.Email,
                    DOB = signupDto.DOB,
                    Name = signupDto.Name,
                    PhoneNumber = signupDto.PhoneNumber,
                    StoreId = signupDto.StoreId
                };
            }
            else if (signupDto.RoleName == "Manager")
            {
                 user = new ApplicationUser
                {
                    UserName = signupDto.Username,
                    Email = signupDto.Email,
                    DOB = signupDto.DOB,
                    Name = signupDto.Name,
                    PhoneNumber = signupDto.PhoneNumber,
                };
            }

   
            var result = await userManager.CreateAsync(user, signupDto.Password);
            if (result.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(user, signupDto.RoleName);
                if (!roleResult.Succeeded)
                {
                    throw new Exception($"Failed to add user to role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                }
                return result; 
            }
            throw new Exception($"Signup error: {string.Join(", ", result.Errors.Select(e => e.Description))}"); 

        }

        public async Task<SignInResult> login(SigninDto signinDto)
        {
           return  await signInManager.PasswordSignInAsync(signinDto.Username,signinDto.Password,false,false);
        }

       public async Task<IList<string>> getUserRoles(string username)
        {
            ApplicationUser user = await userManager.FindByNameAsync(username);
            return await userManager.GetRolesAsync(user);
        }

        public async Task<List<UserDto>> getUsers()
        {

            List<ApplicationUser> users = await storeContext.Users
           .Include(u => u.Store)                         
               .ThenInclude(store => store.Products)      
           .Include(u => u.Store)                         
               .ThenInclude(store => store.Orders)        
           .ToListAsync();
            return mapper.Map < List <UserDto>> (users);

        }

        public async Task<UserDto> getUser(string username)
        {
            ApplicationUser user = await storeContext.Users.Include("Store").FirstAsync(u=>u.UserName==username);
            if(user == null)
            {
                throw new Exception($"User {username} Not Exists.");
            }
            return mapper.Map<UserDto>(user);
        }

    }
}
