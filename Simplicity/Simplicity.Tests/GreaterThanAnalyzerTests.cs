using System.Threading.Tasks;
using Xunit;
using Verifier = Microsoft.CodeAnalysis.CSharp.Testing.XUnit.CodeFixVerifier<Simplicity.GreaterThanAnalyzer, Simplicity.GreaterThanCodeFixProvider>;

namespace Simplicity.Tests;

public class GreaterThanAnalyzerTests
{
    [Fact]
    public async Task ReportsDiagnosticOnGreaterThanUsage()
    {
        var test = @"
class C
{
    void M()
    {
        if (1 > 0) { }
    }
}";
        var expected = Verifier.Diagnostic()
            .WithLocation(6, 15);
        await Verifier.VerifyAnalyzerAsync(test, expected);
    }

    [Fact]
    public async Task CodeFix_FlipsOperandsAndOperator()
    {
        var test = @"
class C
{
    void M()
    {
        int a = 1, b = 2;
        if (a > b) { }
    }
}";
        var fixedCode = @"
class C
{
    void M()
    {
        int a = 1, b = 2;
        if (b < a) { }
    }
}";
        var expected = Verifier.Diagnostic().WithLocation(7, 15);
        await Verifier.VerifyCodeFixAsync(test, expected, fixedCode);
    }

    [Fact]
    public async Task CodeFix_PreservesTriviaAndComments()
    {
        var test = @"
class C
{
    void M()
    {
        int x = 1, y = 2;
        // compare
        if (/*left*/ x /*mid*/ > /*right*/ y) { }
    }
}";
        var fixedCode = @"
class C
{
    void M()
    {
        int x = 1, y = 2;
        // compare
        if (/*left*/ y /*mid*/ < /*right*/ x) { }
    }
}";
    var expected = Verifier.Diagnostic().WithLocation(8, 32);
        await Verifier.VerifyCodeFixAsync(test, expected, fixedCode);
    }

    [Fact]
    public async Task CodeFix_FlipsOperandsInLambda()
    {
        var test = @"
class C
{
    void M()
    {
        System.Func<int, int, bool> f = (a, b) => a > b;
    }
}";
        var fixedCode = @"
class C
{
    void M()
    {
        System.Func<int, int, bool> f = (a, b) => b < a;
    }
}";
        var expected = Verifier.Diagnostic().WithLocation(6, 53);
        await Verifier.VerifyCodeFixAsync(test, expected, fixedCode);
    }

    [Fact]
    public async Task NoDiagnosticWhenNoGreaterThan()
    {
        var test = @"
class C
{
    void M()
    {
        if (1 == 0) { }
    }
}";
        await Verifier.VerifyAnalyzerAsync(test);
    }

    [Fact]
    public async Task ReportsDiagnosticOnGreaterThanInLambda()
    {
        var test = @"
class C
{
    void M()
    {
        System.Func<int, int, bool> f = (a, b) => a > b;
    }
}";
        var expected = Verifier.Diagnostic()
            .WithLocation(6, 53);
        await Verifier.VerifyAnalyzerAsync(test, expected);
    }
}
