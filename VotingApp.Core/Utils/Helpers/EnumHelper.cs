﻿namespace VotingApp.Core.Utils.Helpers
{
    public static class EnumHelper
    {
        public static T ParseEnum<T>( this string value )
        {
            return ( T ) Enum.Parse( typeof( T ), value, true );
        }
    }
}