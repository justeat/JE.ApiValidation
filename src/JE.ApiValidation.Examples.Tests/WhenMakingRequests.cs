using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JE.ApiValidation.DTOs;
using JE.ApiValidation.Examples.WebApi;
using Microsoft.Owin.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;

namespace JE.ApiValidation.Examples.Tests
{
    public class WhenMakingRequests
    {
        [Test]
        public async void WhenValidShouldGet200Ok()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var response = await HttpResponseMessage(server, new { Name = "foo" }).ConfigureAwait(false);

                response.StatusCode.ShouldBe(HttpStatusCode.OK);
            }
        }

        [Test]
        public async void WhenRequestIsInvalidShouldGet400BadRequestWithStandardErrorResponse()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var response = await HttpResponseMessage(server, new { Name = "" }).ConfigureAwait(false);

                response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

                var ser = ReadErrorBody(response);
                ser.Code.ShouldBe(40000);
                ser.Errors.Count.ShouldBe(1);
            }
        }

        [Test]
        public async void WhenThereIsAnErrorDuringResponseProcessingShouldGet400BadRequestWithStandardErrorResponse()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var response = await HttpResponseMessage(server, new { Name = "wibble" }).ConfigureAwait(false);

                response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

                var ser = ReadErrorBody(response);
                ser.Code.ShouldBe(45000);
                ser.Errors.Count.ShouldBe(1);
            }
        }

        private static async Task<HttpResponseMessage> HttpResponseMessage(TestServer server, object body)
        {
            var content = Body(body);
            var response =
                await server.CreateRequest("/api/widgets").And(x => x.Content = content).PostAsync().ConfigureAwait(false);
            return response;
        }

        private static StandardErrorResponse ReadErrorBody(HttpResponseMessage response)
        {
            var responseBody = response.Content.ReadAsStringAsync().Result;
            var ser = JsonConvert.DeserializeObject<StandardErrorResponse>(responseBody);
            return ser;
        }

        private static StringContent Body(object dto)
        {
            return new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
        }
    }
}
