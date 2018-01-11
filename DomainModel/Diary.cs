using System;
using System.Collections.Generic;
using Common.DomainEntities;
using Common.Helpers;
using Newtonsoft.Json;

namespace DomainModel
{
    public class Diary : IDiaryDomainEntity
    {
        public int ParrentId => Author.Id;
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Timestamp { get; set; }

        [JsonConverter(typeof(ConcreteConverter<ICollection<Entry>>))]
        public ICollection<IEntryDomainEntity> Entries { get; set; }

        [JsonConverter(typeof(ConcreteConverter<User>))]
        public IUserDomainEntity Author { get; set; }
    }
}