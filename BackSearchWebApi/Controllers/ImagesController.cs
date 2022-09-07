using Microsoft.AspNetCore.Mvc;
using BackSearch.DAL;
using BackSearchWebApi.DTO;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace BackSearchWebApi.Controllers
{
    [ApiController]
    [Route("/image")]
    public class ImagesController : Controller
    {
        private ImageRepository imgRepo;
        public ImagesController()
        {
            imgRepo = new ImageRepository(new DataContext());
        }
        [HttpPost("addImage")]
        public async Task<IActionResult> AddImage([FromBody] ImageDto imgData)
        {

                ImageObj image = new ImageObj()
                {
                    Url = imgData.Url,
                    Description = imgData.Description,
                };

                await imgRepo.InsertAsync(image);
                return Ok(image);

        }

        [HttpGet("getImage")]
        public async Task<IActionResult> GetImages([FromQuery]string? filter = null)
        {
            return Ok(imgRepo.GetByFilter(filter).ToList());
        }
        [HttpGet("getImageById")]
        public async Task<IActionResult> GetImages([FromQuery] int id )
        {
            ImageObj? img = imgRepo.GetById(id);
            if (img != null)
            {
                return Ok(img);
            }
            return BadRequest("Invalid id");
        }
        [HttpPut("editImage")]
        public async Task<IActionResult> EditImage([FromQuery] int Id,[FromBody] ImageDto imgData)
        {
            ImageObj imgSrc = new ImageObj() { Url= imgData.Url, Description=imgData.Description };

            if(imgRepo.UpdateAsync(Id, imgSrc) !=null)
            {
                return Ok(imgSrc);
            }
            return BadRequest("imageError");
            //await imgRepo.InsertAsync(image);
            
        }
        [HttpDelete("deleteImage")]
        public async Task<IActionResult> DeleteImage([FromQuery] int Id)
        {
            await imgRepo.DeleteAsync(Id);

            return Ok();
        }
    }
}
