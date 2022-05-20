﻿using System.ComponentModel.DataAnnotations;

namespace LibraryProject.API.DTO_s
{
    public class PublisherRequest
    {
        [Required]
        [StringLength(62, ErrorMessage = "Name must not contain more than 62 chars")]
        public string Name { get; set; }  
    }
}
