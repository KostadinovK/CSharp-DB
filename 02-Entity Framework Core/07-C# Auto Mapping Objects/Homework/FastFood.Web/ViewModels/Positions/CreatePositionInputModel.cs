using System.ComponentModel.DataAnnotations;

namespace FastFood.Web.ViewModels.Positions
{
    public class CreatePositionInputModel
    {
        [Required]
        public string PositionName { get; set; }
    }
}
