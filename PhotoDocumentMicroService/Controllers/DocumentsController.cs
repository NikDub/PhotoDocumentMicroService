using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoDocumentMicroService.Application.DTO;
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
    [Authorize($"{nameof(UserRole.Doctor)},{nameof(UserRole.Patient)},{nameof(UserRole.Receptionist)}")]
    public async Task<IActionResult> Get(string id)
    {
        var document = await _documentService.GetByIdAsync(id);
        return Ok(document);
    }

    [HttpGet("/api/result/{id}/documents")]
    [DisableRequestSizeLimit]
    [Authorize($"{nameof(UserRole.Doctor)},{nameof(UserRole.Patient)}")]
    public async Task<IActionResult> GetByPatientId(string id)
    {
        var document = await _documentService.GetByPatientAsyncId(id);
        return Ok(document);
    }

    [HttpPost(nameof(DocumentTypeEnum.Photo))]
    [DisableRequestSizeLimit]
    [Authorize($"{nameof(UserRole.Doctor)},{nameof(UserRole.Patient)},{nameof(UserRole.Receptionist)}")]
    public async Task<IActionResult> CreatePhoto([FromBody] DocumentForCreatedDto model)
    {
        var document = await _documentService.CreateAsync(model, DocumentTypeEnum.Photo.ToString("D"));
        return Created("", document);
    }

    [HttpPost(nameof(DocumentTypeEnum.Document))]
    [DisableRequestSizeLimit]
    [Authorize($"{nameof(UserRole.Doctor)},{nameof(UserRole.Patient)},{nameof(UserRole.Receptionist)}")]
    public async Task<IActionResult> CreateDocument([FromBody] DocumentForCreatedDto model)
    {
        var document = await _documentService.CreateAsync(model, DocumentTypeEnum.Document.ToString("D"));
        return Created("", document);
    }
}