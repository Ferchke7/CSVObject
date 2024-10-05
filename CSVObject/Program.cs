using CSVObject.File;

CsvConverter csvConverter = new();
string[] temp = csvConverter.GetAllCsvFiles();
List<List<DynamicRow>> information = new();
foreach (string filepath in temp)
{
    information.Add(csvConverter.ReadCsvFile(filepath));
}

information.ForEach(x =>
{
    x.ForEach(y =>
    {
        Console.WriteLine(y.fileName);
        Console.WriteLine();
        csvConverter.MoveFileByResult(y.fileName, false);
        Console.WriteLine(y.Fields.Values);
        Console.WriteLine("New val");
    });
});

var informatio1 = information.Count;
Console.WriteLine(informatio1);




