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
    }
}