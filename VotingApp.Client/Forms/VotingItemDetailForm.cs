using System;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using VotingApp.Core.Models;
using VotingApp.Core.Repositories;
using VotingApp.Core.Services;

namespace VotingApp.Client.Forms
{
    public partial class VotingItemDetailForm : Form
    {
        private const string VOTED = "Voted";
        private const string EXPIRED = "Expired";

        private readonly VotingItem _item;
        private readonly bool _login;
        private readonly string _userEmail;

        private readonly VoteRepository _votes;
        private DialogResult _currentResult;

        public VotingItemDetailForm( VotingItem item, bool login, string userEmail )
        {
            _item = item;
            _login = login;
            _userEmail = userEmail;
            InitializeComponent();
            buttonVote.Enabled = login;

            HttpClient client = DependencyService.Resolve<HttpClient>();
            _votes = new VoteRepository( client );
        }

        private void VotingItemDetailForm_Load(object sender, EventArgs e)
        {
            InitializeLabels();
            if( !_login )
                return;

            CheckVoteStatus();
        }

        private void InitializeLabels()
        {
            labelID.Text = _item.Id;
            labelName.Text = _item.Name;
            labelDescription.Text = _item.Description;
            labelCreatedDate.Text = _item.CreatedDate.ToShortDateString();
            labelVotersCount.Text = _item.VotersCount.ToString();
            labelDueDate.Text = _item.DueDate.ToShortDateString();
            labelCategories.Text = _item.Categories;
        }

        private async void CheckVoteStatus()
        {
            if( _item.DueDate < DateTime.Now )
            {
                buttonVote.Enabled = false;
                buttonVote.Text = EXPIRED;
                return;
            }

            RepositoryResponse<bool> response = await _votes.CanVote( _userEmail, _item.Id );
            if( !response.Success && response.StatusCode != HttpStatusCode.NotFound )
            {
                MessageBox.Show( $@"Check status error. Code: {response.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );

                buttonVote.Enabled = false;
                return;
            }

            if( response.Content )
                return;

            buttonVote.Text = VOTED;
            buttonVote.Enabled = false;
        }

        private async void buttonVote_Click( object sender, EventArgs e )
        {
            if( !_login )
                return;

            RepositoryResponse<VoteRecord> response = await _votes.Vote( _item.Id );
            if( !response.Success )
            {
                MessageBox.Show( $@"Vote error. Code: {response.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );

                return;
            }

            if( !response.Content.UserEmail.Equals( _userEmail ) || !response.Content.VotingItemId.Equals( _item.Id ) )
                return;

            _item.VotersCount++;
            labelVotersCount.Text = _item.VotersCount.ToString();

            _currentResult = DialogResult.OK;
            CheckVoteStatus();
        }

        private void VotingItemDetailForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if( DialogResult != DialogResult.OK && _currentResult == DialogResult.OK )
                DialogResult = _currentResult;
        }
    }
}