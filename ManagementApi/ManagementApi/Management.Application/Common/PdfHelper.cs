using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Management.Application.Common
{
    public class PdfHelper
    {
        /// <summary>
        /// 网站内容输出到PDF
        /// </summary>
        /// <param name="strHTML">html text</param>
        /// <param name="cssPath">css url</param>
        /// <param name="FilePath">pdf url</param>
        public static void HTMLTestToPdf(string strHTML, string cssPath, string FilePath)
        {
            Document document = new Document(PageSize.A3);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(FilePath, FileMode.Create));
            var cssText = File.ReadAllText(cssPath);
            document.Open();
            byte[] data = Encoding.UTF8.GetBytes(strHTML);//字串转成byte[]
            MemoryStream msInput = new MemoryStream(data);
            MemoryStream cssMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(cssText));
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, msInput, cssMemoryStream, Encoding.UTF8, new UnicodeFontFactory());
            PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, document.PageSize.Height, 1f);
            //将pdfDest设定的资料写到PDF档
            PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
            writer.SetOpenAction(action);
            document.Close();
            cssMemoryStream.Close();
            msInput.Close();
        }

        /// <summary>
        ///  html to pdf
        /// </summary>
        /// <param name="url">html url</param>
        /// <param name="cssPath">css url</param>
        /// <param name="FilePath">pdf url</param>
        public static void HtmlToPdf(string url, string cssPath, string FilePath)
        {
            WebClient wc = new WebClient();
            //从网址下载Html字串
            string htmlText = wc.DownloadString(url);
            HTMLTestToPdf(htmlText, cssPath, FilePath);
        }

        /// <summary>  
        /// 判断是否有乱码  
        /// </summary>  
        /// <param name="txt"></param>  
        /// <returns></returns>  
        public static bool isMessyCode(string txt)
        {
            var bytes = Encoding.UTF8.GetBytes(txt);            //239 191 189              
            for (var i = 0; i < bytes.Length; i++)
            {
                if (i < bytes.Length - 3)
                    if (bytes[i] == 239 && bytes[i + 1] == 191 && bytes[i + 2] == 189)
                    {
                        return true;
                    }
            }
            return false;
        }

        /// <summary>
        /// html图片相对路径替换成绝对路径
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string getImage(string input)
        {
            if (input == null)
                return string.Empty;
            string tempInput = input;
            string pattern = @"<img(.|\n)+?>";
            string src = string.Empty;
            HttpContext context = HttpContext.Current;

            //Change the relative URL's to absolute URL's for an image, if any in the HTML code.
            foreach (Match m in Regex.Matches(input, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline |
            RegexOptions.RightToLeft))
            {
                if (m.Success)
                {
                    string tempM = m.Value;
                    string pattern1 = "src=[\'|\"](.+?)[\'|\"]";
                    Regex reImg = new Regex(pattern1, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    Match mImg = reImg.Match(m.Value);

                    if (mImg.Success)
                    {
                        src = mImg.Value.ToLower().Replace("src=", "").Replace("\"", "").TrimStart('/').Replace("/", "\\");

                        if (src.ToLower().Contains("http://") == false)
                        {
                            src = "src=\"" + AppDomain.CurrentDomain.BaseDirectory + src + "\"";
                            try
                            {
                                tempM = tempM.Remove(mImg.Index, mImg.Length);
                                tempM = tempM.Insert(mImg.Index, src);

                                //insert new url img tag in whole html code
                                tempInput = tempInput.Remove(m.Index, m.Length);
                                tempInput = tempInput.Insert(m.Index, tempM);
                                UTF8Encoding utf8 = new UTF8Encoding();//此处需要转码一下
                                Byte[] encodedBytes = utf8.GetBytes(tempInput);
                                tempInput = utf8.GetString(encodedBytes);
                            }
                            catch (Exception e) { }
                        }
                    }
                }
            }
            return tempInput;
        }

        /// <summary>
        /// 获取网站内容，包含了 HTML+CSS 不包含图片
        /// </summary>
        /// <param name="strUrl">url</param>
        /// <returns>返回网页信息</returns>
        public static string GetWebContent(string strUrl)
        {
            try
            {
                if (strUrl.Substring(0, 5) == "https")
                {
                    // 解决WebClient不能通过https下载内容问题
                    System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                    delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                System.Security.Cryptography.X509Certificates.X509Chain chain,
                                System.Net.Security.SslPolicyErrors sslPolicyErrors)
                    {
                        return true; // **** Always accept
                    };
                }

                WebClient web = new WebClient();
                if (strUrl.Substring(0, 5) != "https")
                {
                    web.Credentials = CredentialCache.DefaultCredentials;
                }
                else
                {
                    web.Credentials = CredentialCache.DefaultCredentials;
                }
                //获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                byte[] pageData = web.DownloadData(strUrl);
                //从指定网站下载数据
                string pageHtml = Encoding.UTF8.GetString(pageData);

                bool isBool = isMessyCode(pageHtml);//判断使用哪种编码 读取网页信息
                if (!isBool)
                {
                    string pageHtml1 = Encoding.UTF8.GetString(pageData);
                    pageHtml = pageHtml1;
                }
                else
                {
                    string pageHtml2 = Encoding.Default.GetString(pageData);
                    pageHtml = pageHtml2;
                }

                return pageHtml;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 获取网站内容，包含了 HTML+CSS 可包含图片
        /// </summary>
        /// <param name="strUrl"></param>
        /// <param name="isImg"></param>
        /// <returns></returns>
        public static string GetWebContent(string strUrl, bool isImg = false)
        {
            try
            {
                WebClient web = new WebClient();

                web.Credentials = CredentialCache.DefaultCredentials;
                //获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                byte[] pageData = web.DownloadData(strUrl);
                //从指定网站下载数据
                string pageHtml = Encoding.UTF8.GetString(pageData);

                if (isImg)
                {
                    pageHtml = getImage(pageHtml);//输出html上的图片
                }

                bool isBool = isMessyCode(pageHtml);//判断使用哪种编码 读取网页信息
                if (!isBool)
                {
                    string pageHtml1 = Encoding.UTF8.GetString(pageData);
                    pageHtml = pageHtml1;
                }
                else
                {
                    string pageHtml2 = Encoding.Default.GetString(pageData);
                    pageHtml = pageHtml2;
                }

                return pageHtml;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 获取CSS内容，包含CSS 不包含图片
        /// </summary>
        /// <param name="strUrl">url</param>
        /// <returns></returns>
        public static MemoryStream GetCss(string strUrl)
        {
            if (string.IsNullOrEmpty(strUrl))
            {
                return null;
            }
            var cssText = File.ReadAllText(strUrl);
            MemoryStream cssMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(cssText));

            return cssMemoryStream;
        }

        /// <summary>
        /// 将HTML和CSS 输出到PDF档里
        /// </summary>
        /// <param name="htmlText">html文本</param>
        /// <param name="cssPath">CSS路径</param>
        /// <returns></returns>
        public static byte[] ConvertHtmlTextToPDF(string htmlText, MemoryStream cssMemoryStream, bool IsOutputHorizontally = false)
        {
            if (string.IsNullOrEmpty(htmlText))
            {
                return null;
            }
            //避免当htmlText无任何html tag标签的纯文字时，转PDF时会挂掉，所以一律加上<p>标签  
            //htmlText = "<p>" + htmlText + "</p>";
            MemoryStream outputStream = new MemoryStream();//要把PDF写到哪个串流  

            byte[] data = Encoding.UTF8.GetBytes(htmlText);//字串转成byte[]  
            //html 
            MemoryStream msInput = new MemoryStream(data);

            Document doc = new Document(PageSize.A4);//要写PDF的文件，建构子没填的话预设值是A4

            if (IsOutputHorizontally)
            {
                doc.SetPageSize(PageSize.A4.Rotate());    //横向输出
            }

            PdfWriter writer = PdfWriter.GetInstance(doc, outputStream);
            //指定文件预设开档时的缩放为100%  
            PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1f);
            //开启Document文件   
            doc.Open();

            //使用XMLWorkerHelper把Html parse到PDF档里  
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, cssMemoryStream, Encoding.UTF8, new UnicodeFontFactory());
            //XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, Encoding.UTF8);

            //将pdfDest设定的资料写到PDF档  
            PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
            writer.SetOpenAction(action);
            doc.Close();
            msInput.Close();
            cssMemoryStream.Close();
            outputStream.Close();
            //回传PDF档案   
            return outputStream.ToArray();
        }
        /// <summary>
        /// 将HTML文字 输出到PDF档里
        /// </summary>
        /// <param name="htmlText">html文本</param>
        /// <returns></returns>
        public static byte[] ConvertHtmlTextToPDF(string htmlText, bool IsOutputHorizontally = false)
        {
            if (string.IsNullOrEmpty(htmlText))
            {
                return null;
            }
            //避免当htmlText无任何html tag标签的纯文字时，转PDF时会挂掉，所以一律加上<p>标签  
            //htmlText = "<p>" + htmlText + "</p>";
            MemoryStream outputStream = new MemoryStream();//要把PDF写到哪个串流  

            byte[] data = Encoding.UTF8.GetBytes(htmlText);//字串转成byte[]  
            //html 
            MemoryStream msInput = new MemoryStream(data);

            Document doc = new Document(PageSize.A4);//要写PDF的文件，建构子没填的话预设值是A4

            if (IsOutputHorizontally)
            {
                doc.SetPageSize(PageSize.A4.Rotate());    //横向输出
            }
            PdfWriter writer = PdfWriter.GetInstance(doc, outputStream);
            //指定文件预设开档时的缩放为100%  

            PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1f);
            //开启Document文件   
            doc.Open();

            //使用XMLWorkerHelper把Html parse到PDF档里  
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, Encoding.UTF8, new UnicodeFontFactory());

            //将pdfDest设定的资料写到PDF档  
            PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
            writer.SetOpenAction(action);
            doc.Close();
            msInput.Close();
            outputStream.Close();
            //回传PDF档案   
            return outputStream.ToArray();
        }
        /// <summary>         
        /// PDF解密         
        /// </summary>         
        /// <param name="sourceFullName">源文件路径(如：D:\old.pdf)</param>
        /// <param name="newFullName">目标文件路径(如:D:\new.pdf)</param>         
        private static bool deletePDFEncrypt(string sourceFullName, string newFullName)
        {
            if (string.IsNullOrEmpty(sourceFullName) || string.IsNullOrEmpty(newFullName))
            {
                throw new Exception("源文件路径或目标文件路径不能为空或null.");
            }
            //Console.WriteLine("读取PDF文档");             
            try
            {
                // 创建一个PdfReader对象                 
                PdfReader reader = new PdfReader(sourceFullName);
                PdfReader.unethicalreading = true;
                // 获得文档页数                 
                int n = reader.NumberOfPages;
                // 获得第一页的大小                 
                iTextSharp.text.Rectangle pagesize = reader.GetPageSize(1);
                float width = pagesize.Width;
                float height = pagesize.Height;
                // 创建一个文档变量  
                Document document = new Document(pagesize, 50, 50, 50, 50);
                // 创建该文档                 
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(newFullName, FileMode.Create));
                // 打开文档
                document.Open();
                int i = 0;
                int p = 0;
                // 添加内容                
                PdfContentByte cb = writer.DirectContent;
                while (i < n)
                {
                    document.NewPage();
                    p++;
                    i++;
                    PdfImportedPage page1 = writer.GetImportedPage(reader, i);
                    cb.AddTemplate(page1, 1f, 0, 0, 1f, 0, 0);
                }
                // 关闭文档                 
                document.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// PDF加密
        /// </summary>
        /// <param name="SrcPath">来源</param>
        /// <param name="OutPath">输出</param>
        /// <param name="strength">强度(高:安全,但耗时)</param>
        /// <param name="UserPw">user密码</param>
        /// <param name="OwrPw">owner密码</param>
        /// <param name="pmss">权限(ex.PdfWriter.ALLOW_SCREENREADERS)</param>
        public static void EncryptPDF(string SrcPath, string OutPath, bool strength, string UserPw, string OwrPw, int pmss)
        {
            using (PdfReader reader = new PdfReader(SrcPath))
            {
                using (var os = new FileStream(OutPath, FileMode.Create))
                {
                    PdfEncryptor.Encrypt(reader, os, strength, UserPw, OwrPw, pmss);
                }
            }
        }
    }
    public class UnicodeFontFactory : FontFactoryImp
    {
        private static readonly string arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arialuni.ttf");//arial unicode MS是完整的unicode字型。
        private static readonly string sumsongPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "simsun.ttc,0");//宋体

        public override Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color, bool cached)
        {
            //可用Arial或宋体，自己选一个
            BaseFont baseFont = BaseFont.CreateFont(sumsongPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            return new Font(baseFont, size, style, color);
        }
    }
}
