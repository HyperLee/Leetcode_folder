namespace leetcode_316.Tests;

public class RemoveDuplicateLettersTests
{
    public static TheoryData<string, string> ValidCases => new()
    {
        { "bcabc", "abc" },
        { "cbacdcbc", "acdb" },
        { "cdadabcc", "adbc" },
        { "abacb", "abc" },
        { string.Empty, string.Empty }
    };

    [Theory]
    [MemberData(nameof(ValidCases))]
    public void RemoveDuplicateLetters_ValidInput_ReturnsExpectedResult(string input, string expected)
    {
        Program solver = new Program();

        string actual = solver.RemoveDuplicateLetters(input);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(ValidCases))]
    public void RemoveDuplicateLetters2_ValidInput_ReturnsExpectedResult(string input, string expected)
    {
        Program solver = new Program();

        string actual = solver.RemoveDuplicateLetters2(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void RemoveDuplicateLetters_NullInput_ThrowsArgumentNullException()
    {
        Program solver = new Program();

        Assert.Throws<ArgumentNullException>(() => solver.RemoveDuplicateLetters(null!));
    }

    [Fact]
    public void RemoveDuplicateLetters2_NullInput_ThrowsArgumentNullException()
    {
        Program solver = new Program();

        Assert.Throws<ArgumentNullException>(() => solver.RemoveDuplicateLetters2(null!));
    }
}