using System.Net.Http;
using System.Windows.Forms;
using VotingApp.Core.Models;
using VotingApp.Core.Repositories;
using VotingApp.Core.Services;

namespace VotingApp.Admin.Forms
{
    public partial class VotingItemDetailForm : Form
    {
        private readonly VotingItemRepository _votingItems;
        private readonly VotingItem _item;

        public VotingItemDetailForm( VotingItem item )
        {
            InitializeComponent();

            _item = item;
            HttpClient client = DependencyService.Resolve<HttpClient>();
            _votingItems = new VotingItemRepository( client );
        }

        private void VotingItemDetailForm_Load(object sender, System.EventArgs e)
        {
            labelID.Text = _item.Id;
            labelName.Text = _item.Name;
            labelDescription.Text = _item.Description;
            labelCreatedDate.Text = _item.CreatedDate.ToShortDateString();
            labelVotersCount.Text = _item.VotersCount.ToString();
            labelDueDate.Text = _item.DueDate.ToShortDateString();
            labelCategories.Text = _item.Categories;
        }

        private async void buttonDelete_Click(object sender, System.EventArgs e)
        {
            RepositoryResponse response = await _votingItems.RemoveAsync( _item.Id );
            if( !response.Success )
            {
                MessageBox.Show( $@"Delete error. Code: {response.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonUpdate_Click(object sender, System.EventArgs e)
        {
            UpdateVotingItemForm form = new UpdateVotingItemForm( _item );
            DialogResult = form.ShowDialog();
            Close();
        }
    }
}