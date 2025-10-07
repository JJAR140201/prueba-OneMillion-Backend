using Microsoft.AspNetCore.Mvc;
using RealEstate.Infrastructure.Mongo;
using MongoDB.Driver;
using MongoDB.Bson;

namespace RealEstate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class DiagnosticController(MongoContext mongoContext) : ControllerBase
{
    /// <summary>
    /// Endpoint para diagnosticar la conexión y estructura de datos en MongoDB
    /// </summary>
    [HttpGet("database-info")]
    public async Task<IActionResult> GetDatabaseInfo()
    {
        try
        {
            // Verificar conexión básica
            var databaseName = mongoContext.Database.DatabaseNamespace.DatabaseName;
            
            // Verificar colecciones
            var collections = await mongoContext.Database.ListCollectionNamesAsync();
            var collectionList = await collections.ToListAsync();
            
            // Contar documentos usando BsonDocument para evitar problemas de mapeo
            var propertiesCollection = mongoContext.Database.GetCollection<BsonDocument>("properties");
            var propertiesCount = await propertiesCollection.CountDocumentsAsync(new BsonDocument());
            
            // Obtener algunos documentos raw de ejemplo
            var sampleDocs = await propertiesCollection
                .Find(new BsonDocument())
                .Limit(2)
                .ToListAsync();

            return Ok(new
            {
                DatabaseName = databaseName,
                Collections = collectionList,
                PropertiesCount = propertiesCount,
                SampleDocuments = sampleDocs.Select(doc => doc.ToJson()).ToList(),
                Status = "Connected"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Error = ex.Message,
                StackTrace = ex.StackTrace,
                Status = "Connection Failed"
            });
        }
    }
}