using System;
using System.IO;
using System.Text;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            TestData testData = new TestData();
            testData.Name = "Testing Test You know";
            testData.Input = "SELECT & FROM HUJETA";
            string test = @$"
[Fact]
public async Task {testData.Name.Replace(" ", string.Empty)}()";
            string test1 = @"
[Fact]
public async Task @Name()
{
    _output.WriteLine(""Configuration:"" + Environment.NewLine + JsonConvert.SerializeObject(_fixture.Configuration, Formatting.Indented));

    await new DatabaseRestore(_fixture)
        .Execute();

    await new SqlScript(_fixture)
        .WithConfig(new SqlScriptConfig
        {
            Script = @""
                @Input"",
        })
        .Execute();
}";
            StringBuilder sb = new StringBuilder(test1);
            sb.Replace("@Name", testData.Name.Replace(" ", string.Empty));
            sb.Replace("@Input", testData.Input);
            Console.WriteLine(sb);
            Console.WriteLine(sb.Capacity);

            try
            {
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(testData.Name))
                {
                    File.Delete(testData.Name);
                }

                // Create a new file     
                using (StreamWriter fs = File.CreateText($".{testData.Name}.cs"))
                {
                    // Add some text to file    
                    fs.Write(sb.ToString());

                }

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
            Console.ReadLine();


        }
    }

    public class TestData {
        public string Name { get; set; }
        public string Input { get; set; }

    }
}
