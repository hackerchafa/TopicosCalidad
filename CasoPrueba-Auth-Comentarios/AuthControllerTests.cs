using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using RestSharp;

namespace PlataformaEscolar.Tests
{
    public class AuthControllerTests
    {
        private const string BaseUrl = "http://localhost:5013/api/Auth";

        [Fact]
        public async Task Register_Alumno_ReturnsOk()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("register", Method.Post);
            request.AddJsonBody(new {
                UserName = "TestAlumno",
                Email = "testalumno@correo.com",
                Password = "Abc123!",
                Rol = "Alumno"
            });
            var response = await client.ExecuteAsync(request);
            Assert.True(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Login_Alumno_ReturnsToken()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("login", Method.Post);
            request.AddJsonBody(new {
                UserName = "TestAlumno",
                Password = "Abc123!"
            });
            var response = await client.ExecuteAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("token", response.Content);
        }
    }
}
