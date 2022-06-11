using LanguageExt;
using Xunit;

namespace Sample.Tests;

public class PreludeSpec
{
    [Fact]
    public void AddSuccess()
    {
        var a = Schedule.Once | Schedule.recurs(5);


    }
}
