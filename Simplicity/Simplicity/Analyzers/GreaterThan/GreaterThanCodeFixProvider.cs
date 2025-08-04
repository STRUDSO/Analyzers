using System.Collections.Immutable;
using System.Composition;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;

namespace Simplicity;

/// <summary>
/// Code fix provider that flips operands and changes '>' to '<'.
/// </summary>
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(GreaterThanCodeFixProvider))]
[Shared]
public class GreaterThanCodeFixProvider : CodeFixProvider
{
    public sealed override ImmutableArray<string> FixableDiagnosticIds =>
        ImmutableArray.Create(GreaterThanAnalyzer.DiagnosticId);

    public sealed override FixAllProvider GetFixAllProvider()
    {
        return WellKnownFixAllProviders.BatchFixer;
    }

    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        SyntaxNode? root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
        Diagnostic diagnostic = context.Diagnostics[0];
        TextSpan diagnosticSpan = diagnostic.Location.SourceSpan;

        // Find the binary expression node flagged by the analyzer
        SyntaxNode node = root.FindNode(diagnosticSpan);
        if (node is not BinaryExpressionSyntax binaryExpr || binaryExpr.Kind() != SyntaxKind.GreaterThanExpression)
        {
            return;
        }

        context.RegisterCodeFix(
            CodeAction.Create(
                "Flip operands and use '<'",
                c => FlipGreaterThanAsync(context.Document, binaryExpr, c),
                "FlipGreaterThanToLessThan"),
            diagnostic);
    }

    private async Task<Document> FlipGreaterThanAsync(Document document, BinaryExpressionSyntax binaryExpr,
        CancellationToken cancellationToken)
    {
        // Swap leading/trailing trivia between operands
        ExpressionSyntax left = binaryExpr.Left;
        ExpressionSyntax right = binaryExpr.Right;

        // Save trivia
        SyntaxTriviaList leftLeading = left.GetLeadingTrivia();
        SyntaxTriviaList leftTrailing = left.GetTrailingTrivia();
        SyntaxTriviaList rightLeading = right.GetLeadingTrivia();
        SyntaxTriviaList rightTrailing = right.GetTrailingTrivia();

        // Swap trivia
        ExpressionSyntax newLeft = right.WithLeadingTrivia(leftLeading).WithTrailingTrivia(leftTrailing);
        ExpressionSyntax newRight = left.WithLeadingTrivia(rightLeading).WithTrailingTrivia(rightTrailing);

        SyntaxToken lessThanToken =
            SyntaxFactory.Token(SyntaxKind.LessThanToken).WithTriviaFrom(binaryExpr.OperatorToken);
        BinaryExpressionSyntax newExpr = SyntaxFactory.BinaryExpression(
            SyntaxKind.LessThanExpression,
            newLeft,
            lessThanToken,
            newRight);

        SyntaxNode? root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
        SyntaxNode? newRoot = root.ReplaceNode(binaryExpr, newExpr.WithTriviaFrom(binaryExpr));
        return document.WithSyntaxRoot(newRoot);
    }
}