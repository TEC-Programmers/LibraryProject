using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryProject.Database.Entities
{
    public class Book
    {

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public decimal Language { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string PublishYear { get; set; }

        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int AuthorId { get; set; }

        [ForeignKey("AuhtorId")]
        public Author Author { get; set; }

        public int PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public Publisher  Publisher{ get; set; }


    }
}
