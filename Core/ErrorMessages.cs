namespace Core
{
    public class ErrorMessages
    {
        public const string TournamentHasFinished = "Tournament has already finished";
        public const string NotAllMatchesAreFinished = "Tournament can not be finished, because not all matches are finished";
        public const string OrganizerOnly = "Only the tournament organizer is allowed to perform this action";
        public const string NoAvailableSlots = "No available slots in the tournament";
        public const string RestrictedAccess = "The access to this tournament is restricted";
        public const string OnlyClassicCanProceed = "Only classic tournament can proceed to eliminations";
        public const string ActionAlreadyExecuted = "Action has already been executed";

        public const string TeamDoesNotExist = "Team does not exist";
    }
}
