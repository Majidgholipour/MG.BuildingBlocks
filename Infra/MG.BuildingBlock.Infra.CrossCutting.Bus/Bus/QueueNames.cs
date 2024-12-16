using System.Text.RegularExpressions;

namespace MG.BuildingBlock.Infra.CrossCutting.Bus.Bus;

public class QueueNames
{
    private const string RabbitUri = "queue:";
    public static Uri GetMessageUri(string key)
    {
        return new Uri(RabbitUri + key.PascalToKebabCaseMessage());
    }
    public static Uri GetActivityUri(string key)
    {
        var kebabCase = key.PascalToKebabCaseActivity();
        if (kebabCase.EndsWith('-'))
        {
            kebabCase = kebabCase.Remove(kebabCase.Length - 1);
        }
        return new Uri(RabbitUri + kebabCase + '_' + "execute");
    }
}

public static class StringExtensions
{
    public static string PascalToKebabCaseMessage(this string value)
    {
        return PascalToKebabCase(value, "message");
    }
    public static string PascalToKebabCaseActivity(this string value)
    {
        return PascalToKebabCase(value, "activity");
    }
    private static string PascalToKebabCase(string value, string postfix)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        var result = Regex.Replace(
                value,
                "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])",
                "-$1",
                RegexOptions.Compiled)
            .Trim()
            .ToLower();

        var segments = result.Split('-');
        if (segments[segments.Length - 1] != postfix)
        {
            return result;
        }

        return result.Substring(0, result.Length - 8);
    }
}
