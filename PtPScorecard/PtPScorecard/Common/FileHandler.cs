using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;


namespace PtPScorecard.Common
{
    public static class FileHandler
    {
        public static async Task WriteToFile(this string text, string fileName, StorageFolder folder = null, CreationCollisionOption options = CreationCollisionOption.OpenIfExists)
        {
            folder = folder ?? ApplicationData.Current.LocalFolder;
            //Read old file contents
            string old = await ReadFromFile("PtP-SaveFile.txt");
            var file = await folder.CreateFileAsync(fileName, options);
            using (var fs = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (var outStream = fs.GetOutputStreamAt(0))
                {
                    using (var dataWriter = new DataWriter(outStream))
                    {
                        if (text != null)
                           dataWriter.WriteString(old + text);
                        await dataWriter.StoreAsync();
                        dataWriter.DetachStream();
                    }

                    await outStream.FlushAsync();
                }
            }
        }

        public static async Task<string> ReadFromFile(string fileName, StorageFolder folder = null)
        {
            folder = folder ?? ApplicationData.Current.LocalFolder;
            var file = await folder.GetFileAsync(fileName);

            using (var fs = await file.OpenAsync(FileAccessMode.Read))
            {
                using (var inStream = fs.GetInputStreamAt(0))
                {
                    using (var reader = new DataReader(inStream))
                    {
                        await reader.LoadAsync((uint)fs.Size);
                        string data = reader.ReadString((uint)fs.Size);
                        reader.DetachStream();
                        System.Diagnostics.Debug.WriteLine(data);
                        return data;
                    }
                }
            }
        }


    }
}
