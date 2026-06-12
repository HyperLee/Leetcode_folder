using System.Diagnostics;
using Xunit;

namespace leetcode_187.Tests;

public class ProgramOutputTests
{
    [Fact]
    public void Main_PrintsRunnableSampleResultsForBothSolutions()
    {
        String projectPath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "leetcode_187", "leetcode_187.csproj"));

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"run --project \"{projectPath}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using Process? process = Process.Start(startInfo);

        Assert.NotNull(process);

        String standardOutput = process.StandardOutput.ReadToEnd();
        String standardError = process.StandardError.ReadToEnd();

        process.WaitForExit();

        Assert.True(process.ExitCode == 0, $"dotnet run failed.{Environment.NewLine}{standardError}");
        Assert.Contains("Solution 1", standardOutput);
        Assert.Contains("Solution 2", standardOutput);
        Assert.Contains("Expected", standardOutput);
        Assert.DoesNotContain("Hello, World!", standardOutput);
    }
}
