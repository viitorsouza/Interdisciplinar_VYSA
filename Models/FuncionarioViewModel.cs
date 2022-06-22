using System.ComponentModel.DataAnnotations;

namespace VYSA.Models
{
    public class FuncionarioViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Senha { get; set; }
    }
}