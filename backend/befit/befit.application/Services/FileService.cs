using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.application.Contracts;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace befit.application.Services
{
    internal class FileService : IFileService
    {
        private readonly ICloudinary _cloud;
        public FileService(ICloudinary cloud)
        {
            _cloud = cloud;
        }

        public async Task Delete(string filePath)
        {
            var parameters = new DeletionParams(getPublicId(filePath));

            await _cloud.DestroyAsync(parameters);
        }

        public async Task<string> Upload(FileStream file)
        {

            var parameters = new RawUploadParams
            {
                File = new FileDescription(new Guid().ToString(), file)
            };

            return (await _cloud.UploadAsync(parameters)).Url.ToString();
        }

        private string getPublicId(string filePath)
        {
            return filePath.Split('/').Last().Split('.').First();
        }
    }
}
