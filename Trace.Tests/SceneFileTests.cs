using System.IO;
using Xunit;
using System.Text;
using System;
 

namespace Trace.Tests;

public class SceneFileTests
{
    [Fact]
    public void TestInputFile()
    {
        var buf = Encoding.ASCII.GetBytes("abc   \nd\nef");
        using var memStream = new MemoryStream(buf);
        var stream = new InputStream(memStream);

        Assert.True(stream.Location.LineNum == 1);
        Assert.True(stream.Location.ColNum == 1);

        Assert.True(stream.read_char() == 'a');
        Assert.True(stream.Location.LineNum == 1);
        Assert.True(stream.Location.ColNum == 2);

        stream.unread_char('A');
        Assert.True(stream.Location.LineNum == 1);
        Assert.True(stream.Location.ColNum == 1);

        Assert.True(stream.read_char() == 'A');
        Assert.True(stream.Location.LineNum == 1);
        Assert.True(stream.Location.ColNum == 2);

        Assert.True(stream.read_char() == 'b');
        Assert.True(stream.Location.LineNum == 1);
        Assert.True(stream.Location.ColNum == 3);

        Assert.True(stream.read_char() == 'c');
        Assert.True(stream.Location.LineNum == 1);
        Assert.True(stream.Location.ColNum == 4);

        stream.skip_whitespaces_and_comments();

        Assert.True(stream.read_char() == 'd');
        Assert.True(stream.Location.LineNum == 2);
        Assert.True(stream.Location.ColNum == 2);

        Assert.True(stream.read_char() == '\n');
        Assert.True(stream.Location.LineNum == 3);
        Assert.True(stream.Location.ColNum == 1);

        Assert.True(stream.read_char() == 'e');
        Assert.True(stream.Location.LineNum == 3);
        Assert.True(stream.Location.ColNum == 2);

        Assert.True(stream.read_char() == 'f');
        Assert.True(stream.Location.LineNum == 3);
        Assert.True(stream.Location.ColNum == 3);

        Assert.True(stream.read_char() == null);
    }

    [Fact]
    public void TestLexer()
    {
        var buf = Encoding.ASCII.GetBytes(
            "# This is a comment" +
            "# This is another comment" +
            "\nnew material sky_material(" +
            "diffuse(image(\"my file.pfm\"))," +
            "<5.0, 500.0, 300.0>" +
            ") # Comment at the end of the line"
        );
        using var memStream = new MemoryStream(buf);
        var stream = new InputStream(memStream);

        
        Assert.True(stream.ReadToken().Keyword == KeywordEnum.New);
        Assert.True(stream.ReadToken().Keyword == KeywordEnum.Material);
        Assert.True(stream.ReadToken().Identifier == "sky_material");
        Assert.True(stream.ReadToken().Symbol == '(');
        Assert.True(stream.ReadToken().Keyword == KeywordEnum.Diffuse);
        Assert.True(stream.ReadToken().Symbol == '(');
        Assert.True(stream.ReadToken().Keyword == KeywordEnum.Image);
        Assert.True(stream.ReadToken().Symbol == '(');
        Assert.True(stream.ReadToken().String == "my file.pfm");
        Assert.True(stream.ReadToken().Symbol == ')');
    }
}