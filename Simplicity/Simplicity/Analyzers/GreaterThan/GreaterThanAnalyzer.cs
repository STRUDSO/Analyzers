// Documentation: https://github.com/STRUDSO/Analyzers/blob/main/docs/Rules/SIMP1001.md

using System.Collections.Immutable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Simplicity;

/// <summary>
/// Analyzer that detects usage of the greater than sign ('>') in C# code.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class GreaterThanAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "SIMP1001";
    private static readonly LocalizableString Title = "Greater than sign usage detected";
    private static readonly LocalizableString MessageFormat = "Usage of '>' is not allowed";

    private static readonly LocalizableString Description =
        "Detects all usages of the greater than sign ('>') in C# code.";

    private const string Category = "Syntax";

    private static readonly DiagnosticDescriptor Rule = new(
        DiagnosticId,
        Title,
        MessageFormat,
        Category,
        DiagnosticSeverity.Warning,
        true,
        Description,
        RuleIdentifiers.GetHelpUri(RuleIdentifiers.GreaterThan)
    );

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.GreaterThanExpression);
    }

    private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        BinaryExpressionSyntax node = (BinaryExpressionSyntax)context.Node;
        SyntaxToken operatorToken = node.OperatorToken;
        Diagnostic diagnostic = Diagnostic.Create(Rule, operatorToken.GetLocation());
        context.ReportDiagnostic(diagnostic);
    }
}