using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class UpdateStockRequest
    {
        [Required]
        [MinLength(5,ErrorMessage ="Symbol must be 5 characters")]
        [MaxLength(255,ErrorMessage ="Symbol cannot be over 255 characters")]
        public string Symbol { get; set; } = string.Empty;
        
        [Required]
        [MinLength(5,ErrorMessage ="CompanyName must be 5 characters")]
        [MaxLength(255,ErrorMessage ="CompanyName cannot be over 255 characters")]
        public string CompanyName{get;set;} = string.Empty;
        [Required]
        [Range(1,1000000000)]
        public decimal Purchase {get;set;} 
        [Required]
        [Range(0.001,100)]
        public decimal LastDiv { get; set; }

        [Required]
        [MinLength(5,ErrorMessage ="Industry must be 5 characters")]
        [MaxLength(255,ErrorMessage ="Industry cannot be over 255 characters")]
        public string Industry{get;set;} = string.Empty;
        [Required]
        [Range(1,5000000000000)]
        public long MarketCap {get;set;}

    }
}