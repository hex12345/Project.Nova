namespace TrialEngine.Debate;

public static class MathLawResolver
{
    public static bool IsTriangleValid(float a, float b, float c)
    {
        return a + b > c && a + c > b && b + c > a;
    }

    public static bool IsSquareRootStatementTrue(float radicand, float claim, float epsilon = 0.001f)
    {
        if (radicand < 0f)
            return false;

        return MathF.Abs(MathF.Sqrt(radicand) - claim) <= epsilon;
    }

    public static bool IsRightTriangleHypotenuse(float a, float b, float c, float epsilon = 0.001f)
    {
        return MathF.Abs(a * a + b * b - c * c) <= epsilon;
    }
}
