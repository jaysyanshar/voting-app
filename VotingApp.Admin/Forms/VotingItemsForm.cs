using System.Net.Http;
using System.Windows.Forms;
using VotingApp.Admin.Repositories;
using VotingApp.Core.Models;
using VotingApp.Core.Services;

namespace VotingApp.Admin.Forms
{
    public partial class VotingItemsForm : Form
    {
        private readonly SessionRepository _session;
        public VotingItemsForm()
        {
            InitializeComponent();

            HttpClient client = DependencyService.Resolve<HttpClient>();
            _session = new SessionRepository( client );
        }

        private async void buttonLogout_Click(object sender, System.EventArgs e)
        {
            RepositoryResponse response = await _session.Logout();
            if( !response.Success )
            {
                MessageBox.Show( $@"Logout error. Code: {response.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );
                return;
            }

            Hide();

            LoginForm loginForm = new LoginForm();
            loginForm.Closed += ( s, args ) => Close();
            loginForm.Show();
        }
    }
}
