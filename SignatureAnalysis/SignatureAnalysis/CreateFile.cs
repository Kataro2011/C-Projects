using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace SignatureAnalysis
{
    public class CreateFile
    {

        public string GetFileType(string fileSignature)
        {
            if (fileSignature == "ffd8ffe0")

                return "JPG file";

            else if (fileSignature == "25504446")

                return "PDF file";

            else

                return "Other file";
        }

        public string GetFileSignature(string path)
        {
            int bytesRead = 4;

            byte[] fileBuffer;

            using (FileStream filesStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(filesStream))

                    fileBuffer = binaryReader.ReadBytes(bytesRead);
            }
            string fileSignature = BitConverter.ToString(fileBuffer);

            return fileSignature.Replace("-", String.Empty).ToLower();
        }

 
      
        public string createMD5Hash(string path)
        {
            MD5 md5hash = MD5.Create();

            byte[] bytedata = md5hash.ComputeHash(Encoding.Default.GetBytes(path));

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < bytedata.Length; i++)
            
                stringBuilder.Append(bytedata[i].ToString("x2"));

                return stringBuilder.ToString();
            
        }

        public void createFile(string filePath, string fileType, string hash, string csvFile)
        {
            string columnSeperator = ",";

            string[][] fileInformation = new string[][] { new string[] { filePath, fileType, hash } };

            int length = fileInformation.GetLength(0);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)

                sb.AppendLine(string.Join(columnSeperator, fileInformation[i]));

            File.AppendAllText(csvFile, sb.ToString());
        }
    }
}