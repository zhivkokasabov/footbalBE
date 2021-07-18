using Core.contracts.response;
using Core.contracts.Response;
using System.Collections.Generic;

namespace Core.InternalObjects
{
    public class CloseTournamentModel
    {
        public TournamentOutputDto Tournament { get; set; }
        public List<ErrorModel> Errors { get; set; }
    }
}
