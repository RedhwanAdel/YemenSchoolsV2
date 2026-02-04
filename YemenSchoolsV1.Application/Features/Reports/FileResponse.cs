namespace YemenSchoolsV1.Application.Features.Reports
{
    public class FileResponse
    {
        public byte[] FileContents { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }

        public FileResponse(byte[] fileContents, string contentType, string fileName)
        {
            FileContents = fileContents;
            ContentType = contentType;
            FileName = fileName;
        }
    }
}
