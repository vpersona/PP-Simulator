using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using Simulator;
namespace TestSimulator;
public class ValidatorTest
{
    [Fact]
    public void Limiter_ValueBelowMin_ReturnsMin()
    {
        int result = Validator.Limiter(-5, 0, 10);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Limiter_ValueAboveMax_ReturnsMax()
    {
        int result = Validator.Limiter(15, 0, 10);
        Assert.Equal(10, result);
    }

    [Fact]
    public void Limiter_ValueWithinRange_ReturnsValue()
    {
        int result = Validator.Limiter(5, 0, 10);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Shortener_ValueShorterThanMin_PadsRight()
    {
        string result = Validator.Shortener("test", 6, 10);
        Assert.Equal("test##", result);
    }

    [Fact]
    public void Shortener_ValueLongerThanMax_TrimsEnd()
    {
        string result = Validator.Shortener("hello world", 6, 8);
        Assert.Equal("hello wo", result);
    }

    [Fact]
    public void Shortener_ValueWithinRange_ReturnsValue()
    {
        string result = Validator.Shortener("test", 2, 10);
        Assert.Equal("test", result);
    }

    [Fact]
    public void Shortener_CustomPlaceholder_PadsWithCustomCharacter()
    {
        string result = Validator.Shortener("test", 6, 10, '*');
        Assert.Equal("test**", result);
    }
}

