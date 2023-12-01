var input = File.ReadAllLines("input.txt");

var calibration = input.Select(l => l.Select(c =>
                                             {
                                                 if (int.TryParse(c.ToString(), out var number))
                                                 {
                                                     return (int?)number;
                                                 }

                                                 return null;
                                             })
                                     .Where(c => c is not null))
                       .Select(l => int.Parse(l.First().ToString() + l.Last()))
                       .Sum();

Console.WriteLine($"PartOne: {calibration}");

var mappings = new Dictionary<string, int>
              {
                  { "one", 1 },
                  { "two", 2 },
                  { "three", 3 },
                  { "four", 4 },
                  { "five", 5 },
                  { "six", 6 },
                  { "seven", 7 },
                  { "eight", 8 },
                  { "nine", 9 }
              };

var correctCalibrationValue = input.Select(l =>
                                           {
                                               int matchedIndex;
                                               
                                               for (var position = 0; position < l.Length; position = matchedIndex)
                                               {
                                                   matchedIndex = l.Length;
                                                   var matchedWord = string.Empty;
                                                   
                                                   foreach (var mapping in mappings)
                                                   {
                                                       var index = l.IndexOf(mapping.Key, position, StringComparison.InvariantCultureIgnoreCase);
                                                       if (index == -1 || index >= matchedIndex)
                                                       {
                                                           continue;
                                                       }

                                                       matchedIndex = index;
                                                       matchedWord = mapping.Key;
                                                   }

                                                   // All matches were replaced
                                                   if (matchedWord == string.Empty)
                                                   {
                                                       break;
                                                   }

                                                   // Replace only first occurence
                                                   l = l[..matchedIndex] + mappings[matchedWord] + l[(matchedIndex + matchedWord.Length)..];
                                               }

                                               return l;
                                           })
                                   .Select(l => l.Select(c =>
                                                         {
                                                             if (int.TryParse(c.ToString(), out var number))
                                                             {
                                                                 return (int?)number;
                                                             }

                                                             return null;
                                                         })
                                                 .Where(c => c is not null))
                                   .Select(l => int.Parse(l.First().ToString() + l.Last()))
                                   .Sum();

Console.WriteLine($"PartTwo: {correctCalibrationValue}");