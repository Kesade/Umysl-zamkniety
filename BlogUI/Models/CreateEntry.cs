using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.DomainEntities;
using Common.UI;

namespace BlogUI.Models
{
    public class CreateEntry : ICreateEntry
    {
        [AllowHtml]
        [Required]
        public string Body { get; set; }
        [Required]
        public string Title { get; set; }
        public int ParrentId { get; set; }
        public IDiaryDomainEntity Diary { get; set; }
        public IUserDomainEntity Author { get; set; }
    }
}