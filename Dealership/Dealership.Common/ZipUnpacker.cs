using Ionic.Zip;

namespace Dealership.Common
{
    public class ZipUnpacker
    {
        public void Unpack(string zipToUnpack, string directoryToUnpackIn)
        {
            using (var zipFileList = ZipFile.Read(zipToUnpack))
            {
                foreach (var zipFile in zipFileList)
                {
                    zipFile.Extract(directoryToUnpackIn, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }
    }
}
