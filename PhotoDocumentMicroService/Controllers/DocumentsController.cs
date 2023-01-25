using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoDocumentMicroService.Application.Dto;
using PhotoDocumentMicroService.Application.Service.Abstractions;
using PhotoDocumentMicroService.Domain.Entities.Enums;

namespace PhotoDocumentMicroService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentsController : Controller
{
    private readonly IDocumentService _documentService;

    public DocumentsController(IDocumentService documentService)
    {
        _documentService = documentService;
    }


    [HttpGet("{id}")]
    [DisableRequestSizeLimit]
    [Authorize(Roles = $"{nameof(UserRole.Doctor)},{nameof(UserRole.Patient)},{nameof(UserRole.Receptionist)}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var document = await _documentService.GetByIdAsync(id);
        return Ok(document);
    }

    [HttpGet("/api/Results/{id}/documents")]
    [DisableRequestSizeLimit]
    [Authorize(Roles = $"{nameof(UserRole.Doctor)},{nameof(UserRole.Patient)}")]
    public async Task<IActionResult> GetByResultId(Guid id)
    {
        var document = await _documentService.GetByResultIdAsync(id);
        return Ok(document);
    }

    [HttpPost(nameof(DocumentType.Photo))]
    [DisableRequestSizeLimit]
    [Authorize(Roles = $"{nameof(UserRole.Doctor)},{nameof(UserRole.Patient)},{nameof(UserRole.Receptionist)}")]
    public async Task<IActionResult> CreatePhoto([FromBody] PhotoForCreatedDto model)
    {
        var document = await _documentService.CreatePhotoAsync(model);
        return Created("", document);
    }

    [HttpPost(nameof(DocumentType.Document))]
    [DisableRequestSizeLimit]
    [Authorize(Roles = $"{nameof(UserRole.Doctor)},{nameof(UserRole.Patient)},{nameof(UserRole.Receptionist)}")]
    public async Task<IActionResult> CreateDocument([FromBody] DocumentForCreatedDto model)
    {
        var document = await _documentService.CreateDocumentAsync(model);
        return Created("", document);
    }
}