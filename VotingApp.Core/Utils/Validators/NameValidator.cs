﻿using System.Text.RegularExpressions;

namespace VotingApp.Core.Utils.Validators
{
    public class NameValidator : IValidator<string>
    {

        public bool Validate( string data )
        {
            if( string.IsNullOrWhiteSpace( data ) )
                return false;

            Regex name = new Regex( @"^[A-Z][a-zA-Z]*$" );
            return name.IsMatch( data );
        }
    }
}