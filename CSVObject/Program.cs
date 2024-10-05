using CSVObject.File;

CsvConverter csvConverter = new();
string[] temp = csvConverter.GetAllCsvFiles();
List<List<DynamicRow>> information = new();
foreach (string filepath in temp)
{
    information.Add(csvConverter.ReadCsvFile(filepath));
}



var informatio1 = information.Count;
Console.WriteLine(informatio1);




