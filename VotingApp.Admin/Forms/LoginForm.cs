using System;
using System.ComponentModel;
using System.Net.Http;
using System.Windows.Forms;
using VotingApp.Admin.Repositories;
using VotingApp.Core.Models;
using VotingApp.Core.Services;
using VotingApp.Core.Utils.Helpers;

namespace VotingApp.Admin.Forms
{
    public partial class LoginForm : Form
    {
        private readonly SessionRepository _session;
        private bool _isValidEmail;
        private bool _isValidPassword;

        public LoginForm()
        {
            InitializeComponent();
            buttonLogin.Enabled = _isValidEmail && _isValidPassword;

            HttpClient client = DependencyService.Resolve<HttpClient>();
            _session = new SessionRepository( client );

            // test only
            textBoxEmail.Text = @"admin@voting.app";
            textBoxPassword.Text = @"Admin123";
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
            RepositoryResponse<Session> login = await _session.Login( textBoxEmail.Text, textBoxPassword.Text, UserRole.Type.Admin );
            if( !login.Success )
            {
                MessageBox.Show( $@"Login error. Code: {login.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );
                return;
            }

            Hide();

            VotingItemsForm itemsForm = new VotingItemsForm();
            itemsForm.Closed += ( s, args ) => Close();
            itemsForm.Show();
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