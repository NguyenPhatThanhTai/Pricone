using PriCone.Models.dataModels;
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
            if (result != null)
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
                    //Xóa skill ở đây
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
    }
}