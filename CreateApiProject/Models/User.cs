using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateApiProject.Models
{
    public class User : Token
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;    
        public string Password { get; set; } = string.Empty;
        public int IdRole { get; set; }

    }
}
