using Core.Enums;
using Core.Models;
using System;
using System.Collections.Generic;

namespace Services.Tournaments.Factories
{
    public class Base
    {

        protected void SetMatchStart(Tournament tournament, List<TournamentMatch> matches, DateTime startDate, bool startFromNextSlot)
        {
            var cycleTeams = new HashSet<int>();
            var date = startDate;
            // For scenarios where not all tournament matches are calculated at once but in parts (Classical tournament)
            // Allow skipping current cycle to avoid conflicts.
            var cycleCount = startFromNextSlot ? tournament.PlayingFields + 1 : 1;

            matches.ForEach(match =>
            {
                // if no available field for the current time slots
                // or if team conflict go to the next time slot
                if (cycleCount > tournament.PlayingFields ||
                    cycleTeams.Contains(match.AwayTeamSequenceId) ||
                    cycleTeams.Contains(match.HomeTeamSequenceId)
                )
                {
                    cycleTeams = new HashSet<int>();
                    cycleCount = 1;

                    // give 15 minutes between time slot. Can be configurable at some point but.
                    date += new TimeSpan(0, 15, 0);

                    if (date.Hour * 60 + date.Minute < tournament.LastMatchStartsAt.Hour * 60 + tournament.LastMatchStartsAt.Minute)
                    {
                        date += tournament.MatchLength;
                        date += tournament.HalfTimeLength;
                    }
                    else
                    {
                        date = GetNextDate(tournament, date);
                    }
                }

                match.StartTime = date;
                cycleCount++;
                cycleTeams.Add(match.HomeTeamSequenceId);
                cycleTeams.Add(match.AwayTeamSequenceId);
            });
        }

        protected DateTime GetNextDate(Tournament tournament, DateTime currentDate)
        {
            var nextDate = currentDate;

            switch (tournament.PlayingDaysId)
            {
                case (int)PlayingDaysEnum.WeekEnd:
                    if ((int)currentDate.DayOfWeek == 0)
                    {
                        nextDate = CreateDate(
                            currentDate,
                            currentDate.Day,
                            tournament.FirstMatchStartAt.Hour,
                            tournament.FirstMatchStartAt.Minute
                        );

                        nextDate = nextDate.AddDays(6);
                    }
                    else if ((int)currentDate.DayOfWeek == 6)
                    {
                        nextDate = CreateDate(
                            currentDate,
                            currentDate.Day,
                            tournament.FirstMatchStartAt.Hour,
                            tournament.FirstMatchStartAt.Minute
                        );

                        nextDate = nextDate.AddDays(1);
                    }
                    break;
                case (int)PlayingDaysEnum.WorkDays:
                    if ((int)currentDate.DayOfWeek == 5)
                    {
                        nextDate = CreateDate(
                            currentDate,
                            currentDate.Day,
                            tournament.FirstMatchStartAt.Hour,
                            tournament.FirstMatchStartAt.Minute
                        );

                        nextDate = nextDate.AddDays(3);
                    }
                    else
                    {
                        nextDate = CreateDate(
                            currentDate,
                            currentDate.Day,
                            tournament.FirstMatchStartAt.Hour,
                            tournament.FirstMatchStartAt.Minute
                        );

                        nextDate = nextDate.AddDays(1);
                    }

                    break;
                default:
                    nextDate = CreateDate(
                        currentDate,
                        currentDate.Day,
                        tournament.FirstMatchStartAt.Hour,
                        tournament.FirstMatchStartAt.Minute
                    );

                    nextDate = nextDate.AddDays(1);

                    break;
            }

            return nextDate;
        }

        protected DateTime CreateDate(DateTime date, int day, int hour, int minute)
        {
            return new DateTime(
                date.Year,
                date.Month,
                day,
                hour,
                minute,
                0
            );
        }
    }
}
