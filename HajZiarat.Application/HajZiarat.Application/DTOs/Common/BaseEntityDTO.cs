using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class BaseEntityDTO : IBaseEntityDTO
    {
        [Display(Name = "شناسه")]
        public long Id { get; set; }
    }
}
