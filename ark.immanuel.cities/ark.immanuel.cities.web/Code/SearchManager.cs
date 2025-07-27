using System.Collections;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace ark.immanuel.cities
{
    public class SearchManager
    {
        public static dynamic SearchCities(string country, string searchText, int limit_count = 10)
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
                    if (results.Count > limit_count) break;
                }
            }

            return new
            {
                error = false,
                message = $"found {results.Count} records",
                data = results
            };
        }
        public static dynamic SearchPincode(string country, string searchText, int limit_count = 10)
        {
            country = country.ToLower().Trim();
            char searchLetter = char.ToUpper(searchText[0]);
            string filePath = $"./data/pincode/{country}/in.txt";
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
                    //circlename,regionname,divisionname,officename,pincode,officetype,delivery,district,statename,latitude,longitude
                    //0 - circlename, 1 - regionname, 2 - divisionname, 3 - officename, 4 - pincode, 5 - officetype, 6 - delivery, 7 - district, 8 - statename, 9 - latitude, 10 - longitude
                    if (columns.Length > 8) // Ensure there are at least 5 columns
                    {
                        string pincode = columns[4].Trim();
                        if (!string.IsNullOrEmpty(pincode) && pincode.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                        {
                            results.Add(new
                            {
                                lat = columns.Length > 9 ? columns[9] : "",
                                lng = columns.Length > 10 ? columns[10] : "",
                                pincode = pincode,
                                state = columns[8],
                                district = columns[7],
                                delivery = columns[6],
                                office_type = columns[5],
                                office_name = columns[3],
                                division_name = columns[2],
                                region_name = columns[1],
                                circle_name = columns[0]
                            });
                        }
                    }
                    if (results.Count > limit_count) break;
                }
            }

            return new
            {
                error = false,
                message = $"found {results.Count} records",
                data = results
            };
        }
        public static dynamic SearchPincodeUnique(string country, string searchText, int limit_count = 10)
        {
            country = country.ToLower().Trim();
            char searchLetter = char.ToUpper(searchText[0]);
            string filePath = $"./data/pincode/{country}/in.txt";
            Hashtable key = new Hashtable();
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
                    //circlename,regionname,divisionname,officename,pincode,officetype,delivery,district,statename,latitude,longitude
                    //0 - circlename, 1 - regionname, 2 - divisionname, 3 - officename, 4 - pincode, 5 - officetype, 6 - delivery, 7 - district, 8 - statename, 9 - latitude, 10 - longitude
                    if (columns.Length > 8) // Ensure there are at least 5 columns
                    {
                        string pincode = columns[4].Trim();
                        string state = columns[8].Trim();
                        string district = columns[7].Trim();
                        if (!string.IsNullOrEmpty(pincode) && pincode.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                        {
                            string k = $"{pincode}-{state}-{district}";
                            if (key.ContainsKey(k)) continue;
                            key.Add(k, k);
                            results.Add(new
                            {
                                lat = columns.Length > 9 ? columns[9] : "",
                                lng = columns.Length > 10 ? columns[10] : "",
                                pincode = pincode,
                                state = columns[8],
                                district = columns[7],
                                delivery = columns[6],
                                office_type = columns[5],
                                office_name = columns[3],
                                division_name = columns[2],
                                region_name = columns[1],
                                circle_name = columns[0]
                            });
                        }
                    }
                    if (results.Count > limit_count) break;
                }
            }

            return new
            {
                error = false,
                message = $"found {results.Count} records",
                data = results
            }; ;
        }
    }
}
