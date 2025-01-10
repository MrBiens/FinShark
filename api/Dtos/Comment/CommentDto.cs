using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class CommentDto
    {
        public int Id{get;set;}
        public int? StockId { get; set; }

        public string Title{get;set;} = string.Empty;

        public string Content{get;set;} = string.Empty;

        public DateTime CreateOn {get;set;} = DateTime.Now;


    }
}