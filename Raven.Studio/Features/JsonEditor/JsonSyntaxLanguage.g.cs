namespace Raven.Studio.Features.JsonEditor
{
    using ActiproSoftware.Text;
    using ActiproSoftware.Text.Implementation;
    using ActiproSoftware.Text.Lexing;

    /// <summary>
    /// Represents a <c>Json</c> syntax language definition.
    /// </summary>
    /// <remarks>
    /// This type was generated by the Actipro Language Designer tool v12.1.561.0 (http://www.actiprosoftware.com).
    /// </remarks>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("LanguageDesigner", "12.1.561.0")]
    public partial class JsonSyntaxLanguage : SyntaxLanguage
    {

        /// <summary>
        /// Initializes a new instance of the <c>JsonSyntaxLanguage</c> class.
        /// </summary>
        public JsonSyntaxLanguage() : base("Json")
        {

            // Create a classification type provider and register its classification types
            var classificationTypeProvider = new JsonClassificationTypeProvider();
            classificationTypeProvider.RegisterAll();

            // Register an ILexer service that can tokenize text
            RegisterService<ILexer>(new JsonLexer(classificationTypeProvider));

            // Register an ICodeDocumentTaggerProvider service that creates a token tagger for
            //   each document using the language
            RegisterService(new JsonTokenTaggerProvider(classificationTypeProvider));

            // Register an IExampleTextProvider service that provides example text
            RegisterService<IExampleTextProvider>(new JsonExampleTextProvider());
        }
    }
}