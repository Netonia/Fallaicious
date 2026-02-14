using Fluid;
using Fallaicious.App.Models;

namespace Fallaicious.App.Services;

public class TemplateService
{
    private static readonly FluidParser Parser = new();

    public string DefaultTemplate { get; }

    public TemplateService(string defaultTemplate)
    {
        DefaultTemplate = defaultTemplate;
    }

    public string Render(string templateText, AnalysisRequest request)
    {
        if (!Parser.TryParse(templateText, out var template, out var error))
        {
            return $"Erreur de template : {error}";
        }

        var inputType = request.Input.Type == InputType.Conversation
            ? "conversation entre deux personnes"
            : "discours individuel";

        var ctx = request.Context;
        var hasContext = !string.IsNullOrWhiteSpace(ctx.Subject)
            || !string.IsNullOrWhiteSpace(ctx.Location)
            || !string.IsNullOrWhiteSpace(ctx.Date)
            || !string.IsNullOrWhiteSpace(ctx.PersonAInfo)
            || !string.IsNullOrWhiteSpace(ctx.PersonBInfo)
            || !string.IsNullOrWhiteSpace(ctx.Relationship)
            || !string.IsNullOrWhiteSpace(ctx.OtherInfo);

        var context = new TemplateContext();
        context.SetValue("input_type", inputType);
        context.SetValue("text", request.Input.Text);
        context.SetValue("has_context", hasContext);
        context.SetValue("subject", ctx.Subject ?? "");
        context.SetValue("location", ctx.Location ?? "");
        context.SetValue("date", ctx.Date ?? "");
        context.SetValue("person_a", ctx.PersonAInfo ?? "");
        context.SetValue("person_b", ctx.PersonBInfo ?? "");
        context.SetValue("relationship", ctx.Relationship ?? "");
        context.SetValue("other_info", ctx.OtherInfo ?? "");

        return template.Render(context);
    }
}
