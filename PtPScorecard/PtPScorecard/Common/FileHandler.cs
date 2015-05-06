using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Net.Http;
using System.Net.Http.Headers;


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

        //This method is NOT working at the moment
        //public static async Task WriteToOnedrive(this string text, StorageFolder folder = null, CreationCollisionOption options = CreationCollisionOption.OpenIfExists)
        //{
        //    //Create the file
        //    string fileName = "test_file.txt";
        //    folder = folder ?? ApplicationData.Current.LocalFolder;
        //    var file = await folder.CreateFileAsync(fileName, options);
        //    using (var fs = await file.OpenAsync(FileAccessMode.ReadWrite))
        //    {
        //        using (var outStream = fs.GetOutputStreamAt(0))
        //        {
        //            using (var dataWriter = new DataWriter(outStream))
        //            {
        //                if (text != null)
        //                    dataWriter.WriteString(text);
        //                await dataWriter.StoreAsync();
        //                dataWriter.DetachStream();
        //            }

        //            await outStream.FlushAsync();
        //        }
        //    }
        //    //Send to Onedrive
        //    try
        //    {
        //        using (HttpClient hc = new HttpClient())
        //        {
        //            //PUT https://apis.live.net/v5.0/me/skydrive/files/HelloWorld.txt?access_token=ACCESS_TOKEN

        //            hc.BaseAddress = new Uri("https://apis.live.net");
        //            string AccessToken = "EwBoAq1DBAAUGCCXc8wU/zFu9QnLdZXy+YnElFkAAW6UTNx1C1XGhD5Om7oRZ1tjeAPnlc07Fw3Kz0pU6Kw7hasJzoqrWmxsHb41gs+dXhtMLZ9qfR2dCKj8ZiR69ZU9xFAFN22ktFW+AlRickZgp07oRWZW9XoymRhC90GbnzhgwXUUdRkE9AdY8aiML2pLkNLvnt9+jYuuXZrFAg9yLS+Xth6ccVQMw7C3Ynv/UPacsQVAw+pcTs0bzFSbSoqpv2xng8Of07Fc7akXRbzI6hJ9Ng8ZwTawXI9IUBcJMq1nZrUOetovjtEQZDVZD1ockL1Qi2zCbi9GxgYPN071GdcKRsrgP4JwyA5JIAbYxsa7qpY/tKf56Zmni4lrrXYDZgAACFhNFCjj+Dk+OAGcdYgGfN4fsUS06iMxx+Efcft+oavxOirirFr8WKybGh5MFOQxm1/LWm7CyrEpOycHp+UaWwR3gRxAZyleFer1qIzbYFBnKwm+wRX+n5uqkHJGhPnIGCBI3GCwDsVL8+A5jHHLxn31losfPCs7nS01wGYyJ7pRQPwPRLJlZHmSUFkB+4GoYjkjfRmeCy7Dxlu4998iBSIrPTRZIj+b92rUSmIDfgc/PxBS0xvAIooTEV9muy6deWtdePxm1+wyL49wqNWaTcoPhvgU93KgVhTUI2I/ex04dMV1SiLLEFjVZk0dra5vGE/9gTVhhDkxR3jxJhlKa9krPt+JDTV6BDyi3JevGviMO+cprfics3y0VfmeLKTXOoZ7xdtK4Hgt5LYZLAj/SYRzzAIARwclnGwGIFgLDZhXDwtaAQ==";
        //            var url = "v5.0/me/skydrive/files/HelloWorld.txt?access_token=" + AccessToken;
        //            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            HttpContent content = await folder.GetFileAsync(fileName);
        //            HttpResponseMessage response = await hc.PutAsync(url,content,);

        //            //Write debug information
        //            System.Diagnostics.Debug.WriteLine("Response: " + response.StatusCode + " ---- " + response.ToString());
        //            }
        //        }
            
        //    //If not successful, give feedback to user
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine("Exception: " + ex.ToString());
        //        //InputBox.Text = "Error occured, sorry.";
        //    }

        //    //Delete the file from local directory

        //}


    }
}
