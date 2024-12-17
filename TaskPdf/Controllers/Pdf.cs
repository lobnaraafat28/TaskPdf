using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using Path = System.IO.Path;
using TaskPdf.ResultEntities;
using Org.BouncyCastle.Asn1.Cms;
using static iTextSharp.text.pdf.AcroFields;
using System.Text.RegularExpressions;

namespace TaskPdf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pdf : ControllerBase
    {
        [HttpPost]
        public Result SearchingPdf([FromBody]IFormFile file, [FromBody]JsonObject keywords)
        {
           Result result = new Result();
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            else
            {
                string extractedPath = "D:\\Pdf";
                Directory.CreateDirectory(extractedPath);
                if (Path.HasExtension(file.Name).Equals("zip")){
                    string zipPath = Path.Combine(extractedPath, file.Name + ".zip");
                    using (FileStream fileStream = new FileStream(zipPath, FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fileStream);
                    }
                    using ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Read);
                    System.Collections.ObjectModel.ReadOnlyCollection<ZipArchiveEntry> entries = archive.Entries;
                    ZipFile.ExtractToDirectory(zipPath, extractedPath);
                    string[] files = Directory.GetFiles(extractedPath, "*", SearchOption.AllDirectories);
                    JsonObject jsonObject = JsonSerializer.Deserialize<JsonObject>(keywords);

                    JsonArray keywordsArray = jsonObject["keywords"].AsArray();

                    string[] keyword = new string[keywordsArray.Count];
                    for (int i = 0; i < keywordsArray.Count; i++)
                    {
                        keywords[i] = keywordsArray[i].ToString().Trim('"');
                    }
                    foreach (var item in files)
                    {
                        foreach (var key in keyword)
                        {

                        }
                        FileInformation fileInfo = new FileInformation();
                        Metadata data = new Metadata();
                        fileInfo.FileName = Path.GetFileName(item);


                      

                    }

                }
                return result;

            }
        }
        public static int GetWordCountFromString(IFormFile file,string word)
        {
            int count = 0;
            if (string.IsNullOrEmpty(word))
                return 0;
            else
            {
                var reader = new iTextSharp.text.pdf.PdfReader(file.Name);

                StringBuilder str = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    string pageText = PdfTextExtractor.GetTextFromPage(reader, i);
                    str.AppendLine(pageText);
                }
                string fullText = str.ToString();

                string pattern = $@"\b{Regex.Escape(word)}\b"; 
                count = Regex.Matches(fullText, pattern, RegexOptions.IgnoreCase).Count;

            }
            return count;
            
        }
    }
}
