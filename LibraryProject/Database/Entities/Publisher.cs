using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryProject.API.Database.Entities
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(62)")]
        public string Name { get; set; }
    }
}
