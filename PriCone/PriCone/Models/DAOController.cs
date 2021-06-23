﻿using PriCone.Models.dataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PriCone.Models
{
    public class DAOController
    {
        private DAO dao = null;
        public DAOController()
        {
            dao = new DAO();
        }
        public List<Characters> getAllChar()
        {
            return dao.Characters.ToList();
        }
        public List<Guild> getAllGuild()
        {
            return dao.Guild.ToList();
        }

        public Characters detailChar(String Id)
        {
            var result = dao.Characters.FirstOrDefault(p => p.CharId.Equals(Id));
            if(result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public List<Guild> getListGuild()
        {
            return dao.Guild.ToList();
        }

        public bool saveChar(Characters characters)
        {
            try
            {
                dao.Characters.Add(characters);
                dao.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }

        public Characters getChar(string Id)
        {
            try {
                var result = dao.Characters.FirstOrDefault(p => p.CharId.Equals(Id));
                if(result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                e.Message.ToString();
                return null;
            }
        }

        public bool updateChar(Characters characters)
        {
            bool flag = false;
            try
            {
                var result = dao.Characters.FirstOrDefault(p => p.CharId.Equals(characters.CharId));
                if (result != null)
                {
                    result.CharName = characters.CharName;
                    result.Height = characters.Height;
                    result.Weight = characters.Weight;
                    result.Birthday = characters.Birthday;
                    result.BloodType = characters.BloodType;
                    result.Race = characters.Race;
                    result.Hobbies = characters.Hobbies;
                    result.VA = characters.VA;
                    result.Description = characters.Description;
                    result.Detail = characters.Detail;
                    result.Icon = characters.Icon;
                    result.GuildId = characters.GuildId;

                    dao.SaveChanges();
                    flag = true;
                }
            }
            catch(Exception e)
            {
                e.Message.ToString();
                flag = false;         
            }
            return flag;
        }

        public bool deleteChar(string Id)
        {
            bool flag = false;
            try
            {
                var result = dao.Characters.FirstOrDefault(p => p.CharId.Equals(Id));
                if (result != null)
                {
                    dao.Characters.Remove(result);
                    dao.SaveChanges();
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }catch(Exception e)
            {
                e.Message.ToString();
                flag = false;
            }
            return flag;
        }

        public bool checkLogin(string user, string pass)
        {
            try
            {
                var result = dao.User.FirstOrDefault(p => p.Username.Equals(user) && p.Password.Equals(pass));
                if(result != null)
                {
                    if(result.Status == false)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }catch(Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }

        public bool registerUser(User user)
        {
            try
            {
                dao.User.Add(user);
                dao.SaveChanges();
                return true;
            }catch(Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }
    }
}