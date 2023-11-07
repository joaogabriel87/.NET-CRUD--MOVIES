using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Models
{
    public class MovieModels
    {
    public int Id { get; set; }

    [StringLength(60, MinimumLength = 3)]
    public string? Title { get; set; }

    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    [Range(1, 100)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }    

    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    [StringLength(30)]
    public string? Genre { get; set; }
    
    [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
    [StringLength(20)]
    public string? Rating { get; set; }
    }
}