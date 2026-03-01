using ASP_NET_22._TaskFlow_CQRS.Application.DTOs;
using FluentAssertions;

namespace ASP_NET_22._TaskFlowCQRS_Unit_Test.Parametrized;
public class TakeItemQueryParamsTests
{
    [Theory]
    [InlineData(0, 10,   1, 10)]
    [InlineData(15, 0,   15, 10)]
    [InlineData(25, 101, 25, 100)]
    [InlineData(13, 20,  13, 20)]
    public void Validate_NormalizesPageAndSize(
        int page, 
        int size, 
        int expectedPage, 
        int expectedSize)
    {
        // Arange
        var param = new TaskItemQueryParams { Page = page, Size = size };

        // Act
        param.Validate();

        // Assert
        param.Page.Should().Be(expectedPage);
        param.Size.Should().Be(expectedSize);
    }
}
