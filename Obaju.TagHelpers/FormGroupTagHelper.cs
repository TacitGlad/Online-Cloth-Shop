using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Obaju.TagHelpers
{
    [HtmlTargetElement("form-group")]
    public class FormGroupTagHelper : TagHelper
    {
        private readonly IHtmlGenerator _htmlGenerator;

        public FormGroupTagHelper(IHtmlGenerator htmlGenerator)
        {
            _htmlGenerator = htmlGenerator;
        }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "form-group");

            var label = await GenerateLabel(context);
            output.Content.AppendHtml(label);

            var textBox = await GenerateTextBox(context);
            output.Content.AppendHtml(textBox);

            var validationMessage = await GenerateValidationMessage(context);
            output.Content.AppendHtml(validationMessage);
        }

        private async Task<TagHelperOutput> GenerateLabel(TagHelperContext context)
        {
            var labelTagHelper =
                new LabelTagHelper(_htmlGenerator)
                {
                    For = this.For,
                    ViewContext = this.ViewContext,
                };

            var tagHelperOutput = CreateTagHelperOutput("label");

            await labelTagHelper.ProcessAsync(context, tagHelperOutput);

            return tagHelperOutput;
        }

        private async Task<TagHelperOutput> GenerateTextBox(TagHelperContext context)
        {
            var inputTagHelper =
               new InputTagHelper(_htmlGenerator)
               {
                   For = this.For,
                   ViewContext = this.ViewContext,
               };

            var tagHelperOutput = CreateTagHelperOutput("input");
            tagHelperOutput.Attributes.Add(new TagHelperAttribute("class", "form-control"));

            await inputTagHelper.ProcessAsync(context, tagHelperOutput);

            return tagHelperOutput;
        }

        private async Task<TagHelperOutput> GenerateValidationMessage(TagHelperContext context)
        {
            var validationMessageTagHelper =
               new ValidationMessageTagHelper(_htmlGenerator)
               {
                   For = this.For,
                   ViewContext = this.ViewContext,
               };

            var inputOutput = CreateTagHelperOutput("span");
            inputOutput.Attributes.Add(new TagHelperAttribute("class", "text-danger"));

            await validationMessageTagHelper.ProcessAsync(context, inputOutput);

            return inputOutput;
        }

        private TagHelperOutput CreateTagHelperOutput(string tagName)
        {
            return new TagHelperOutput(
                tagName: tagName,
                attributes: new TagHelperAttributeList(),
                getChildContentAsync: (s, t) =>
                {
                    return Task.Factory.StartNew<TagHelperContent>(
                            () => new DefaultTagHelperContent());
                }
            );
        }
    }
}
