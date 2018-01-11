using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.StorageEntities;

namespace Entities
{
    [Serializable]
    public class Comments : ICommentEntity
    {
        public virtual Users Author { get; set; }

        public virtual Entries Entry { get; set; }

        public int AuthorId { get; set; }
        public int EntryId { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime Timestamp { get; set; }

        public string Body { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}