using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace QuizApp.Models
{
    public static class TestLoader
    {
        public static List<Test> LoadTests(string PATH)
        {
            var json = File.ReadAllText(PATH);
            return JsonConvert.DeserializeObject<List<Test>>(json);
        }
    }
}
