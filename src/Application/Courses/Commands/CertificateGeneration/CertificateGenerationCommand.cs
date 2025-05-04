using Application.Core;
using Application.Courses.DTOs;
using MediatR;

namespace Application.Courses.Commands.CertificateGeneration;

public class CertificateGenerationCommand(string id) : IRequest<Result<CertificateDto>>
{
    public string Id { get; set; } = id;
}
