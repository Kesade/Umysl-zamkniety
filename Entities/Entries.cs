using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.StorageEntities;

namespace Entities
{
    [Serializable]
    public sealed class Entries : IEntryEntity
    {
        public Users Author { get; set; }
        public Diaries Diary { get; set; }
        public ICollection<Comments> Comments { get; set; }


        [Column(TypeName = "DateTime2")]
        public DateTime Timestamp { get; set; }

        public int DiaryId { get; set; }
        public int AuthorId { get; set; }

        public string Body { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}