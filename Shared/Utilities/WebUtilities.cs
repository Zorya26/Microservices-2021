using System;
using System.IO;
using System.Net;
using System.Text;
using static System.Int32;

namespace Shared.Utilities
{
    public static class WebUtilities
    {
        public static string GetRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.KeepAlive = false;
            request.Method = "GET";
            request.ServicePoint.Expect100Continue = false;
            var response = (HttpWebResponse)request.GetResponse();

            return GetResponse(response);
        }

        public static string GetResponse(HttpWebResponse webResponse)
        {
            string response;
            var receiveStream = webResponse.GetResponseStream();

            using (var readStream =
                new StreamReader(receiveStream ?? throw new InvalidOperationException(), Encoding.UTF8))
            {
                response = readStream.ReadToEnd();
            }

            return response;
        }


        public static string SendPostRequest(string url, string body)
        {
            var request = CreatePostRequest(url);
            return GetPostResponse(body, request);
        }


        public static HttpWebRequest CreatePostRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.KeepAlive = false;
            request.Method = "POST";
            request.Timeout = MaxValue;
            request.ProtocolVersion = HttpVersion.Version10;

            request.ContentType = "application/json";
            return request;
        }

      

        public static string GetPostResponse(string body, WebRequest request)
        {
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var response = (HttpWebResponse)request.GetResponse();

            return GetResponse(response);
        }
    }
}