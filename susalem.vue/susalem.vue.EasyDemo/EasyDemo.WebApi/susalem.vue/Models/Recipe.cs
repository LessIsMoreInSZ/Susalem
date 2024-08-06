using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace susalem.vue.Models
{
    [Table("Recipe")]
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("RecipeName")]
        public string RecipeName { get; set; }

        [Column("Level")]
        public int Level { get; set; }

        [Column("State")]
        public int State { get; set; }
    }
}
