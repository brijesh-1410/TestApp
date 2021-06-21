using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApp.API.Models;

namespace TestApp.API.Service
{
    public interface IUserService 
    {
        List<Models.Users> GetUsers();
        int? AddUpdateUser(Users user);

        bool ActiveInactiveUser(int userId,bool isActive);

        Users GetUserDetails(int userId);
    }
    public class UserService : IUserService
    {
        AppDBEntities _entities = new AppDBEntities();

        public List<Models.Users> GetUsers() {
            try
            {
                List<Models.Users> users = _entities.Users.ToList();
                return users;
            }
            catch(Exception) {
                return null;
            }
           
        }

        public int? AddUpdateUser(Users user) {
            try
            {
                int userId = 0;

                //check Name already used
                if (_entities.Users.Where(x=>x.UserName == user.UserName && x.UserId != user.UserId).Any()) {
                    userId = -1;
                    return userId;
                }

                //check EmailId already used
                if (_entities.Users.Where(x => x.EmailId == user.EmailId && x.UserId != user.UserId).Any())
                {
                    userId = -2;
                    return userId;
                }

                //check MobileNo already used
                if (_entities.Users.Where(x => x.MobileNo == user.MobileNo && x.UserId != user.UserId).Any())
                {
                    userId = -3;
                    return userId;
                }

                if (user.UserId == 0)
                {
                    user.EntryDate = DateTime.Now;
                    _entities.Users.Add(user);
                    userId = user.UserId;
                }
                else {
                    Users existingUser = _entities.Users.Where(x=>x.UserId == user.UserId).FirstOrDefault();
                    if (existingUser != null)
                    {
                        existingUser.FirstName = user.FirstName;
                        existingUser.LastName = user.LastName;
                        existingUser.EmailId = user.EmailId;
                        existingUser.MobileNo = user.MobileNo;
                        existingUser.CountryId = user.CountryId;
                        existingUser.StateId = user.StateId;
                        existingUser.CityId = user.CityId;
                        existingUser.IsActive = user.IsActive;
                        existingUser.Password = user.Password;
                    }
                    userId = existingUser.UserId;
                }
                _entities.SaveChanges();
                return userId;
            }
            catch (Exception) {
                return null;
            }
        }

        public bool ActiveInactiveUser(int userId,bool isActive)
        {
            try {
                Users user = _entities.Users.Where(x=>x.UserId == userId).FirstOrDefault();
                if (user != null)
                {
                    user.IsActive = isActive;
                    _entities.SaveChanges();
                    return true;
                }
                else {
                    return false;
                }
            }
            catch (Exception) {
                return false;
            }
        }

        public Users GetUserDetails(int userId) {
            try {
                return _entities.Users.Where(x => x.UserId == userId).FirstOrDefault();
            }
            catch (Exception) {
                return null;
            }
        }

    }  
}