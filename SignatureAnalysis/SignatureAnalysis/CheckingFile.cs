using System;
using System.IO;

namespace SignatureAnalysis
{
    class CheckingFile
    {
        public void CheckFile(string directory, string csvFile)
        {
            foreach (string path in Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories))
            {
                if (File.Exists(path)) 

                    ProcessFile(path, csvFile); 

                else if (Directory.Exists(path)) 
                    
                    CheckFile(path, csvFile); 
            }
        }

      
        public void ProcessFile(string path, string csvFile)
        {
            CreateFile createFile = new CreateFile();

            string fileSignature = createFile.GetFileSignature(path); 
            
            string fileType = createFile.GetFileType(fileSignature);

            string hash = createFile.createMD5Hash(path); 

            createFile.createFile(path, fileType, hash, csvFile);  
        }
    }
}
