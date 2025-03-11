using System.Xml.Linq;

namespace ark.immanuel.cities
{
    public class SearchManager
    {
        public static dynamic SearchCities(string country, string searchText)
        {
            country = country.ToLower().Trim();
            char searchLetter = char.ToUpper(searchText[0]);
            string filePath = $"./data/{country}/{searchLetter}.txt";
            var results = new List<dynamic>();
            if (!System.IO.File.Exists(filePath)) return new
            {
                error = true,
                message = $"file {searchLetter}.txt not found"
            };
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] columns = line.Split('\t');
                    //0 - geonameid, 1 - name, 2 - asciiname, 3 - alternatenames, 4 - latitude, 5 - longitude
                    if (columns.Length > 4) // Ensure there are at least 5 columns
                    {
                        string alternate_name = columns[3].Trim();
                        if (!string.IsNullOrEmpty(alternate_name) && alternate_name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                        {
                            results.Add(new
                            {
                                lat = columns[4],
                                lng = columns[5],
                                name = columns[1],
                                ascii_name = columns[2],
                                geonameid = columns[0]
                            });
                        }
                    }
                }
            }

            return new
            {
                error = true,
                message = $"found {results.Count} records",
                data = results
            }; ;
        }
    }
}
