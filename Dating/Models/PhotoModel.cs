using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dating.Models
{
    public class PhotoModel
    {   
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public UserModel User { get; set; }
        public string PublicId { get; set; } 
        [ForeignKey("UserId")]
        public int UserId { get; set; }
    }
}