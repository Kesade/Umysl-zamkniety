using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Common.DomainEntities;
using Common.StorageEntities;

namespace Entities
{
    [Serializable]
    public class Diaries : IDiaryEntity
    {
        public virtual Users Author { get; set; }

        public ICollection<Entries> Entries { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public int AuthorId { get; set; }

        public string Title { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime Timestamp { get; set; }

        public Task<IDiaryDomainEntity> ConvertToDomain(IDiaryEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}