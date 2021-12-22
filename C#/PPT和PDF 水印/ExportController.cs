using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BIPortalService.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Spire.Presentation;
using Spire.Presentation.Drawing;


namespace BIPortalService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExportController : Controller
    {
        /// 设置Pdf水印
        /// pdf所在的文件完整路径
        /// 生成水印的pdf文件完整路径
        [HttpGet]
        [AllowAnonymous]
        public IActionResult SetTextMarkOfPdf(string text)
        {
            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            FileStream fileStream = null;
            IHostingEnvironment hostingEnvironment = DIServiceUtil.GetService<IHostingEnvironment>();
            string path = hostingEnvironment.ContentRootPath + "\\wwwroot\\export\\";
            string filePath = path + "aa.pdf";
            string outputfilepath = path + Guid.NewGuid().ToString() + ".pdf";
            try
            {
                pdfReader = new PdfReader(filePath);
                fileStream = new FileStream(outputfilepath, FileMode.Create);
                pdfStamper = new PdfStamper(pdfReader, fileStream);
                int total = pdfReader.NumberOfPages + 1;
                iTextSharp.text.Rectangle psize = pdfReader.GetPageSize(1);
                float width = psize.Width;
                float height = psize.Height;
                PdfContentByte content;
                BaseFont font = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\SIMFANG.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                PdfGState gs = new PdfGState();

                for (int i = 1; i < total; i += 1)
                {
                    content = pdfStamper.GetOverContent(i);
                    //透明度
                    gs.FillOpacity = 0.3f;
                    content.SetGState(gs);
                    content.SetGrayFill(0.3f);
                    //开始写入文本
                    content.BeginText();
                    content.SetColorFill(BaseColor.BLACK);
                    content.SetFontAndSize(font, 100);
                    content.SetTextMatrix(0, 0);
                    content.ShowTextAligned(Element.ALIGN_CENTER, text, width / 2 - 50, height / 2 - 50, 55);
                    content.EndText();
                    content.Stroke();
                }
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (pdfStamper != null)
                {
                    pdfStamper.Close();
                }
                if (pdfReader != null)
                {
                    pdfReader.Close();
                }
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
            return Ok(outputfilepath);
        }
        public IActionResult SetImgMarkOfPdf()
        {
            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            FileStream fileStream = null;
            IHostingEnvironment hostingEnvironment = DIServiceUtil.GetService<IHostingEnvironment>();
            string path = hostingEnvironment.ContentRootPath + "\\wwwroot\\export\\";
            string outputfilepath = path + Guid.NewGuid().ToString() + ".pdf";
            string filePath = path + "aa.pdf";
            try
            {
                pdfReader = new PdfReader(filePath);
                fileStream = new FileStream(outputfilepath, FileMode.Create);
                pdfStamper = new PdfStamper(pdfReader, fileStream);
                int total = pdfReader.NumberOfPages + 1;
                iTextSharp.text.Rectangle psize = pdfReader.GetPageSize(1);
                float width = psize.Width;
                float height = psize.Height;
                PdfContentByte content;
                PdfGState gs = new PdfGState();

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(path + "loading.gif");
                img.SetAbsolutePosition(150, 100);
                for (int i = 1; i < total; i += 1)
                {
                    content = pdfStamper.GetOverContent(i);
                    content.SetGState(gs);
                    content.AddImage(img);
                }
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (pdfStamper != null)
                {
                    pdfStamper.Close();
                }
                if (pdfReader != null)
                {
                    pdfReader.Close();
                }
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
            return Ok(outputfilepath);
        }
        /// <summary>
        /// 设置logo图片水印
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult SetImgMarkOfPpt()
        {
            IHostingEnvironment hostingEnvironment = DIServiceUtil.GetService<IHostingEnvironment>();
            string path = hostingEnvironment.ContentRootPath + "\\wwwroot\\export\\";
            string outfile = path + Guid.NewGuid().ToString()+ ".pptx";
            string oldfile = path + "test.pptx";
            try
            {
                //加载PowerPoint文档
                Presentation ppt = new Presentation();
                ppt.LoadFromFile(oldfile, FileFormat.Pptx2010);

                RectangleF rect = new RectangleF(340, 150, 200, 100);

                //添加图片水印到幻灯片母版
                int i = 0;
                foreach (var s in ppt.Slides)
                {
                    //添加图片到幻灯片母版的指定位置
                    IEmbedImage image = ppt.Slides[i].Shapes.AppendEmbedImage(ShapeType.Rectangle, path+"loading.gif", rect);
                    //设置图片的透明度
                    image.PictureFill.Picture.Transparency = 50;
                    //锁定图片使其不能被选择
                    image.ShapeLocking.SelectionProtection = true;
                    //设置图片的边框为无边框
                    image.Line.FillType = FillFormatType.None;
                    i += 1;
                }

                //保存文档
                ppt.SaveToFile(outfile, FileFormat.Pptx2010);
                System.IO.File.Delete(oldfile);
            }
            catch (DocumentException de)
            {

            }
            return Ok(outfile);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SetTextMarkOfPpt(string text)
        {

            IHostingEnvironment hostingEnvironment = DIServiceUtil.GetService<IHostingEnvironment>();
            string path = hostingEnvironment.ContentRootPath + "\\wwwroot\\export\\";
            string oldfile = path + "test.pptx";
            string outfile = path + Guid.NewGuid().ToString()+".pptx";

            Presentation ppt = new Presentation();
            ppt.LoadFromFile(oldfile, FileFormat.Pptx2010);
            try
            {
                int fontSize = 90;
                System.Drawing.Font stringFont = new System.Drawing.Font("Arial", fontSize);
                float with = System.Text.Encoding.UTF8.GetBytes(text).Length* fontSize/2;
                RectangleF rect = new RectangleF((ppt.SlideSize.Size.Width - with) / 2, (ppt.SlideSize.Size.Height - fontSize) / 2, with, fontSize);
                int i = 0;
                foreach (var s in ppt.Slides)
                {
                    IAutoShape shape = ppt.Slides[i].Shapes.AppendShape(Spire.Presentation.ShapeType.Rectangle, rect);
                    shape.Fill.FillType = FillFormatType.None;
                    shape.ShapeStyle.LineColor.Color = Color.White;
                    shape.Rotation = -45;

                    //设定形状保护属性、填充模式
                    shape.Locking.SelectionProtection = true;
                    shape.Line.FillType = FillFormatType.None;

                    //设置文本水印文字，并设置水印填充模式、水印颜色、大小等
                    shape.TextFrame.Text = text;
                    TextRange textRange = shape.TextFrame.TextRange;
                    textRange.Fill.FillType = Spire.Presentation.Drawing.FillFormatType.Solid;
                    textRange.Fill.SolidColor.Color = Color.FromArgb(150, Color.LightBlue);
                    textRange.FontHeight = 90;
                    i += 1;
                }

                //保存并打开文档
                ppt.SaveToFile(outfile, FileFormat.Pptx2010);
                System.IO.File.Delete(oldfile);
            }
            catch (Exception) { }
            finally
            {
                ppt.Dispose();
            }
            return Ok(outfile);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult DeletFile(string fileName)
        {
            IHostingEnvironment hostingEnvironment = DIServiceUtil.GetService<IHostingEnvironment>();
            string root = hostingEnvironment.ContentRootPath + "\\wwwroot\\export\\";
            string path = root + fileName;
            if(System.IO.File.Exists(path)){ 
                System.IO.File.Delete(path);
            }
            return Ok("");
        }

    }
}
