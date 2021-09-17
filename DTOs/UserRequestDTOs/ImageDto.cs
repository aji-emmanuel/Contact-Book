using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.DTOs
{
    public class ImageDto
    {
        public IFormFile Image { get; set; }
    }
}
