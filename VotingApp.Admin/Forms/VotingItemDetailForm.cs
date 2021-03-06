using System.Windows.Forms;
using VotingApp.Core.Models;

namespace VotingApp.Admin.Forms
{
    public partial class VotingItemDetailForm : Form
    {
        private readonly VotingItem _item;

        public VotingItemDetailForm( VotingItem item )
        {
            _item = item;
            InitializeComponent();
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
    }
}