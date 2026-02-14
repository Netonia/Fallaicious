namespace Fallaicious.App.Models;

public enum InputType
{
    Individual,
    Conversation
}

public class InputModel
{
    public InputType Type { get; set; } = InputType.Individual;
    public string Text { get; set; } = string.Empty;
}
