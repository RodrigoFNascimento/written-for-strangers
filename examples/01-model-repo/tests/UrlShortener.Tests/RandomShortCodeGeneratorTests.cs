using UrlShortener.Links;

namespace UrlShortener.Tests;

public class RandomShortCodeGeneratorTests
{
    [Fact]
    public void Codes_have_the_documented_length_and_alphabet()
    {
        var generator = new RandomShortCodeGenerator();

        for (var i = 0; i < 1_000; i++)
        {
            var code = generator.Next();

            Assert.Equal(RandomShortCodeGenerator.Length, code.Length);
            Assert.All(code, c => Assert.Contains(c, RandomShortCodeGenerator.Alphabet));
        }
    }
}
