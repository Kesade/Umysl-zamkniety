using System;
using Common.DomainEntities;
using Common.Helpers;
using Newtonsoft.Json;

namespace DomainModel
{
    public class Comment : ICommentDomainEntity
    {
        public int ParrentId => Entry.Id;
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Body { get; set; }

        [JsonConverter(typeof(ConcreteConverter<User>))]
        public IUserDomainEntity Author { get; set; }

        [JsonConverter(typeof(ConcreteConverter<Entry>))]
        public IEntryDomainEntity Entry { get; set; }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }
    }
}