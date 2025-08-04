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

namespace Simplicity
{
    /// <summary>
    /// Code fix provider that flips operands and changes '>' to '<'.
    /// </summary>
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(GreaterThanCodeFixProvider)), Shared]
    public class GreaterThanCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(GreaterThanAnalyzer.DiagnosticId);

        public sealed override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            var diagnostic = context.Diagnostics[0];
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            // Find the binary expression node flagged by the analyzer
            var node = root.FindNode(diagnosticSpan);
            if (node is not BinaryExpressionSyntax binaryExpr || binaryExpr.Kind() != SyntaxKind.GreaterThanExpression)
                return;

            context.RegisterCodeFix(
                CodeAction.Create(
                    title: "Flip operands and use '<'",
                    createChangedDocument: c => FlipGreaterThanAsync(context.Document, binaryExpr, c),
                    equivalenceKey: "FlipGreaterThanToLessThan"),
                diagnostic);
        }

        private async Task<Document> FlipGreaterThanAsync(Document document, BinaryExpressionSyntax binaryExpr, CancellationToken cancellationToken)
        {
            // Swap leading/trailing trivia between operands
            var left = binaryExpr.Left;
            var right = binaryExpr.Right;

            // Save trivia
            var leftLeading = left.GetLeadingTrivia();
            var leftTrailing = left.GetTrailingTrivia();
            var rightLeading = right.GetLeadingTrivia();
            var rightTrailing = right.GetTrailingTrivia();

            // Swap trivia
            var newLeft = right.WithLeadingTrivia(leftLeading).WithTrailingTrivia(leftTrailing);
            var newRight = left.WithLeadingTrivia(rightLeading).WithTrailingTrivia(rightTrailing);

            var lessThanToken = SyntaxFactory.Token(SyntaxKind.LessThanToken).WithTriviaFrom(binaryExpr.OperatorToken);
            var newExpr = SyntaxFactory.BinaryExpression(
                SyntaxKind.LessThanExpression,
                newLeft,
                lessThanToken,
                newRight);

            var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
            var newRoot = root.ReplaceNode(binaryExpr, newExpr.WithTriviaFrom(binaryExpr));
            return document.WithSyntaxRoot(newRoot);
        }
    }
}
