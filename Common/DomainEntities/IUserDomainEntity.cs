using System;
using System.Collections.Generic;

namespace Common.DomainEntities
{
    public interface IUserDomainEntity : IFormattable, IDomainEntity
    {
        string Name { get; set; }
        string Roles { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        string GetRoleColor();
    }
}