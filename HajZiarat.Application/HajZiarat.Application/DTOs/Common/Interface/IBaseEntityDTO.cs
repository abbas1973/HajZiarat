using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public interface IBaseEntityDTO
    {
        [Display(Name = "شناسه")]
        long Id { get; set; }
    }
}
