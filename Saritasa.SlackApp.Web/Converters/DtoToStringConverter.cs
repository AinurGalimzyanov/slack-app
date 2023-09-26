using System.Globalization;
using System.Text;
using Saritasa.SlackApp.UseCases.Slack.GetNonBillableDays.Dto;
using Saritasa.SlackApp.UseCases.Slack.GetPlannerQuery;

namespace Saritasa.SlackApp.Web.Converters;

/// <summary>
/// Dto to string converter.
/// </summary>
public class DtoToStringConverter
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="nonBillableDays">Non billable days.</param>
    /// <returns>Dto converted to string.</returns>
    public string MapUserDaysToString(IReadOnlyCollection<UserDayDto> nonBillableDays)
    {
        if (nonBillableDays.Count == 0)
        {
            return "Hello! You have no missing jobs.";
        }

        var firstString = $"Hello! You have some missing jobs for the days:{Environment.NewLine}";
        var endString = $"{Environment.NewLine}Please fill the gaps at your earliest convenience.";

        var result = nonBillableDays
            .Select(day =>
            {
                var dayStr = day.Date.ToString("dddd, d MMMM", CultureInfo.InvariantCulture);
                return $"{dayStr} - {day.Duration:h\\h\\ m\\m} / {day.Capacity:h\\h\\ m\\m}";
            })
            .ToList();

        var jobsPerDay = string.Join(Environment.NewLine, result);

        return string.Join(Environment.NewLine, firstString, jobsPerDay, endString);
    }

    /// <summary>
    /// Converts dto to string.
    /// </summary>
    /// <param name="getPlannerQueryResultDto">Get planner query result dto.</param>
    /// <returns>Dto converted to string.</returns>
    public string ConvertToString(GetPlannerQueryResultDto getPlannerQueryResultDto)
    {
        var projects = getPlannerQueryResultDto.ProjectDtos;
        if (projects.Any() == false)
        {
            return "You are not assigned to any project yet";
        }

        const int loadLineBlocks = 10;
        var result = new StringBuilder();
        result.AppendLine("Hello! Your planner progress for this week:");
        foreach (var project in projects)
        {
            result.AppendLine(project.ProjectName);

            var spentBlocksCount = (int)(loadLineBlocks * GetTimeSpentPercents(project));
            var spentBlocks = string.Concat(Enumerable.Repeat('◼', spentBlocksCount).ToArray());

            result.Append('[');
            result.Append(spentBlocks);

            var unSpentBlocksCount = loadLineBlocks - spentBlocksCount;
            var unSpentBlocks = string.Concat(Enumerable.Repeat('◻', unSpentBlocksCount));

            result.Append(unSpentBlocks);
            result.Append(']');

            result.Append(GetFormatTimeSpan(project.TimeSpent));
            result.Append('/');

            result.AppendLine(GetFormatTimeSpan(project.AssignedTime));

            if (project.EditedAt is not null)
            {
                result.Append("(edited at: ");
                result.AppendLine($"{project.EditedAt:g})");
            }
        }

        return result.ToString();
    }

    private double GetTimeSpentPercents(ProjectDto projectDto)
    {
        const int oneHundredPercents = 1;
        if (projectDto.AssignedTime.Equals(TimeSpan.Zero))
        {
            return oneHundredPercents;
        }

        var spentMinutesPercents = projectDto.TimeSpent / projectDto.AssignedTime;

        return Math.Min(spentMinutesPercents, oneHundredPercents);
    }

    private string GetFormatTimeSpan(TimeSpan timeSpan)
    {
        if (timeSpan.Equals(TimeSpan.Zero))
        {
            return " 0h ";
        }

        var totalHours = (int)timeSpan.TotalHours;
        var formatHours = int.IsPositive(totalHours)
            ? $" {totalHours}h "
            : string.Empty;
        var formatMinutes = int.IsPositive(timeSpan.Minutes)
            ? $@"{timeSpan:m\m} "
            : string.Empty;

        return $"{formatHours}{formatMinutes}";
    }
}
