using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.StorageEntities;

namespace Entities
{
    [Serializable]
    public sealed class Users : IUserEntity
    {
        private ICollection<Diaries> Diaries { get; set; }
        private ICollection<Comments> Comments { get; set; }
        private ICollection<Entries> Entries { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Roles { get; set; }

        [Index("IX_Login", 1, IsUnique = true)]
        [MaxLength(30)]
        public string Login { get; set; }

        public string Password { get; set; }
    }
}