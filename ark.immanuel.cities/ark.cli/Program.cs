using System;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    static void TrasnfromToIndividaulFile()
    {
        string inputFilePath = "./IN/IN.txt"; // Path to the input file
        string outputDirectory = "./IN"; // Directory to store output files

        // Create the output directory if it doesn't exist
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        // Dictionary to hold StreamWriters for each alphabet
        var writers = new Dictionary<char, StreamWriter>();

        try
        {
            using (var reader = new StreamReader(inputFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Split the line by tab character
                    string[] columns = line.Split('\t');

                    if (columns.Length > 1) // Ensure there is a second column
                    {
                        string secondValue = columns[1].Trim(); // Get the second value
                        if (!string.IsNullOrEmpty(secondValue))
                        {
                            // Get the first character of the second value
                            char firstChar = secondValue[0];

                            // Convert to uppercase to handle both lowercase and uppercase
                            firstChar = char.ToUpper(firstChar);

                            // Check if the first character is a letter
                            if (char.IsLetter(firstChar))
                            {
                                // Get or create the StreamWriter for the corresponding alphabet
                                if (!writers.ContainsKey(firstChar))
                                {
                                    string outputFilePath = Path.Combine(outputDirectory, $"{firstChar}.txt");
                                    writers[firstChar] = new StreamWriter(outputFilePath, append: true);
                                }

                                // Write the line to the appropriate file
                                writers[firstChar].WriteLine(line);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // Close all StreamWriters
            foreach (var writer in writers.Values)
            {
                writer.Close();
            }
        }

        Console.WriteLine("File segregation completed.");
    }
    public static void ConvertCsvToTab(string inputFilePath, string outputFilePath, char csvDelimiter = ',', bool hasHeader = true)
    {
        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Error: Input file '{inputFilePath}' not found.");
            return;
        }

        try
        {
            using (StreamReader reader = new StreamReader(inputFilePath))
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                string line;
                int lineNumber = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;

                    // Skip header if specified
                    if (hasHeader && lineNumber == 1)
                    {
                        writer.WriteLine(string.Join('\t', ParseCsvLine(line, csvDelimiter)));
                        continue;
                    }

                    string[] fields = ParseCsvLine(line, csvDelimiter);
                    writer.WriteLine(string.Join('\t', fields));
                }
            }

            Console.WriteLine($"Successfully converted '{inputFilePath}' to '{outputFilePath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Parses a single CSV line, handling quoted fields.
    /// </summary>
    /// <param name="line">The CSV line to parse.</param>
    /// <param name="delimiter">The delimiter character for the CSV.</param>
    /// <returns>An array of strings representing the fields in the line.</returns>
    private static string[] ParseCsvLine(string line, char delimiter)
    {
        var fields = new System.Collections.Generic.List<string>();
        bool inQuote = false;
        StringBuilder currentField = new StringBuilder();

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                if (inQuote && i + 1 < line.Length && line[i + 1] == '"')
                {
                    // Escaped double quote (e.g., "hello ""world""")
                    currentField.Append('"');
                    i++; // Skip the next quote
                }
                else
                {
                    inQuote = !inQuote;
                }
            }
            else if (c == delimiter && !inQuote)
            {
                fields.Add(currentField.ToString());
                currentField.Clear();
            }
            else
            {
                currentField.Append(c);
            }
        }
        fields.Add(currentField.ToString()); // Add the last field

        return fields.ToArray();
    }
    static void Main(string[] args)
    {
        //TrasnfromToIndividaulFile();
        ConvertCsvToTab(@"C:\Immi\Personal\git_hub\ir-dev\ark-citites\ark.immanuel.cities\ark.immanuel.cities.web\data\pincode\in\in.csv", @"C:\Immi\Personal\git_hub\ir-dev\ark-citites\ark.immanuel.cities\ark.immanuel.cities.web\data\pincode\in\in.txt");
    }
}