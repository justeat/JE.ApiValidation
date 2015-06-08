using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using OpenRasta.Codecs;
using OpenRasta.TypeSystem;
using OpenRasta.Web;

namespace JE.ApiValidation.Examples.OpenRasta
{
    [MediaType("application/json; charset=utf-8", "json")]
    // ReSharper disable ClassNeverInstantiated.Global
    public class JsonCodec : IMediaTypeWriter, IMediaTypeReader
        // ReSharper restore ClassNeverInstantiated.Global
    {
        public object ReadFrom(IHttpEntity request, IType destinationType, string destinationName)
        {
            if (destinationType.StaticType == null)
            {
                throw new InvalidOperationException();
            }

            string body;
            using (var reader = new StreamReader(request.Stream))
            {
                body = reader.ReadToEndAsync().Result;
            }

            var postData = body;

            return JsonConvert.DeserializeObject(postData, destinationType.StaticType);
        }

        public object Configuration { get; set; }

        public void WriteTo(object entity, IHttpEntity response, string[] codecParameters)
        {
            if (entity == null)
            {
                return;
            }

            var output = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(entity));
            response.Stream.Write(output, 0, output.Length);

            response.ContentType = new MediaType("application/json") { CharSet = "utf-8" };
        }
    }
}
