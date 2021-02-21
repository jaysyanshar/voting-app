using System;

namespace VotingApp.Admin.Pages
{
    public abstract class PageBase
    {
        protected string Title { get; set; }

        public virtual void Render()
        {
            Console.WriteLine( Title );
        }

        public virtual void RenderError( Exception e )
        {
            Console.WriteLine( "\nException Caught!" );
            Console.WriteLine( "Message :{0} ", e.Message );
        }
    }
}