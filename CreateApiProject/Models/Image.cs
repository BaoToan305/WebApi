using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateApiProject.Models
{
    public class Image
    {
        [NotMapped]
        public IFormFile File { get; set; }
        public string image_name { get; set; }

        public string? image_description { get; set; }
        public string image_extension { get; set; }
        public long image_size { get; set; }
        public string image_path { get; set; }
    }

    public class ImageBody
    {
        public string image_name { get; set; }

        public string? image_description { get; set; }
        public string image_extension { get; set; }
        public long image_size { get; set; }
        public string image_path { get; set; }
    }
}
