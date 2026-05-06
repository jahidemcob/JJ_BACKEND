using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Backend.src.app.Features.Motobikes.application.usecases;
using Backend.src.app.Features.Motobikes.domain.repository;
using Backend.src.app.Features.Motobikes.domain.entities;
using Backend.src.app.Features.Motobikes.application.DTOs;

namespace MiBackendTests.Tests.Features.Motorbikes.GetAllMotorbikes;
public class GetAllMotorbikesUsecaseTests
{
    [Fact]
    public async Task Execute_ShouldReturnMotorbikesOnlyForUser()
    {
        // ARRANGE
        var repoMock = new Mock<IMotorbikesRepository>();

        // Datos simulados que devolvería el repositorio
        var mockData = new List<(Motorbike moto, string nombreUsuario)>
{
    (
        new Motorbike {
            idMoto = 1,
            idUsuario = 10,
            marca = "Yamaha",
            modelo = "R15",
            placa = "AAA123",
            cilindraje = 150,
            anio = 2020,
            Activo = true
        },
        "Juan Pérez"
    ),
    (
        new Motorbike {
            idMoto = 2,
            idUsuario = 20,
            marca = "Honda",
            modelo = "CBF",
            placa = "BBB456",
            cilindraje = 250,
            anio = 2019,
            Activo = true
        },
        "Carlos Gómez"
    )
};

        repoMock
            .Setup(r => r.GetAllMotorbikesWithUserAsync())
            .ReturnsAsync(mockData);

        var usecase = new GetAllMotorbikesUsecase(repoMock.Object);

        int userId = 10;

        // ACT
        var result = await usecase.Execute(userId);

        // ASSERT
        Assert.Single(result); // solo debe devolver la moto del usuario 10

        var moto = result.First();
        Assert.Equal(1, moto.idMoto);
        Assert.Equal(10, moto.idUsuario);
        Assert.Equal("Yamaha", moto.marca);
        Assert.Equal("Juan Pérez", moto.nombreUsuario);

        // Verificar que el repositorio fue llamado exactamente una vez
        repoMock.Verify(r => r.GetAllMotorbikesWithUserAsync(), Times.Once);
    }
}