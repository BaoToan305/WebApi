using CreateApiProject.ModelDto;
using CreateApiProject.Models;
using CreateApiProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreateApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        public readonly ILogger<ImageController> logger;
        public readonly ImageRepository imageRepository;

        public readonly IWebHostEnvironment webHostEnvironment;
        public readonly IHttpContextAccessor httpContextAccessor;

        public ImageController(ILogger<ImageController> logger, ImageRepository imageRepository, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.imageRepository = imageRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDto requestBody)
        {
            ValidateFileUpload(requestBody);

            if(ModelState.IsValid)
            {
                var imageModel = new Image
                {
                    File = requestBody.File,
                    image_description = requestBody.FileDescription,
                    image_extension = Path.GetExtension(requestBody.File.FileName),
                    image_size = requestBody.File.Length,
                    image_path = requestBody.FileName,
                    image_name = Path.GetFileName(requestBody.File.FileName)
                };

               await UploadImage(imageModel);


                var imageBody = new ImageBody
                {
                    image_name = imageModel.image_name,
                    image_path = imageModel.image_path,
                    image_description = imageModel.image_description,
                    image_extension = imageModel.image_extension,
                    image_size = imageModel.image_size,
                };
                int check = await imageRepository.UploadImageAsync(imageBody);
                if (check != 0)
                    return Ok(imageModel);
                else
                {
                    return BadRequest();
                }
            }


            return BadRequest();
        }

        private async Task UploadImage(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
                $"{image.image_name}");

            using var stream = new FileStream(localFilePath, FileMode.Create);

            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.image_name}";
            image.image_path = urlFilePath;

        }

        private void ValidateFileUpload(ImageUploadDto image)
        {
            var allowExtention = new string[] { ".jpg", ".jped", ".png" };

            if(!allowExtention.Contains(Path.GetExtension(image.File.FileName))) 
            {
                ModelState.AddModelError("File", "File ảnh sai định dạng");
            }

            if(image.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "Độ lớn của file tối đa 10MB");
            }
        }
    }
}
