using ASP_NET_22._TaskFlow_CQRS.Application.Interfaces;
using ASP_NET_22._TaskFlow_CQRS.Application.Mapping;
using ASP_NET_22._TaskFlow_CQRS.Application.Queries.Projects;
using ASP_NET_22._TaskFlow_CQRS.Domain;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace ASP_NET_22._TaskFlowCQRS_Unit_Test.Handlers;

public class GetProjectsQueryHanlderTests
{
    private static readonly IMapper mapper = new MapperConfiguration(
        config => config.AddProfile<MappingProfile>()).CreateMapper();

    [Fact]
    public async Task Handle_CallsGetAllForUserAsyncAndReturnsMappedDtos()
    {
        // Arrange
        var projectRepo = new Mock<IProjectRepository>();

        var projects = new List<Project>
        {
            new()
            {
               Id = 1,
               Name = "proj1",
               OwnerId = "user1",
               CreatedAt = DateTimeOffset.UtcNow
            }
        };

        projectRepo
            .Setup(r => r.GetAllForUserAsync("user1", It.IsAny<IList<string>>()))
            .ReturnsAsync(projects);
        var handler = new GetProjectsQueryHandler(projectRepo.Object, mapper);
        var query = new GetProjectsQuery("user1", new List<string> { "User" });

        // Act

        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().HaveCount(1);
        result.First().Name.Should().Be("proj1");
        projectRepo.Verify(r=> r.GetAllForUserAsync("user1", It.IsAny<IList<string>>()), Times.Once);
    }
}
