// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using CSVObject.File;

Console.WriteLine("Hello, World!");
CsvConverter csvConverter = new CsvConverter();
string[] getAll = csvConverter.GetAllCsvFiles("/home/ferdavs/csvProject/csvfiles");
List<List<DynamicRow>> allinforamtion = new();
foreach(string fileName in getAll) {
    Console.WriteLine("" + fileName);
    allinforamtion.Add(csvConverter.ReadCsvFile(fileName));
}
int number = 1;
allinforamtion.ForEach(x => {
    x.ForEach(u => {
        Console.WriteLine("Name Of File" + u.fileName);
        csvConverter.MoveFileFIFO(u.fileName, number is 1);
        int v = number == 1 ? 0 : 1;
        number = v;
        csvConverter.DeleteCopiedFiles(u.fileName);
    });
});

System.Console.WriteLine("Bye world");