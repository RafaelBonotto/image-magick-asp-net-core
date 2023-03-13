using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace ImageMagickExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageMagickExampleController : ControllerBase
    {
        [HttpGet("JpegToPdf")]
        public async Task<IActionResult> JpegToPdfAsync()
        {
            List<string> images = new List<string>();
            var imgByte = System.IO.File.ReadAllBytes(@"C:/temp/compResidencia.jpeg"); // OK

            using (var ms = new MemoryStream(imgByte))                      
            {
                using (var magickImageCollection = new MagickImageCollection())
                {
                    magickImageCollection.AddRange(ms);

                    foreach (var item in magickImageCollection)
                    {
                        var image = new MagickImage(item);
                        image.Format = MagickFormat.Pdf;
                        image.Depth = 1;
                        image.SetCompression(CompressionMethod.Zip);
                        images.Add(image.ToBase64());
                    }
                }
            }
            var ret = images;
            return Ok(ret);
        }

        [HttpGet("teste")]
        public async Task<IActionResult> TestAsync()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(@"C:\Users");
            System.IO.FileStream stream = new FileStream(@"C:\temp\testeWordPdf\file.pdf", FileMode.CreateNew);
            System.IO.BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(bytes, 0, bytes.Length);
            writer.Close();

            return Ok();
        }
    }
}