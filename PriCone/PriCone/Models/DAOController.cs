using PriCone.Models.dataModels;
using PriCone.Models.viewModels;
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

        public List<Characters> getCharTop()
        {
            try
            {
                return dao.Characters.OrderByDescending(p => p.Likes).Take(3).ToList();
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }

        public Characters detailChar(String Id)
        {
            var result = dao.Characters.FirstOrDefault(p => p.CharId.Equals(Id));
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public bool checkLike(string UserId, string CharId)
        {
            var like = dao.Liking.FirstOrDefault(p => p.UserId.Equals(UserId) && p.CharId.Equals(CharId));
            if(like != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkSave(string UserId, string CharId)
        {
            var save = dao.Saved.FirstOrDefault(p => p.UserId.Equals(UserId) && p.CharId.Equals(CharId));
            if (save != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool updateLike(Liking liking)
        {
            try
            {
                var like = dao.Liking.FirstOrDefault(p=>p.CharId.Equals(liking.CharId) && p.UserId.Equals(liking.UserId));
                if(like != null)
                {
                    updateLikeChar("remove", liking.CharId);
                    dao.Liking.Remove(like);
                    dao.SaveChanges();
                    return true;
                }
                else
                {
                    updateLikeChar("add", liking.CharId);
                    dao.Liking.Add(liking);
                    dao.SaveChanges();
                    return true;
                }
            }catch(Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool updateSave(Saved save)
        {
            try
            {
                var saved = dao.Saved.FirstOrDefault(p => p.CharId.Equals(save.CharId) && p.UserId.Equals(save.UserId));
                if (saved != null)
                {
                    dao.Saved.Remove(saved);
                    dao.SaveChanges();
                    return true;
                }
                else
                {
                    dao.Saved.Add(save);
                    dao.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool updateLikeChar(string check, string charId)
        {
            var flag = false;
            try
            {
                var chars = dao.Characters.FirstOrDefault(p => p.CharId.Equals(charId));
                if (chars != null)
                {
                    if (check == "add")
                    {
                        chars.Likes += 1;
                    }
                    else
                    {
                        chars.Likes -= 1;
                    }
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                flag = false;
            }
            return flag;
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
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
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
            catch (Exception e)
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
                    var res = dao.Feedback.Where(p => p.CharId.Equals(Id));
                    dao.Feedback.RemoveRange(res);
                    var card = dao.Card.Where(p => p.CharId.Equals(Id));
                    dao.Card.RemoveRange(card);
                    var skill = dao.Skill.Where(p => p.CharId.Equals(Id));
                    dao.Skill.RemoveRange(skill);
                    var likes = dao.Liking.Where(p => p.CharId.Equals(Id));
                    dao.Liking.RemoveRange(likes);
                    var saves = dao.Saved.Where(p => p.CharId.Equals(Id));
                    dao.Saved.RemoveRange(saves);
                    dao.Characters.Remove(result);
                    dao.SaveChanges();
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            } catch (Exception e)
            {
                e.Message.ToString();
                flag = false;
            }
            return flag;
        }

        public User checkLogin(string user, string pass)
        {
            try
            {
                var result = dao.User.FirstOrDefault(p => p.Username.Equals(user) && p.Password.Equals(pass));
                if (result != null)
                {
                    if (result.Status == false)
                    {
                        return null;
                    }
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    return null;
                }
            } catch (Exception e)
            {
                e.Message.ToString();
                return null;
            }
        }

        public bool registerUser(User user)
        {
            try
            {
                dao.User.Add(user);
                dao.SaveChanges();
                return true;
            } catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }

        public List<Feedback> comment(string Id)
        {
            try {
                return dao.Feedback.Where(p => p.CharId.Equals(Id)).ToList();
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return null;
            }
        }

        public bool addComment(Feedback feedback)
        {
            try
            {
                dao.Feedback.Add(feedback);
                dao.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }

        public bool deleteComent(string FeedId)
        {
            try
            {
                var result = dao.Feedback.FirstOrDefault(p => p.FeedId.Equals(FeedId));
                if (result != null)
                {
                    dao.Feedback.Remove((Feedback)result);
                    dao.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            } catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }
        public List<Card> getCard(string Id) 
        {
            try
            {
                return dao.Card.Where(p => p.CharId.Equals(Id)).ToList();
            }catch(Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }

        public List<Card> getListCard(string Id)
        {
            try
            {
                return dao.Card.Where(p => p.Card1.Contains("CA"+Id)).ToList();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }

        public bool addCardDetail(Card card)
        {
            var flag = false;
            try
            {
                if(card != null)
                {
                    dao.Card.Add(card);
                    dao.SaveChanges();
                    flag =  true;
                }
            }catch(Exception ex)
            {
                ex.Message.ToString();
                flag = false;
            }
            return flag;
        }

        public Card getCardDetail(string flag, string id)
        {
            try 
            {
                return dao.Card.FirstOrDefault(p => p.Card1.Contains(flag + id));
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }

        public List<Card> getAllCard(string flag, string id)
        {
            try 
            {
                return dao.Card.Where(p => p.Card1.Contains(flag + id)).ToList();
            }catch(Exception ex)
            {
                return null;
                ex.Message.ToString();
            }
        }

        public bool deleteCard(string IdCard)
        {
            var flag = false;
            try
            {
                var card = dao.Card.FirstOrDefault(p => p.Card1.Equals(IdCard));
                if(card != null)
                {
                    dao.Card.Remove(card);
                    dao.SaveChanges();
                    flag = true;
                }
            }
            catch(Exception ex)
            {
                flag = false;
                ex.Message.ToString();
            }
            return flag;
        }

        public Skill getSkill(string Id)
        {
            var Skill = dao.Skill.FirstOrDefault(p => p.CharId.Equals(Id));
            try {
                if(Skill != null)
                {
                    return Skill;
                }
                else
                {
                    return Skill;
                }
            }catch(Exception ex)
            {
                ex.Message.ToString();
                return Skill;
            }

        }

        public bool addSkill(Skill skill)
        {
            try
            {
                dao.Skill.Add(skill);
                dao.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool updateSkill(Skill skill)
        {
            bool flag = false;
            try 
            {
                var ski = dao.Skill.FirstOrDefault(p => p.SkillId.Equals(skill.SkillId));
                if(ski != null)
                {
                    ski.EnSkill = skill.EnSkill;
                    ski.EnUB = skill.EnUB;
                    ski.ExSkill = skill.ExSkill;
                    ski.Skill1 = skill.Skill1;
                    ski.Skill2 = skill.Skill2;
                    ski.UB = skill.UB;

                    dao.SaveChanges();
                    flag = true;
                }
            }catch(Exception ex)
            {
                ex.Message.ToString();
                flag = false;
            }
            return flag;
        }

        public List<Guild> guildListPagedList()
        {
            return dao.Guild.OrderBy(p=>p.GuildId).ToList();
        }

        public bool updateGuild(Guild guild)
        {
            var flag = false;
            try
            {
                var guildId = dao.Guild.FirstOrDefault(p=>p.GuildId.Equals(guild.GuildId));
                if(guildId != null)
                {
                    guildId.GuildName = guild.GuildName;
                    dao.SaveChanges();
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }catch(Exception ex)
            {
                ex.Message.ToString();
                flag = false;
            }
            return flag;
        }

        public User getUser(string Id)
        {
            try
            {
                return dao.User.FirstOrDefault(p => p.UserId.Equals(Id));
            }catch(Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }

        public bool updateUser(User user)
        {
            try
            {
                var us = dao.User.FirstOrDefault(p => p.UserId.Equals(user.UserId));
                if(us != null)
                {
                    us.Username = user.Username;
                    us.Password = user.Password;
                    us.FullName = user.FullName;
                    us.Birthday = user.Birthday;
                    us.Address = user.Address;
                    us.Phone = user.Phone;
                    us.Email = user.Email;

                    dao.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }catch(Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public List<Liking> userLikeList(string Id)
        {
            try
            {
                return dao.Liking.OrderByDescending(p => p.LikeId).Where(p=>p.UserId.Equals(Id)).Take(3).ToList();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }

        public List<Saved> userSavedList(string Id)
        {
            try
            {
                return dao.Saved.Where(p=>p.UserId.Equals(Id)).ToList();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }

        public List<Characters> gacha(int number)
        {
            try
            {
                return dao.Characters.OrderBy(p => p.CharId).Take(number).ToList();
            }catch(Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }

        public List<User> getAllUser()
        {
            try
            {
                return dao.User.ToList();
            }catch(Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }

        public bool updateUser(string Id, string flag)
        {
            bool check = false;
            try
            {
                var user = dao.User.FirstOrDefault(p => p.UserId.Equals(Id));
                if (user != null)
                {
                    if (flag.Equals("Admin"))
                    {
                        if (user.Role == true)
                        {
                            user.Role = false;
                            dao.SaveChanges();
                            check = true;
                        }
                        else
                        {
                            user.Role = true;
                            dao.SaveChanges();
                            check = true;
                        }
                    }
                    else if (flag.Equals("Block"))
                    {
                        if(user.Status == true)
                        {
                            user.Status = false;
                            dao.SaveChanges();
                            check = true;
                        }
                        else
                        {
                            user.Status = true;
                            dao.SaveChanges();
                            check = true;
                        }
                    }
                    else if(flag.Equals("Delete"))
                    {
                        dao.User.Remove(user);
                        dao.SaveChanges();
                        check = true;
                    }
                    else
                    {
                        check = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                check = false;
            }

            return check;
        }

        public bool addGuild(Guild guild)
        {
            try
            {
                dao.Guild.Add(guild);
                dao.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }
    }
}