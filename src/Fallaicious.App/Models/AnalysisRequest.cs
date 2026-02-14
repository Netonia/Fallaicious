namespace Fallaicious.App.Models;

public class AnalysisRequest
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Title { get; set; } = string.Empty;
    public InputModel Input { get; set; } = new();
    public ContextModel Context { get; set; } = new();
    public string Template { get; set; } = string.Empty;
    public string GeneratedPrompt { get; set; } = string.Empty;
}
