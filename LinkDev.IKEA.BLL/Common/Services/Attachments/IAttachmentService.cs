using Microsoft.AspNetCore.Http;

namespace LinkDev.IKEA.BLL.Common.Services.Attachments
{
	public interface IAttachmentService
	{
		public Task<string?> UploadAsync(IFormFile file, string folderName);
		
		public bool Delete(string filePath);
	}
}
