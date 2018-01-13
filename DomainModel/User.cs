using System;
using System.Collections.Generic;
using System.Linq;
using Common.DomainEntities;

namespace DomainModel
{
    public class User : IUserDomainEntity
    {
        readonly Dictionary<string, string> _colours = new Dictionary<string, string>
        {
            {"Administrator", "goldenrod"},
            {"Moderator", "TBD"},
            {"User", "#93c763"},
            {"Guest", "initial"}
        };
       
        public int ParrentId => Id;
        public string Name { get; set; }
        public string Roles { get; set; }
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string GetRoleColor()
        {
            var initColor = "initial";
            var firstRole = Roles.Split(',').Select(x => x.Trim()).FirstOrDefault(role => _colours.TryGetValue(role, out initColor));
          
            return initColor;
        }


        public string ToString(string format, IFormatProvider formatProvider)
        {
            return Name;
        }
    }
}