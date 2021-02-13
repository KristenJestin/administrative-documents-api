using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Domain.Settings
{
    public class DocumentSettings
    {
        public string EncryptKey { get; set; }
        public string[] BaseFilePath { get; set; }


        #region methods
        public string BuildFilePath(string file)
            => Path.Combine(BaseFilePath.Prepend(Directory.GetCurrentDirectory()).Append(file).ToArray());
        #endregion
    }
}
