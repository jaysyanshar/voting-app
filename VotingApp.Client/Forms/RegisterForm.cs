using System;
using System.Net.Http;
using System.Windows.Forms;
using VotingApp.Core.Models;
using VotingApp.Core.Repositories;
using VotingApp.Core.Services;

namespace VotingApp.Client.Forms
{
    public partial class RegisterForm : Form
    {
        private readonly ClientRepository _clients;

        public RegisterForm()
        {
            InitializeComponent();

            HttpClient client = DependencyService.Resolve<HttpClient>();
            _clients = new ClientRepository( client );
        }

        private async void buttonRegister_Click( object sender, EventArgs e )
        {
            if( !ValidateInputs( out Core.Models.Client client ) )
            {
                MessageBox.Show( $@"Invalid data. Please check your input again.", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );

                return;
            }

            RepositoryResponse<Core.Models.Client> response = await _clients.Register( client );
            if( !response.Success )
            {
                MessageBox.Show( $@"Register error. Code: {response.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );

                return;
            }

            DialogResult = DialogResult.OK;
        }

        private bool ValidateInputs( out Core.Models.Client validClient )
        {
            if( !textBoxPassword.Text.Equals( textBoxConfirmPassword.Text ) )
            {
                validClient = null;
                return false;
            }

            Core.Models.Client client = new Core.Models.Client
            {
                Email = textBoxEmail.Text,
                Password = textBoxPassword.Text,
                FirstName = textBoxFirstName.Text,
                LastName = textBoxLastName.Text,
                Age = int.Parse( textBoxAge.Text ),
                Gender = radioButtonMale.Checked ? Gender.Male : Gender.Female
            };

            if( client.ValidateFields() )
            {
                validClient = client;
                return true;
            }

            validClient = null;
            return false;
        }

        private void ForceInputNumberOnly(object sender, KeyPressEventArgs e)
        {
            if( !char.IsControl( e.KeyChar ) && !char.IsDigit( e.KeyChar ) )
            {
                e.Handled = true;
            }
        }
    }
}
