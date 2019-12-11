using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.FileProviders;
using Naming.Task5.ThirdParty;

namespace Naming.Task5
{
    public class FileManager
    {
        private static readonly string[] _imageTypes = {"jpg", "png"};
        private static readonly string[] _documentTypes = {"pdf", "doc"};

        private const string _baseNamespace = "Naming.Resources";
        private readonly IFileProvider _fileProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly(), _baseNamespace);

        public IFileInfo RetrieveFile(string fileName)
        {
            ValidateFileType(fileName);
            return _fileProvider.GetFileInfo(fileName);
        }

        #region RetrieveFile

        private static void ValidateFileType(string fileName)
        {
            if (IsInValidFileType(fileName))
            {
                throw new InvalidFileTypeException();
            }
        }

        private static bool IsInValidFileType(string fileName)
        {
            return IsInValidImage(fileName) && IsInValidDocument(fileName);
        }

        private static bool IsInValidImage(string fileName)
        {
            var imageExtensionsPredicate = new FileExtention(_imageTypes);
            return !imageExtensionsPredicate.Test(fileName);
        }

        private static bool IsInValidDocument(string fileName)
        {
            var documentExtensionsPredicate = new FileExtention(_documentTypes);
            return !documentExtensionsPredicate.Test(fileName);
        }

        #endregion

        public List<string> GetAllImages()
        {
            return Files(_imageTypes);
        }

        public List<string> GetAllDocuments()
        {
            return Files(_documentTypes);
        }

        private List<string> Files(string[] allowedExtensions)
        {
            var pred = new FileExtention(allowedExtensions);
            return _fileProvider.GetDirectoryContents(string.Empty)
                .Select(f => f.Name)
                .Where(pred.Test)
                .ToList();
        }
    }
}
