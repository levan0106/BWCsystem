using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
using System.Web;
using System.Text;
using System.Collections.Generic;

namespace BWC.Core.Export
{
    public class GeneratePdf
    {

        string colTemplate = "<td style='border: 1px solid #ccc'>{0}</td>";
        string headerTemplate = "<th style='background-color: #d8d8d8;border: 1px solid #ccc'>{0}</th>";
        string tableTemplate = @"<table width='100%' cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 8pt;'>
                                    {0}
                                    {1}
                                </table>";

        public string NewPage
        {
            get
            {
                return "<div style='page-break-before:always'>&nbsp;</div>";
            }
        }
        public string ExportFromHtml(string html, string fileName)
        {
            GenerateFromHtml(html, fileName);
            return "\\App_Data\\ExportFolder\\" + fileName + ".pdf";
        }
        public byte[] GenerateFromHtml(string htmlData, string fileName)
        {
            string folderPath = "\\App_Data\\ExportFolder";
            string folderServerPath = HttpContext.Current.Server.MapPath(folderPath);
            if (!Directory.Exists(folderServerPath))
                Directory.CreateDirectory(folderServerPath);

            string fileServerPath = HttpContext.Current.Server.MapPath(string.Format("{0}\\{1}.pdf", folderPath, fileName));
            if (File.Exists(fileServerPath))
                File.Delete(fileServerPath);

            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(htmlData);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 20f, 20f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();

                // Write binary to file
                using (FileStream fs = new FileStream(fileServerPath, FileMode.OpenOrCreate))
                {
                    byte[] buf = stream.ToArray();
                    fs.Write(buf, 0, buf.Length);
                    fs.Flush();
                }
                return stream.ToArray();
            }
        }

        public string HtmlBody(string bodyContent)
        {
            return string.Format("<html><body style='font-size: 9pt;'>{0}</body></html>", bodyContent);
        }

        public string FormatTitle(string value, string htmlTag = "h4")
        {
            return string.Format("<{1}>{0}</{1}>", value, htmlTag);
        }

        public string ToDataTable<T>(List<T> dataSource, Dictionary<string,string> columns, string indexLabel = null)
        {
            string html = string.Format(tableTemplate, BuildRowHeader(columns.Values, indexLabel), BuildRowContent(dataSource, columns.Keys, indexLabel));
            return html;
        }
        private string BuildRowContent<T>(List<T> dataSource, ICollection<string> columns, string indexLabel)
        {
            StringBuilder rowResult = new StringBuilder();
            for (int i=0; i < dataSource.Count;i++)
            {
                rowResult.Append("<tr>");
                if (!string.IsNullOrEmpty(indexLabel))
                {
                    rowResult.Append(string.Format(colTemplate, i + 1));
                }
                rowResult.Append(BuildColumnContent(dataSource[i], columns));
                rowResult.Append("</tr>");
            }
            return rowResult.ToString();
        }
        private string BuildColumnContent<T>(T row, ICollection<string> columns)
        {
            StringBuilder columnResult = new StringBuilder();
            foreach(var col in columns)
            {
                var value = row.GetType().GetProperty(col) == null?string.Empty: row.GetType().GetProperty(col).GetValue(row, null);
                columnResult.Append(string.Format(colTemplate, value != null ? value.ToString() : string.Empty));
            }
            return columnResult.ToString();
        }
        private string BuildRowHeader(ICollection<string> columns, string indexLabel)
        {
            StringBuilder header = new StringBuilder().Append("<tr>");
            if (!string.IsNullOrEmpty(indexLabel))
            {
                header.Append(string.Format(headerTemplate, indexLabel));
            }
            foreach (var col in columns)
            {
                header.Append(string.Format(headerTemplate, col));
            }
            header.Append("</tr>");
            return header.ToString();
        }
    }
}
