using System.Text.RegularExpressions;

namespace SysML_Sync
{
    public class MapperService
    {
        //  In diesem Code verwenden wir reguläre Ausdrücke (Regex.Matches), um alle Übereinstimmungen mit dem Muster
        //  name="..." zu finden. Mit Hilfe der Gruppe (Groups[1]) extrahieren wir nur die Werte, die nach name= stehen.

        private const string FilePath = "D:\\TestDataSysMLMoreComplex.uml.txt";

        public void RunMapper()
        {
            try
            {
                // read file
                string text = File.ReadAllText(FilePath);

                // use regex to find words after "name="
                var matches = Regex.Matches(text, @"name=""([^""]+)""");

                // extract and display matched words with indices
                Console.WriteLine("Words after 'name=' in the Text Document:");
                List<string> words = new List<string>();
                int index = 1;
                foreach (Match match in matches)
                {
                    string word = match.Groups[1].Value;
                    Console.WriteLine($"{index}. {word}");
                    words.Add(word);
                    index++;
                }

                // ask user to choose a word to modify
                Console.Write("\nChoose a word to modify (enter the number): ");
                int selectedIndex = Convert.ToInt32(Console.ReadLine());

                // validate user input
                if (selectedIndex >= 1 && selectedIndex <= words.Count)
                {
                    // prompt user for the new name
                    Console.Write($"\nEnter a new name for '{words[selectedIndex - 1]}': ");
                    string newName = Console.ReadLine();

                    // update the selected word
                    text = Regex.Replace(text, $@"name=""{words[selectedIndex - 1]}""", $@"name=""{newName}""");

                    // write the modified text back to the file
                    File.WriteAllText(FilePath, text);

                    Console.WriteLine($"\nThe name has been changed: {words[selectedIndex - 1]} --> {newName}");
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please enter a valid number.");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error reading/writing file: {e.Message}");
            }
        }
    }
}
