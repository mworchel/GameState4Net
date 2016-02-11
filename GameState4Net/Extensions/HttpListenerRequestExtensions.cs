using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameState4Net
{
    public static class HttpListenerRequestExtensions
    {
        public static string GetContent(this HttpListenerRequest self)
        {
            var content = "";

            if (self.InputStream != null)
            {
                using (StreamReader reader = new StreamReader(self.InputStream))
                {
                    content = reader.ReadToEnd();
                }
            }

            return content;
        }
    }
}