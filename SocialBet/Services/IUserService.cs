﻿using System;
using System.Collections.Generic;
using System.Linq;
using SocialBet.Models;
using SocialBet.Helpers;

namespace SocialBet.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(string id);
        UserStat GetStats(string id);
        Boolean IsAvailable(string username);
        User Create(User user, string password);
        User Update(User user, string password = null);
        void Delete(string id);
        UserStat UpdateStats(string id, StatFunction func, int xp = 0);
    }

    public class UserService : IUserService
    {
        private DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            User user = new User();
            try
            {
                user = _context.Users.SingleOrDefault(x => x.Username == username);
            }
            catch(Exception ex)
            {
                throw new AppException(ex.Message);
            }

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(string id)
        {
            return _context.Users.Find(id);
        }

        public UserStat GetStats(string id)
        {
            return _context.UserStats.Find(id);
        }

        public Boolean IsAvailable(string username)
        {
            return !_context.Users.Any(x => x.Username == username);
        }

        public User Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (user.Id == user.Username && _context.Users.Any(x => x.Id == user.Id))
            {
                var _user = _context.Users.Find(user.Id);
                if (string.IsNullOrWhiteSpace(_user.FirstName) || string.IsNullOrWhiteSpace(_user.LastName))
                {
                    _user.FirstName = user.FirstName;
                    _user.LastName = user.LastName;
                    Update(_user);
                }
                throw new AppException("User with id \"" + user.Id + "\" is already registered. Authentication needed.");
            }

            if (_context.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username \"" + user.Username + "\" is already taken");
            try
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                _context.Users.Add(user);
                _context.SaveChanges();

                _context.UserStats.Add(new UserStat(user.Id));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }

            return user;
        }

        public User Update(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new AppException("Username " + userParam.Username + " is already taken");
            }

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                return user;
            }
            catch(Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public void Delete(string id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                try
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw new AppException(ex.Message);
                }
            }
        }

        public UserStat UpdateStats(string id, StatFunction func, int xp = 0)
        {
            var stat = _context.UserStats.Find(id);

            if (func == StatFunction.Cancellation)
            {
                stat.NumOfCancelled++;
            }
            else if (func == StatFunction.Creation)
            {
                stat.NumOfBets++;
            }
            else if (func == StatFunction.Referee)
            {
                stat.NumOfReferreed++;
            }
            else if (func == StatFunction.Win)
            {
                stat.NumOfWins++;
            }
            else if (func == StatFunction.Loss)
            {
                stat.NumOfLosses++;
            }
            else if (func == StatFunction.Draw)
            {
                stat.NumOfDraws++;
            }
            else if (func == StatFunction.XP)
            {
                // xp ???
            }

            _context.UserStats.Update(stat);
            _context.SaveChanges();

            return stat;
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }



        private int getXPForLevel(int lvl)
        {
            int baseXP = 100;
            int ret = 0;

            for (int i = 1; i < lvl; i++)
            {
                ret += baseXP;
                baseXP += 50;
            }

            return ret;
        }
    }
}
