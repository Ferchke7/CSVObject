using CSVObject.File;

CsvConverter csvConverter = new CsvConverter();

List<DynamicRow> list = csvConverter.ReadCsvFile(@"C:\test.csv");
string filePaths = csvConverter.GetListOfCsvFiles(@"C:\");
csvConverter.MoveFileByResult(@"C:\test.csv",false);
//var result = csvConverter.MapTheValue<BasicClassToMap>(list);