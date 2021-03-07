using System;
using System.ComponentModel;
using System.Net.Http;
using System.Windows.Forms;
using VotingApp.Core.Models;
using VotingApp.Core.Repositories;
using VotingApp.Core.Services;
using VotingApp.Core.Utils.Helpers;

namespace VotingApp.Client.Forms
{
    public partial class LoginForm : Form
    {
        private readonly SessionRepository _session;
        private bool _isValidEmail;
        private bool _isValidPassword;

        public string UserEmail { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            buttonLogin.Enabled = _isValidEmail && _isValidPassword;

            HttpClient client = DependencyService.Resolve<HttpClient>();
            _session = new SessionRepository( client );

            // test only
            textBoxEmail.Text = @"anshar@voting.app";
            textBoxPassword.Text = @"Jaysy123";
        }

        private void TryEnableLoginButton()
        {
            if( _isValidEmail && _isValidPassword )
            {
                buttonLogin.Enabled = true;
            }
        }

        private async void buttonLogin_Click( object sender, EventArgs e )
        {
            string email = textBoxEmail.Text;
            string password = textBoxPassword.Text;
            RepositoryResponse<Session> login = await _session.Login( email, password, UserRole.Type.Client );
            if( !login.Success )
            {
                MessageBox.Show( $@"Login error. Code: {login.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );
                return;
            }

            UserEmail = email;
            DialogResult = DialogResult.OK;
        }

        private void textBoxEmail_Validating( object sender, CancelEventArgs e )
        {
            if( !ValidationHelper.ValidateEmail( textBoxEmail.Text ) )
            {
                e.Cancel = true;
                textBoxEmail.Focus();
                _isValidEmail = false;
            }
            else
            {
                e.Cancel = false;
                _isValidEmail = true;
                TryEnableLoginButton();
            }
        }

        private void textBoxPassword_Validating( object sender, CancelEventArgs e )
        {
            if( !ValidationHelper.ValidatePassword( textBoxPassword.Text ) )
            {
                e.Cancel = true;
                textBoxPassword.Focus();
                _isValidPassword = false;
            }
            else
            {
                e.Cancel = false;
                _isValidPassword = true;
                TryEnableLoginButton();
            }
        }

        private void LoginForm_FormClosing( object sender, FormClosingEventArgs e )
        {
            if( AutoValidate == AutoValidate.Disable )
                return;

            AutoValidate = AutoValidate.Disable;
            Close();
        }
    }
}