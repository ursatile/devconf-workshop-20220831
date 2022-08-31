using Grpc.Core;
using GrpcGreeter;

namespace GrpcGreeter.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        var message = request.Language switch {
            "pl-PL" => $"Cześć, {request.FirstName} {request.LastName}",
            "en-GB" => $"Good morning, {request.FirstName} {request.LastName}",
            _ => $"Hello, {request.FirstName} {request.LastName}"
        };
        return Task.FromResult(new HelloReply {
            Message = message
        });
    }
}
