using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CV19Console
{
    internal class Program
    {
        const string DATA_URL = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(DATA_URL, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        static IEnumerable<string> GetDataLines()
        {
            var data_stream = GetDataStream().Result;
            var data_reader = new StreamReader(data_stream);

            while (!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                yield return line.Replace("Korea,", "Korea -");
            }
        }

        static DateTime[] GetDates() => GetDataLines().First().Split(',').Skip(4).Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture)).ToArray();

        static IEnumerable<(string Country, string Province, int[] Counts)> GetData()
        {
            var lines = GetDataLines().Skip(1).Select(line => line.Split(','));

            foreach (var row in lines)
            {
                var province = row[0].Trim();
                var country_name = row[1].Trim(' ','"');
                var counts = row.Skip(4).Select(int.Parse).ToArray();

                yield return (country_name, province, counts);
            }
        }

        static void Main(string[] args)
        {
            //var client = new WebClient();
            //var client = new HttpClient();

            //var response = client.GetAsync(DATA_URL).Result;
            //var csv_str = response.Content.ReadAsStringAsync().Result;

            //foreach (var data_line in GetDataLines())
            //{
            //    Console.WriteLine(data_line);
            //}

            //var dates = GetDates();
            //Console.WriteLine(string.Join("\r\n", dates));

            var russia_data = GetData().First(v => v.Country.Equals("Albania", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(string.Join("\r\n", GetDates().Zip(russia_data.Counts, (date, count) => $"{date:G} - {count}")));

            Console.ReadLine();
        }
    }
}
