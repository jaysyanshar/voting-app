using Microsoft.EntityFrameworkCore;
using VotingApp.Api.Models;

namespace VotingApp.Api.DataContexts
{
    public class AdminDataContext : DataContextBase<Admin, AdminDataContext>
    {

        public AdminDataContext( DbContextOptions<AdminDataContext> options ) : base( options )
        {
        }
    }
}