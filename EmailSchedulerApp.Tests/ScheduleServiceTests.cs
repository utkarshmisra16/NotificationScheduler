using System.Text.Json;
using EmailSchedulerApp.DTOs.Schedule;
using EmailSchedulerApp.Models;
using EmailSchedulerApp.Repositories.Interfaces;
using EmailSchedulerApp.Services.Implementation;
using Moq;

namespace EmailSchedulerApp.Services.Implementations;

public class ScheduleServiceTests
{
    [Fact]
    public async Task SaveSchedule_ShouldMapRequestAndReturnSuccess()
    {
        // Arrange
        var repositoryMock = new Mock<IScheduleRepository>();

        Schedule? savedSchedule = null;

        repositoryMock
            .Setup(x => x.SaveSchedule(It.IsAny<Schedule>()))
            .Callback<Schedule>(schedule =>
            {
                savedSchedule = schedule;
            })
            .ReturnsAsync(10);

        var service = new ScheduleService(repositoryMock.Object);

        var request = new CreateScheduleRequestDto
        {
            Name = "Monthly Report",
            Channel = "Email",
            TemplateId = 1,
            Description = "Monthly sales report",
            StartDate = DateTime.Today,
            StartTime = TimeSpan.FromHours(10),
            Timezone = "IST",

            RecipientEmails =
            [
                "rahul@gmail.com",
                "amit@gmail.com"
            ]
        };

        // Act
        var result = await service.SaveSchedule(request);

        // Assert
        Assert.NotNull(savedSchedule);

        Assert.Equal("Monthly Report", savedSchedule!.Name);
        Assert.Equal("Email", savedSchedule.Channel);
        Assert.Equal(1, savedSchedule.TemplateId);

        Assert.Equal(
            JsonSerializer.Serialize(request.RecipientEmails),
            savedSchedule.Recipients
        );

        Assert.True(savedSchedule.IsActive);

        Assert.Equal(10, result.ScheduleId);
        Assert.True(result.Success);
        Assert.Equal(
            "Schedule created successfully.",
            result.Message
        );

        repositoryMock.Verify(
            x => x.SaveSchedule(It.IsAny<Schedule>()),
            Times.Once
        );
    }
}