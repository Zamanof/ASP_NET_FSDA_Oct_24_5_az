namespace ASP_NET_22._TaskFlowCQRS_Unit_Test.Basic;

public class CalculatorTests
{
    // AAA
    // Arrange
    // Act
    // Assert 

    [Fact]
    public void Add_ZeroPlusZero_ReturnsZero()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(0, 0);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void Add_ZeroPlusOther_ReturnsNotZero()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(0, 2);

        // Assert
        Assert.Equal(2, result);
    }


    public static IEnumerable<object[]> AddData()
    {
        yield return new object[] { 5, 4, 9 };
        yield return new object[] { 15, 24, 39 };
        yield return new object[] { -2, -4, -6 };
        yield return new object[] { 57, 4, 61 };
    }

    [Theory]
    //[InlineData(5, 4, 9)]
    //[InlineData(15, 24, 39)]
    //[InlineData(-2, -4, -6)]
    //[InlineData(57, 4, 61)]
    //[InlineData(-5, 4, -1)]
    //[InlineData(0, 0, 0)]
    [MemberData(nameof(AddData))]
    public void Add_ReturnsExpectedResult(int left, int right, int expectedResult)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(left, right);

        // Assert
        Assert.Equal(expectedResult, result);
    }

}
