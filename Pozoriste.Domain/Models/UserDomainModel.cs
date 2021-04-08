using Pozoriste.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Domain.Models
{
    public class UserDomainModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public UserRole UserRole { get; set; }
    }
}
