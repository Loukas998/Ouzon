using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Template.Domain.Entities;
using Template.Domain.Entities.AuthEntities;
using Template.Domain.Entities.Notifications;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class AccountRepository(UserManager<User> userManager,
        TemplateDbContext dbcontext,
        ITokenRepository tokenRepository,
        IHostEnvironment hostEnvironment,
        IDeviceRepository deviceRepository
        ) : IAccountRepository
{
    public async Task<User> GetUserAsync(string id, bool isAssistant)
    {
        if (isAssistant)
            return await dbcontext.Users
                .Include(u => u.RatingsReceived)
                .FirstOrDefaultAsync(u => u.Id.Equals(id));

        return await dbcontext.Users.Include(u => u.Clinic).FirstOrDefaultAsync(u => u.Id.Equals(id));
    }
    public async Task<User> FindUserByEmail(string email)
    {
        return await userManager.FindByEmailAsync(email);
    }
    public async Task<User> FindUserByUserName(string userName)
    {
        return await userManager.FindByNameAsync(userName);
    }
    public async Task<User> GetUserWithDevicesAsync(string id)
    {
        return await dbcontext.Users.Include(u => u.Devices).FirstOrDefaultAsync(u => u.Id.Equals(id));
    }

    //public async Task<bool> FillWallet(string email, int amount)
    //{
    //    var user = await userManager.FindByEmailAsync(email);
    //    if (user == null)
    //    {
    //        return false;
    //    }
    //    user.Wallet += amount;
    //    await userManager.UpdateAsync(user);
    //    await dbcontext.SaveChangesAsync();
    //    return true;
    //}

    public async Task<IEnumerable<IdentityError>> Register(User owner, string password, string role)
    {
        var user = await userManager.FindByEmailAsync(owner.Email);
        if (user != null)
        {
            var list = new List<IdentityError>
            {
                new()
                {
                    Code = "User already exists",
                    Description = "User with the same email already exists"
                }
            };
        }
        var res = await userManager.CreateAsync(owner, password);
        if (res.Succeeded)
        {
            await userManager.AddToRoleAsync(owner, role);
        }

        return res.Errors;
    }

    public async Task<IEnumerable<IdentityError>> RegisterAdmin(User user, string password)
    {
        var check = await userManager.CreateAsync(user, password);
        if (check.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Administrator");
        }
        return check.Errors;
    }

    public async Task<IEnumerable<IdentityError>> RegisterUser(User user, string password, string verifyUrl)
    {
        if (await userManager.FindByEmailAsync(user.Email) != null)
        {
            var list = new List<IdentityError>
            {
                new()
                {
                    Code = "User already exists",
                    Description = "User with the same email already exists"
                }
            };
            return list;
            //throw new UserAlreadyExistsException(user.Email);
        }

        var check = await userManager.CreateAsync(user, password);

        if (check.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");
            await dbcontext.SaveChangesAsync();
        }
        return check.Errors;
    }
    public async Task<int> NumberOfUsersInRole(string roleId)
    {
        var num = await dbcontext.UserRoles.Where(ur => ur.RoleId == roleId).CountAsync();
        return num;
    }

    public async Task<int> NewUsersAfterMonth(int month, string roleId, int year)
    {
        var records = from ur in dbcontext.UserRoles
                      join u in dbcontext.Users
                      on ur.UserId equals u.Id
                      where u.CreatedAt.Month == month
                      where u.CreatedAt.Year == year
                      where ur.RoleId == roleId
                      select ur;

        var num = await records.CountAsync();
        return num;
    }
    public async Task<bool> CheckPassword(string userId, string password)
    {
        var user = await GetUserAsync(userId, false);
        if (user == null || password == null)
        {
            return false;
        }
        var sucess = await userManager.CheckPasswordAsync(user, password);
        return sucess;
    }

    public async Task UpdateUser(User user)
    {
        await userManager.UpdateAsync(user);
        await dbcontext.SaveChangesAsync();
    }

    public async Task DeleteAccount(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), userId);
        }

        await userManager.DeleteAsync(user);
        await dbcontext.SaveChangesAsync();
    }
    //private async Task SendEmailForVerification(string userEmail, string code, string verifyUrl)
    //{
    //    var emailMessage = new MimeMessage();
    //    emailMessage.From.Add(MailboxAddress.Parse("eldon.reilly25@ethereal.email"));
    //    emailMessage.To.Add(MailboxAddress.Parse(userEmail));
    //    emailMessage.Subject = "Code for Verification";
    //    string fullUrl = verifyUrl + code;
    //    string account = "Account";
    //    string verify = "Verify";
    //    emailMessage.Body = new TextPart(TextFormat.Html)
    //    {
    //        Text = $"To Verify Your Account Press this Link <a href ={fullUrl}> Click here </a>"
    //        //"To Verify Your Account Press this Link <a href=\"fullUrl\"> Click here </a>"
    //        //To Verify Your Account Press this Link < a href = { fullUrl }> Click here </ a >
    //    };
    //    using var smtp = new SmtpClient();
    //    await smtp.ConnectAsync("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
    //    smtp.Authenticate("eldon.reilly25@ethereal.email", "1xyrdZx7msYpj4KPgJ");
    //    smtp.Send(emailMessage);
    //    await smtp.DisconnectAsync(true);
    //    return;
    //}

    public async Task<string> SaveUserProfileAsync(IFormFile userImage)
    {
        if (userImage == null)
            return null;

        var contentPath = hostEnvironment.ContentRootPath;
        var specialPath = "Images/Users";
        var path = Path.Combine(contentPath, specialPath);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        var extension = Path.GetExtension(userImage.FileName);
        var imageName = $"{Guid.NewGuid().ToString()}{extension}";
        var fullName = Path.Combine(path, imageName);
        var returnName = Path.Combine(specialPath, imageName);
        using var stream = new FileStream(fullName, FileMode.Create);
        await userImage.CopyToAsync(stream);
        return returnName;
    }
    public bool DeleteImage(string imageName)
    {
        if (imageName == null)
        {
            return false;
        }
        if (!File.Exists(imageName))
        {
            return false;
        }
        File.Delete(imageName);
        return true;
    }
    public async Task<bool> UpdateUserImage(string userId, IFormFile newImage)
    {
        if (userId == null || newImage == null)
        {
            return false;
        }
        var user = await userManager.FindByIdAsync(userId);
        if (user.ProfileImagePath != null)
        {
            var success = DeleteImage(user.ProfileImagePath);
        }
        var newImagePath = await SaveUserProfileAsync(newImage);
        user.ProfileImagePath = newImagePath;
        await userManager.UpdateAsync(user);
        await dbcontext.SaveChangesAsync();
        return true;
    }
    public async Task<AuthResponse>? LoginUser(string email, string password, string deviceToken)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return null;
        }

        bool isValidCredentials = await userManager.CheckPasswordAsync(user, password);
        if (isValidCredentials)
        {
            var existingDevice = await deviceRepository.GetDeviceByToken(deviceToken, user.Id);
            if (existingDevice != null)
            {
                existingDevice.LastLoggedInAt = DateTime.UtcNow;
                await deviceRepository.SaveChangesAsync();
            }
            else
            {

                var device = new Device()
                {
                    LastLoggedInAt = DateTime.UtcNow,
                    DeviceToken = deviceToken,
                    UserId = user.Id,
                    OptIn = true
                };
                await deviceRepository.AddAsync(device);
            }
            var token = await tokenRepository.GenerateToken(user.Id);
            return token;
        }
        return null;
    }
    public async Task<bool> UserInRoleAsync(string id, string roleName)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user is null)
        {
            return false;
        }
        var success = await userManager.IsInRoleAsync(user, roleName);
        if (!success)
        {
            return false;
        }
        return true;
    }
    public async Task<User> GetUserDetails(string? id)
    {
        var user = userManager.Users
            .Include(u => u.InProcedure)
            .ThenInclude(prc => prc.Procedure)
            .Include(u => u.ProcedureFrom)
            .Include(u => u.Devices)
            .Include(u => u.Holidays)
            .Include(u => u.RatingsReceived)
            .AsSplitQuery()
            .AsQueryable();

        return await user.FirstOrDefaultAsync(u => u.Id.Equals(id));

    }
    public async Task<List<(User user, string? roleName)>> GetUsersWithFilters(string? role, string? email, string? phoneNumber, string? clinicAddress, string? clinicName)
    {
        var query = dbcontext.Users
            .Include(u => u.Clinic)
            .AsQueryable();

        if (!string.IsNullOrEmpty(email))
            query = query.Where(u => u.Email!.Contains(email));

        if (!string.IsNullOrEmpty(phoneNumber))
            query = query.Where(u => u.PhoneNumber!.Contains(phoneNumber));

        if (!string.IsNullOrEmpty(clinicAddress))
            query = query.Where(u => u.Clinic!.Address!.Contains(clinicAddress));
        if (!string.IsNullOrEmpty(clinicName))
            query = query.Where(u => u.Clinic!.Name.Contains(clinicName));


        var result = await query
    .Join(dbcontext.UserRoles,
          u => u.Id,
          ur => ur.UserId,
          (u, ur) => new { u, ur })
    .Join(dbcontext.Roles,
          temp => temp.ur.RoleId,
          r => r.Id,
          (temp, r) => new
          {
              user = temp.u,
              roleName = r.Name
          })
    .Where(x => string.IsNullOrEmpty(role) || x.roleName.Contains(role))

    .ToListAsync();

        return result.Select(x => (x.user, x.roleName)).ToList();
    }

    public Task<List<User>> GetAssistants(string? sortByRating)
    {
        throw new NotImplementedException();
    }
    public async Task<string> GetRoleOfUser(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return "";
        }
        var role = await userManager.GetRolesAsync(user);
        return role.First();
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        await userManager.UpdateAsync(user);
        return user;
    }

    public async Task<bool> UpdatePassword(User user, string oldPassword, string newPassword)
    {
        var result = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        if (result.Succeeded)
        {
            return result.Succeeded;
        }
        return false;
    }
}
//public async Task<bool> Verify(string verficationToken)
//{
//    var user = await dbcontext.Users.FirstOrDefaultAsync(u => u.VerificationToken == verficationToken);
//    if (user != null)
//    {
//        user.VerifiedAt = DateTime.Now;
//        user.EmailConfirmed = true;
//        await userManager.UpdateAsync(user);
//        await dbcontext.SaveChangesAsync();
//        return true;
//    }
//    return false;
//}
//public async Task DeleteAccount(string userId)
//{
//    var user = await userManager.FindByIdAsync(userId);
//    if (user == null)
//    {
//        return;
//    }
//    if (user.ProfileImagePath != null)
//    {
//        var path = Path.Combine(hostEnvironment.ContentRootPath, user.ProfileImagePath);
//        File.Delete(path);

//    }

//    await userManager.DeleteAsync(user);
//    await dbcontext.SaveChangesAsync();
//}
/*
public async Task<IEnumerable<IdentityError>> Verify(string email, string verficationToken)
{
var user = await userManager.FindByEmailAsync(email);
var check = await userManager.ConfirmEmailAsync(user, verficationToken);
if (check.Succeeded)
{
user.VerifiedAt = DateTime.Now;
user.EmailConfirmed = true;
await dbcontext.SaveChangesAsync();
}
return check.Errors;
}
*/


