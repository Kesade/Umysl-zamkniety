using System;
using System.Collections.Generic;
using Common.DomainEntities;
using Common.Enums;
using Common.Helpers;
using Newtonsoft.Json;

namespace DomainModel
{
    public class Entry : IEntryDomainEntity
    {
        public int ParrentId => Diary.Id;
        public DateTime Timestamp { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public EntryType Type { get; set; }

        [JsonConverter(typeof(ConcreteConverter<Diary>))]
        public IDiaryDomainEntity Diary { get; set; }

        [JsonConverter(typeof(ConcreteConverter<ICollection<Comment>>))]
        public ICollection<ICommentDomainEntity> Comments { get; set; }

        [JsonConverter(typeof(ConcreteConverter<User>))]
        public IUserDomainEntity Author { get; set; }

        public int Id { get; set; }
    }
}