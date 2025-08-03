using System.Threading.Tasks;
using Xunit;
using Verifier = Microsoft.CodeAnalysis.CSharp.Testing.XUnit.AnalyzerVerifier<Simplicity.GreaterThanAnalyzer>;

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
