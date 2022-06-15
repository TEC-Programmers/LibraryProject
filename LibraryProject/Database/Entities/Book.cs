using LibraryProject.API.Database.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryProject.Database.Entities
{
    //making Book Entity for the database 
    public class Book
    {

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Language { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Image { get; set; }

        [Column(TypeName = "smallint")]
        public int PublishYear { get; set; }

        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

        public int PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
    }
}
