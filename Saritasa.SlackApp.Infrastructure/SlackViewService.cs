using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack;
using SlackNet;
using SlackNet.Blocks;
using Option = SlackNet.Blocks.Option;

namespace Saritasa.SlackApp.Infrastructure;

/// <inheritdoc />
public class SlackViewService : ISlackViewService
{
    /// <inheritdoc />
    public HomeViewDefinition GetHomePage()
    {
        return new HomeViewDefinition
        {
            Blocks =
            {
                // Command selection header .
                new HeaderBlock { Text = new PlainText("Select a command") },

                // Command selection button.
                new ActionsBlock()
                {
                    BlockId = "select_command",
                    Elements = new List<IActionElement>
                    {
                        new StaticSelectMenu
                        {
                            ActionId = "select_command",
                            Placeholder = new PlainText("Select command"),
                            Options = new List<Option>
                            {
                                new() { Text = new PlainText("Check jobs"), Value = "CheckJobs" },
                                new() { Text = new PlainText("Planer"), Value = "Planer" }
                            }
                        }
                    }
                },

                // Time selection header.
                new HeaderBlock { Text = new PlainText("Set specific time 🕰") },

                // Additional text.
                new SectionBlock { Text = new PlainText("What time to notify you?") },

                // Time selection button.
                new ActionsBlock
                {
                    BlockId = "timepicker",
                    Elements = new List<IActionElement>
                    {
                        new TimePicker
                        {
                            InitialTime = new TimeSpan(13, 35, 0),
                            ActionId = "timepicker",
                            Placeholder = new PlainText { Text = "Select time", Emoji = true }
                        }
                    }
                },

                // Notification button every day.
                new ActionsBlock
                {
                    Elements = new List<IActionElement>
                    {
                        new Button
                        {
                            Text = new PlainText { Text = "Every day", Emoji = true },
                            Style = ButtonStyle.Primary,
                            Value = "click_daily",
                            ActionId = "DailyButton"
                        }
                    }
                },

                // Header.
                new HeaderBlock { Text = new PlainText("ㅤOR") },

                // Week day selection header.
                new HeaderBlock { Text = new PlainText("Set specific dates 📆") },

                // Additional text.
                new SectionBlock { Text = new PlainText("From what date to what date to receive notifications?") },

                // Button for selecting days of the week.
                new ActionsBlock
                {
                    BlockId = "checkboxes_weekdays",
                    Elements = new List<IActionElement>
                    {
                        new CheckboxGroup
                        {
                            Options = new List<Option>
                            {
                                new() { Text = new Markdown("*Monday*"), Value = "Monday" },
                                new() { Text = new Markdown("*Tuesday*"), Value = "Tuesday" },
                                new() { Text = new Markdown("*Wednesday*"), Value = "Wednesday" },
                                new() { Text = new Markdown("*Thursday*"), Value = "Thursday" },
                                new() { Text = new Markdown("*Friday*"), Value = "Friday" },
                                new() { Text = new Markdown("*Saturday*"), Value = "Saturday" },
                                new() { Text = new Markdown("*Sunday*"), Value = "Sunday" },
                            },
                            ActionId = "checkboxes_weekdays"
                        }
                    }
                },

                // Notification button on certain days of the week.
                new ActionsBlock
                {
                    Elements = new List<IActionElement>
                    {
                        new Button
                        {
                            Text = new PlainText { Text = "Specific days", Emoji = true },
                            Style = ButtonStyle.Primary,
                            Value = "click_specific_days",
                            ActionId = "SpecificDaysButton"
                        }
                    }
                },

                // Divider block.
                new DividerBlock(),

                // Unsubscribe button.
                new SectionBlock
                {
                    Text = new PlainText("Unsubscribe from notifications?"),
                    Accessory = new Button
                    {
                        Text = new PlainText { Text = "Unsubscribe", Emoji = true },
                        Style = ButtonStyle.Danger,
                        Value = "click_unsubscribe",
                        ActionId = "UnsubscribeButton"
                    }
                }
            }
        };
    }
}
