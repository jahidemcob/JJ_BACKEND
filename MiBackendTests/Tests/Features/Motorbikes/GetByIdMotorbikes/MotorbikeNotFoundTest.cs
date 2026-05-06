using Backend.src.app.Features.Motobikes.application.exceptions;
using Backend.src.app.Features.Motobikes.application.usecases;
using Backend.src.app.Features.Motobikes.domain.repository;
using Moq;
using Xunit;


namespace MiBackendTests.Tests.Features.Motorbikes.GetByIdMotorbikes;
public class MotorbikeNotFoundTest
{
    [Fact]
    public async Task GetMotorbikeById_ShouldThrowNotFoundException_WhenMotorbikeDoesNotExist()
    {
        // Arrange
        var repoMock = new Mock<IMotorbikesRepository>();
        int motorbikeId = 10;

        // Configuramos el repositorio para que devuelva null (como si no existiera la moto)
        repoMock.Setup(r => r.GetMotorbikeByIdAsync(motorbikeId))
                .ReturnsAsync((Backend.src.app.Features.Motobikes.domain.entities.Motorbike?)null);

        var useCase = new GetByIdMotorbikeUsecase(repoMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<MotorbikeNotFoundException>(() =>
            useCase.Execute(motorbikeId)
        );
    }
}