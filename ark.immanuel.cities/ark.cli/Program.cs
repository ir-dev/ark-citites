using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
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
}