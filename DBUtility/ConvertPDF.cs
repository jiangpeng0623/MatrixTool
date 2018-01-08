using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace DBUtility
{
    public class ConvertPDF
    {
        public static string OperatPDF(string PDFPath,string PDFFile)
        {

            string returnPath = "";
            try
            {
                // 创建一个PdfReader对象
                PdfReader reader = new PdfReader(PDFFile);
                // 获得文档页数
                int n = reader.NumberOfPages;
                // 获得第一页的大小
                iTextSharp.text.Rectangle psize = reader.GetPageSize(1);
                float width = psize.Width;
                float height = psize.Height;
                // 创建一个文档变量
                Document document = new Document(psize, 50, 50, 50, 50);
                // 创建该文档
                returnPath = PDFPath + Guid.NewGuid() + ".pdf";
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(returnPath, FileMode.Create));
                // 打开文档
                document.Open();

                int i = 0;
                int p = 0;
                while (i < n)
                {
                    document.NewPage();
                    // 添加内容
                    PdfContentByte cb = writer.DirectContent;
                    p++;
                    i++;
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    cb.AddTemplate(page, 0, 0);
                    BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 10);//14
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Easy Training", width / 2, height - 26, 0);

                    cb.SetFontAndSize(bf, 10);//12
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Novo Nordisk Site Tianjin", width / 2, height - 12, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "page " + p + " of " + n, width / 2, 6, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), width - 100, 6, 0);

                    cb.EndText();
                }
                // 关闭文档
                document.Close();
            }
            catch (Exception de)
            {
                Console.Error.WriteLine(de.Message);
                Console.Error.WriteLine(de.StackTrace);
            }

            return returnPath;
        }

        public static string OperatPDFForName(string PDFPath,string PDFFile,string userID,string fullName)
        {

            string returnPath = "";
            try
            {
                // 创建一个PdfReader对象
                PdfReader reader = new PdfReader(PDFFile);
                // 获得文档页数
                int n = reader.NumberOfPages;
                // 获得第一页的大小
                iTextSharp.text.Rectangle psize = reader.GetPageSize(1);
                float width = psize.Width;
                float height = psize.Height;
                // 创建一个文档变量
                Document document = new Document(psize, 50, 50, 50, 50);
                // 创建该文档
                returnPath = PDFPath + Guid.NewGuid() + ".pdf";
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(returnPath, FileMode.Create));
                // 打开文档
                document.Open();

                int i = 0;
                int p = 0;
                while (i < n)
                {
                    document.NewPage();
                    // 添加内容
                    PdfContentByte cb = writer.DirectContent;
                    p++;
                    i++;
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    cb.AddTemplate(page, 0, 0);
                    BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 10);//14
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Easy Training", width / 2, height - 26, 0);

                    cb.SetFontAndSize(bf, 10);//12
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Novo Nordisk Site Tianjin", width / 2, height - 12, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "page " + p + " of " + n, width / 2, 6, 0);

                    string strTime=System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    float LenTime=cb.GetEffectiveStringWidth(strTime,true)/2;
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, strTime, width - LenTime - 10, 6, 0);

                    string strForSave = "Saved by " + userID + "-" + fullName;
                    float Len = cb.GetEffectiveStringWidth(strForSave, true) / 2;
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, strForSave, width - Len - 10, 20, 0);
                    cb.EndText();
                }
                // 关闭文档
                document.Close();
            }
            catch (Exception de)
            {
                Console.Error.WriteLine(de.Message);
                Console.Error.WriteLine(de.StackTrace);
            }

            return returnPath;
        }
    }
}
