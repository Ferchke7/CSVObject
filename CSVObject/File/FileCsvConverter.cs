namespace CSVObject.File
{
    public interface IFileCsvConverter
    {
        Func<string, List<DynamicRow>> ReadCsvFile { get; set; }
        Func<string, string> GetListOfCsvFiles { get; set; }
    }
    
    public class CsvConverter : IFileCsvConverter
    {
        public Func<string, List<DynamicRow>> ReadCsvFile { get; set; }
        public Func<string, string> GetListOfCsvFiles { get; set; }
        //TODO Add here for working directory , initially I was thinking about passing through validation of the files
        //public string workingFilePath;
        public CsvConverter()
        {
            ReadCsvFile = ReadCsv;
            GetListOfCsvFiles = GetListOfCsv;
        }

        private List<DynamicRow> ReadCsv(string filePath)
        {
            List<DynamicRow> rows = new List<DynamicRow>();
            
            using (StreamReader reader = new StreamReader(filePath))
            {
                
                string? headerLine = reader.ReadLine();
                string[]? headers = headerLine?.Split(',');

                // Read the rest of the data
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] fields = line.Split(',');
                    DynamicRow row = new DynamicRow();
                    row.fileName = filePath;
                    for (int i = 0; i < headers.Length; i++)
                    {
                        row.Fields[headers[i].Trim()] = fields.Length > i ? fields[i] : string.Empty;
                        
                    }

                    rows.Add(row);
                }
            }

            return rows;
        }

        private string GetListOfCsv(string directoryPath)
        {
            var filePaths = Directory.GetFiles(directoryPath, "*_*.csv");
            return string.Join("\n", filePaths);
        }

        public void MoveFileFIFO(string filename, bool isSuccess = true)
        {
            string sourcePath = filename;
            string targetDirectory = isSuccess ? @"C:\success\" : @"C:\failed\";
            string targetPath = Path.Combine(targetDirectory, Path.GetFileName(filename));

            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                using (FileStream destinationStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write))
                {
                    sourceStream.CopyTo(destinationStream);
                    
                }
                
            }
            
        }

        public void DeleteCopiedFiles(string sourcePath)
        {
            FileInfo file = new(sourcePath);
            if (file.Exists)
            {
                file.Delete();
            }
            else
            {
                throw new Exception($"Error file {sourcePath} doesn't exist");
            }
        }
        public void BulkMoveFile(string folderName)
        {
            string[] csvFilesList = GetAllCsvFiles(folderName);
            
            for(int i = 0; i < csvFilesList.Length; i++)
            {
                MoveFileFIFO(csvFilesList[i]);
                DeleteCopiedFiles(csvFilesList[i]);
            }
        }
        public string[] GetAllCsvFiles(string folder = @"C:\")
        {
            string[] csvFiles = Directory.GetFiles(folder, "*.csv");

            foreach (var item in csvFiles)
            {
                Console.WriteLine(item);
            }
            return csvFiles;
        }
        
    }
    
    public class DynamicRow
    {
        public string fileName { get; set; }
        public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();
    }

    public record BasicClassToMap(string lotid, string someinformation, Dictionary<string, object> ValueCollection);

}