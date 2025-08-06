using System.Net;
using System.Threading.Tasks;
using Xunit;
using RestSharp;
using System.Text.Json;

namespace PlataformaEscolar.Tests
{
    public class ComentarioControllerTests
    {
        private async Task<int> GetAlumnoId()
        {
            var client = new RestClient("http://localhost:5013/api/Alumno");
            var request = new RestRequest("get-by-username", Method.Get);
            request.AddParameter("username", "TestAlumno");
            var response = await client.ExecuteAsync(request);
            var content = response.Content;
            if (content != null)
            {
                try
                {
                    var alumno = JsonSerializer.Deserialize<AlumnoDto>(content);
                    return alumno?.Id ?? 0;
                }
                catch { }
            }
            return 0;
        }

        private async Task<int> GetProfesorId()
        {
            var client = new RestClient("http://localhost:5013/api/Profesor");
            var request = new RestRequest("get-by-username", Method.Get);
            request.AddParameter("username", "TestProfesor");
            var response = await client.ExecuteAsync(request);
            var content = response.Content;
            if (content != null)
            {
                try
                {
                    var profesor = JsonSerializer.Deserialize<ProfesorDto>(content);
                    return profesor?.Id ?? 0;
                }
                catch { }
            }
            return 0;
        }

        private class AlumnoDto
        {
            public int Id { get; set; }
        }
        private class ProfesorDto
        {
            public int Id { get; set; }
        }
        private const string BaseUrl = "http://localhost:5013/api/Comentario";

        private async Task CrearAlumno()
        {
            var client = new RestClient("http://localhost:5013/api/Auth");
            var request = new RestRequest("register", Method.Post);
            request.AddJsonBody(new {
                UserName = "TestAlumno",
                Email = "testalumno@correo.com",
                Password = "Abc123!",
                Rol = "Alumno"
            });
            await client.ExecuteAsync(request);
        }

        private async Task<string> GetTokenAlumno()
        {
            var client = new RestClient("http://localhost:5013/api/Auth");
            var request = new RestRequest("login", Method.Post);
            request.AddJsonBody(new {
                UserName = "TestAlumno",
                Password = "Abc123!"
            });
            var response = await client.ExecuteAsync(request);
            var content = response.Content;
            var token = "";
            if (content != null && content.Contains("token"))
            {
                var start = content.IndexOf("token\":\"") + 8;
                var end = content.IndexOf('"', start);
                token = content.Substring(start, end - start);
            }
            return token;
        }

        [Fact]
        public async Task ComentarAProfesor_ReturnsOk()
        {
            // Crear tanto el alumno como el profesor antes de crear el comentario
            await CrearAlumno();
            await CrearProfesor();
            
            var token = await GetTokenAlumno();
            int alumnoId = await GetAlumnoId();
            int profesorId = await GetProfesorId();
            
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("a-profesor", Method.Post);
            request.AddJsonBody(new {
                Texto = "Buen profesor",
                ProfesorId = profesorId,
                AlumnoId = alumnoId
            });
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = await client.ExecuteAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task CrearProfesor()
        {
            var client = new RestClient("http://localhost:5013/api/Auth");
            var request = new RestRequest("register", Method.Post);
            request.AddJsonBody(new {
                UserName = "TestProfesor",
                Email = "testprofesor@correo.com",
                Password = "Abc123!",
                Rol = "Profesor"
            });
            await client.ExecuteAsync(request);
        }

        private async Task<string> GetTokenProfesor()
        {
            var client = new RestClient("http://localhost:5013/api/Auth");
            var request = new RestRequest("login", Method.Post);
            request.AddJsonBody(new {
                UserName = "TestProfesor",
                Password = "Abc123!"
            });
            var response = await client.ExecuteAsync(request);
            var content = response.Content;
            var token = "";
            if (content != null && content.Contains("token"))
            {
                var start = content.IndexOf("token\":\"") + 8;
                var end = content.IndexOf('"', start);
                token = content.Substring(start, end - start);
            }
            return token;
        }

        [Fact]
        public async Task ComentarAAlumno_ReturnsOk()
        {
            // Asegurar que tanto el alumno como el profesor existan
            await CrearAlumno();
            await CrearProfesor();
            
            var token = await GetTokenProfesor();
            int alumnoId = await GetAlumnoId();
            int profesorId = await GetProfesorId();
            
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("a-alumno", Method.Post);
            request.AddJsonBody(new {
                Texto = "Buen trabajo",
                ProfesorId = profesorId,
                AlumnoId = alumnoId
            });
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = await client.ExecuteAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
