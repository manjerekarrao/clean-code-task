using System.Linq;
using Naming.Task5.ThirdParty;

namespace Naming.Task5
{
    public class FileExtention : IPredicate<string>
    {
        private readonly string[] _extentions;

        public FileExtention(string[] extentions)
        {
            this._extentions = extentions;
        }

        public bool Test(string fileName)
        {
            fileName = fileName.ToLower();
            return _extentions.Any(fileName.EndsWith);
        }
    }
}
