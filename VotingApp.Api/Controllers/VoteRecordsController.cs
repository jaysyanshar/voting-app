﻿using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Api.DataContexts;
using VotingApp.Api.Models;

namespace VotingApp.Api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class VoteRecordsController : ApiControllerBase<long, VoteRecord, VoteRecordDataContext>
    {
        private readonly SessionDataContext _sessionContext;
        private readonly VotingItemDataContext _votingItemContext;

        public VoteRecordsController( VoteRecordDataContext context, SessionDataContext sessionContext,
            VotingItemDataContext votingItemContext ) :
            base( context )
        {
            _sessionContext = sessionContext;
            _votingItemContext = votingItemContext;
        }

        // TODO: B-3
        [HttpPost( "Vote/{itemId}" )]
        public async Task<ActionResult<VoteRecord>> Post( long itemId )
        {
            // TODO: B-5
            StatusCodeResult userValid = await ValidateUser( UserRole.Type.Client );
            if( !( userValid is OkResult ) )
                return userValid;

            VotingItem votingItem = await _votingItemContext.DataSet.FindAsync( itemId );
            if( votingItem == null )
                return NotFound();

            // TODO: B-4
            if( votingItem.DueDate < DateTime.Now )
                return ValidationProblem();

            Session session = await _sessionContext.DataSet.FindAsync( Request.Headers[nameof( Session )] );
            VoteRecord value = new()
            {
                UserEmail = session.UserEmail,
                VotingItemId = itemId
            };

            VoteRecord existingRecord = Context.DataSet.FirstOrDefault( vote =>
                vote.UserEmail == value.UserEmail && vote.VotingItemId == value.VotingItemId );

            if( existingRecord != null )
                return Conflict();

            return await base.Post( value );
        }

        public override async Task<ActionResult<VoteRecord>> Post( VoteRecord value ) => NotFound();

        public override async Task<IActionResult> Put( long id, VoteRecord value ) => NotFound();

        public override async Task<IActionResult> Delete( long id )
        {
            StatusCodeResult userValid = await ValidateUser( UserRole.Type.Client );
            if( !( userValid is OkResult ) )
                return userValid;

            Session session = await _sessionContext.DataSet.FindAsync( Request.Headers[nameof( Session )] );
            VoteRecord voteRecord = await Context.DataSet.FindAsync( id );
            if( voteRecord == null )
                return NotFound();

            if( !session.UserEmail.Equals( voteRecord.UserEmail ) )
                return Unauthorized();

            return await base.Delete( id );
        }

        private async Task<StatusCodeResult> ValidateUser( UserRole.Type role )
        {
            string sessionId = Request.Headers[nameof( Session )];
            if( string.IsNullOrWhiteSpace( sessionId ) )
                return BadRequest();

            StatusCodeResult sessionValid = await ValidateSession( sessionId, role, _sessionContext );
            return sessionValid;
        }
    }
}