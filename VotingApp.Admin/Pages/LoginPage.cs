using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VotingApp.Core.Models;
using VotingApp.Core.Utils.Helpers;

namespace VotingApp.Admin.Pages
{
    public class LoginPage : PageBase
    {
        private const string LOGIN_URI = @"https://localhost:44303/api/Sessions/Login";
        public Login LoginInfo = new Login();

        public LoginPage()
        {
            Title = "Admin Login";
        }

        public override void Render()
        {
            base.Render();
            bool isValidEmail = false;
            while( !isValidEmail )
            {
                Console.Write( "Email: " );
                string email = Console.ReadLine();
                isValidEmail = ValidationHelper.ValidateEmail( email );
                if( isValidEmail )
                    LoginInfo.Email = email;
            }

            bool isValidPassword = false;
            while( !isValidPassword )
            {
                Console.Write("Password: ");
                string password = Console.ReadLine();
                isValidPassword = ValidationHelper.ValidatePassword( password );
                if( isValidPassword )
                    LoginInfo.Password = password;
            }

            LoginInfo.UserRole = UserRole.Admin;

            Console.WriteLine("Email and Password validated!");
        }

        public async Task<Session> TryLogin( HttpClient client )
        {
            HttpContent content = new StringContent( JsonHelper.Serialize( LoginInfo ) );
            content.Headers.ContentType = new MediaTypeHeaderValue( "application/json" );
            
            try
            {
                HttpResponseMessage response = await client.PostAsync( LOGIN_URI, content );
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                return JsonHelper.Deserialize<Session>( body );
            }
            catch( HttpRequestException e )
            {
                RenderError( e );
                return null;
            }
        }
    }
}