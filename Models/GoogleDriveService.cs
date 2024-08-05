using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using System.IO;
using System.Threading.Tasks;

public class GoogleDriveService
{
    private readonly DriveService _driveService;

    public GoogleDriveService(string credentialsPath)
    {
        GoogleCredential credential;
        using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream)
                .CreateScoped(DriveService.ScopeConstants.DriveFile);
        }

        _driveService = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "ASP.NET Core Google Drive Integration",
        });
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var fileMetadata = new Google.Apis.Drive.v3.Data.File
        {
            Name = file.FileName,
            Parents = new List<string> { "1__-FLUnDXdpDn1U_2Wv7mfZyiEUul2j0" }
        };

        FilesResource.CreateMediaUpload request;
        using (var stream = file.OpenReadStream())
        {
            request = _driveService.Files.Create(fileMetadata, stream, file.ContentType);
            request.Fields = "id";
            await request.UploadAsync();
        }

        var fileId = request.ResponseBody.Id;
        return fileId;
    }

    public async Task<string> GetFileUrl(string fileId)
    {
        return $"https://drive.google.com/uc?export=view&id={fileId}";
    }
}
