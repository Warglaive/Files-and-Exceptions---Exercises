using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace _04.Re_Directory
{
   public class Program
    {
       public static void Main()
        {
            var input = Directory.GetFiles("input");
            foreach (var filesx in input)
            {
                var extensions = filesx.Split(new[] { '.', '\\' }
                , StringSplitOptions.RemoveEmptyEntries)
                .ToList();

                var files = (from file in Directory.EnumerateFiles(filesx)
                             where extensions.Contains(Path.GetExtension(file), StringComparer.InvariantCultureIgnoreCase) // comment this out if you don't want to filter extensions
                             select new
                             {
                                 Source = file,
                                 //Destination = Path.Combine(targetPath, Path.GetFileName(file))
                             });
            }
        }
    }
}