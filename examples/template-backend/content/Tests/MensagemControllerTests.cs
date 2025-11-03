using Application.Controllers.V1;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Tests;

public class MensagemControllerTests
{
    [Fact]
    public void Mensagem_HasApiControllerAttribute()
    {
        // Verifica se o controller tem o atributo ApiController
        var attribute = Attribute.GetCustomAttribute(typeof(MensagemController), typeof(ApiControllerAttribute));
        Assert.NotNull(attribute);
    }

    [Fact]
    public void Mensagem_HasApiVersionAttribute()
    {
        // Verifica se o controller tem o atributo ApiVersion
        var attribute = Attribute.GetCustomAttribute(typeof(MensagemController), typeof(ApiVersionAttribute));
        Assert.NotNull(attribute);
    }
}