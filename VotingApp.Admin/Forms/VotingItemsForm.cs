using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;
using VotingApp.Admin.Models;
using VotingApp.Admin.Repositories;
using VotingApp.Core.Models;
using VotingApp.Core.Services;

namespace VotingApp.Admin.Forms
{
    public partial class VotingItemsForm : Form
    {
        private readonly SessionRepository _session;
        private readonly VotingItemRepository _votingItems;
        private readonly CategoryRepository _categories;

        private const int TAKE = 10;
        private  const int SKIP = 10;
        private int _currentPage;

        public VotingItemsForm()
        {
            InitializeComponent();

            comboBoxFilter.DropDownStyle = ComboBoxStyle.DropDownList;

            HttpClient client = DependencyService.Resolve<HttpClient>();
            _session = new SessionRepository( client );
            _votingItems = new VotingItemRepository( client );
            _categories = new CategoryRepository( client );
        }

        private async void LoadCategories()
        {
            RepositoryResponse<IEnumerable<Category>> response = await _categories.GetManyAsync();
            if( !response.Success )
            {
                MessageBox.Show( $@"Load categories error. Code: {response.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );

                return;
            }

            comboBoxFilter.Items.Clear();
            comboBoxFilter.Items.Add( "No Filter" );
            foreach( Category category in response.Content )
            {
                CategoryItem categoryItem = new CategoryItem
                {
                    Id = category.Id,
                    Name = category.Name
                };

                comboBoxFilter.Items.Add( categoryItem );
            }

            comboBoxFilter.SelectedIndex = 0;
        }

        private async void LoadItemsByPage()
        {
            IDictionary<string, string> query = new Dictionary<string, string>();
            query.Add( VotingItemRepository.SKIP_QUERY, _currentPage.ToString() );
            query.Add( VotingItemRepository.TAKE_QUERY, TAKE.ToString() );

            RepositoryResponse<IEnumerable<VotingItem>> response = await _votingItems.GetManyAsync( query );
            if( !response.Success )
            {
                MessageBox.Show( $@"Loading data error. Code: {response.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );

                return;
            }

            GenerateListView( response.Content );
        }

        private async void LoadItemsBySearch()
        {
            RepositoryResponse<IEnumerable<VotingItem>> response = await _votingItems.SearchAsync( textBoxSearch.Text );
            if( !response.Success )
            {
                MessageBox.Show( $@" Search error. Code: {response.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );

                return;
            }

            GenerateListView( response.Content );
        }

        private async void LoadItemsByFilter()
        {
            RepositoryResponse<IEnumerable<VotingItem>>
                response = await _votingItems.FilterAsync( comboBoxFilter.Text );

            if( !response.Success )
            {
                MessageBox.Show( $@" Filter error. Code: {response.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );

                return;
            }

            GenerateListView( response.Content );
        }

        private void GenerateListView( IEnumerable<VotingItem> items )
        {
            listViewVotingItems.Items.Clear();
            foreach( VotingItem item in items )
            {
                string[] row =
                    { item.Name, item.Categories, item.DueDate.ToString( "MM/dd/yyyy" ), item.VotersCount.ToString() };

                ListViewItem lvi = new ListViewItem( row )
                {
                    Tag = item
                };

                listViewVotingItems.Items.Add( lvi );
            }
        }

        private void VotingItemsForm_Load( object sender, EventArgs e )
        {
            LoadCategories();
            LoadItemsByPage();
        }

        private async void buttonLogout_Click( object sender, EventArgs e )
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

        private void buttonAdd_Click( object sender, EventArgs e )
        {
            AddVotingItemForm addItemForm = new AddVotingItemForm();
            DialogResult dialogResult = addItemForm.ShowDialog();
            if( dialogResult != DialogResult.OK )
                return;

            _currentPage = 0;
            LoadItemsByPage();
        }

        private void buttonCategories_Click( object sender, EventArgs e )
        {
            Hide();

            CategoriesForm form = new CategoriesForm();
            form.Closed += ( s, args ) => Close();
            form.Show();
        }

        private void listViewVotingItems_DoubleClick( object sender, EventArgs e )
        {
            VotingItem selected = ( VotingItem ) listViewVotingItems.SelectedItems[0].Tag;

            VotingItemDetailForm form = new VotingItemDetailForm( selected );
            DialogResult dialogResult = form.ShowDialog();
            if( dialogResult != DialogResult.OK )
                return;

            _currentPage = 0;
            LoadItemsByPage();
        }

        private void buttonNext_Click( object sender, EventArgs e )
        {
            _currentPage += SKIP;
            LoadItemsByPage();
        }

        private void buttonPrev_Click( object sender, EventArgs e )
        {
            if( _currentPage - SKIP < 0 )
                return;

            _currentPage -= SKIP;
            LoadItemsByPage();
        }

        private void buttonSearch_Click( object sender, EventArgs e )
        {
            if( string.IsNullOrWhiteSpace( textBoxSearch.Text ) )
                return;

            LoadItemsBySearch();
        }

        private void buttonFilter_Click( object sender, EventArgs e )
        {
            if( string.IsNullOrWhiteSpace( comboBoxFilter.Text ) )
                return;

            if( comboBoxFilter.SelectedIndex == 0 )
            {
                LoadItemsByPage();
                return;
            }

            LoadItemsByFilter();
        }

    }
}