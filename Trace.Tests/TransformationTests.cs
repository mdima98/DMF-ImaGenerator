using System.Numerics;
using Trace.Geometry;
using Xunit;

namespace Trace.Tests;

public class TransformationTests
{
    [Fact]
    public void TestIsClose()
    {
        var m = new Matrix4x4(
            1.0f, 2.0f, 3.0f, 4.0f,
            5.0f, 6.0f, 7.0f, 8.0f,
            9.0f, 9.0f, 8.0f, 7.0f,
            6.0f, 5.0f, 4.0f, 1.0f);

        var invm = new Matrix4x4(
            -3.75f, 2.75f, -1.0f, 0f,
            4.375f, -3.875f, 2.0f, -0.5f,
            0.5f, 0.5f, -1.0f, 1.0f,
            -1.375f, 0.875f, 0.0f, -0.5f);

        var m1 = new Transformation(m, invm);
        
        Assert.True(m1.is_consistent());

    }
    
}