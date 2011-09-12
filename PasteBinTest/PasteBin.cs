using System.Text;
using System.Net;
using System.IO;
using System.Configuration;

namespace PasteBinSample
{
    public static class PasteBin
    {
        private const string _pasteBinUrl = "http://pastebin.com/api/api_post.php";
        private static string _devKey = ConfigurationManager.AppSettings["pasteBinDeveloperApiKey"];

        /// <summary>
        /// Creates a new PasteBin post, with a default name.
        /// </summary>
        /// <param name="text">Text to post</param>
        /// <returns>PasteBin URL</returns>
        public static string Create(string text)
        {
            return Create("My Paste", text);
        }

        /// <summary>
        /// Creates a new PasteBin post.
        /// </summary>
        /// <param name="name">Name of post</param>
        /// <param name="text">Text to post</param>
        /// <returns>PasteBin URL</returns>
        public static string Create(string name, string text)
        {
            string pasteBinUrl = "";

            // Create the request.
            WebRequest wr = WebRequest.Create(_pasteBinUrl);
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] bData = encoding.GetBytes("api_option=paste&api_dev_key=" + _devKey + "&api_paste_code=" + HtmlEncoder.UrlEncode(text) + "&api_paste_private=1&api_paste_expire_date=1D&api_paste_name=" + HtmlEncoder.UrlEncode(name));
            wr.Method = "POST";
            wr.ContentType = "application/x-www-form-urlencoded";
            wr.ContentLength = bData.Length;

            // Send the request.
            using (Stream sMyStream = wr.GetRequestStream())
            {
                sMyStream.Write(bData, 0, bData.Length);
            }

            // Read the response.
            using (WebResponse response = wr.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    pasteBinUrl = reader.ReadToEnd();
                }
            }

            return pasteBinUrl;
        }
    }
}
