#nullable enable

using FluentAssertions;
using Wino.Core.Domain.Misc;
using Xunit;

namespace Wino.Core.Tests.Models;

public class MessageIdGeneratorTests
{
    [Fact]
    public void Generate_Should_Use_Sender_Domain()
    {
        var messageId = MessageIdGenerator.Generate("someone@outlook.com");

        messageId.Should().MatchRegex("^<[0-9a-f-]{36}@outlook\\.com>$");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("no-at-sign")]
    [InlineData("trailing@")]
    public void Generate_Should_Fall_Back_To_Neutral_Domain(string? senderAddress)
    {
        var messageId = MessageIdGenerator.Generate(senderAddress);

        messageId.Should().EndWith("@localhost>");
        messageId.Should().NotContainEquivalentOf("wino");
    }
}
