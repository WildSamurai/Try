using System;


namespace AnagramApp
{
    public class Anagram
    {
        public string Reverse(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null");

            if (string.IsNullOrEmpty(input))
                return input;

            var words = input.Split(' ');
            var reversedWords = new string[words.Length];

            for (int i = 0; i < words.Length; i++)
            {
                reversedWords[i] = ReverseWord(words[i]);
            }

            return string.Join(" ", reversedWords);
        }

        public string ReverseWord(string word)
        {
            if (string.IsNullOrEmpty(word))
                return word;

            char[] result = word.ToCharArray();
            int left = 0;
            int right = word.Length - 1;

            while (left < right)
            {
                
                if (!char.IsLetter(result[left]))
                {
                    left++;
                    continue;
                }

                
                if (!char.IsLetter(result[right]))
                {
                    right--;
                    continue;
                }

                
                char temp = result[left];
                result[left] = result[right];
                result[right] = temp;

                left++;
                right--;
            }

            return new string(result);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var anagram = new Anagram();

            Console.WriteLine("Enter a string to reverse(or 'exit' to quit):");

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                if (input?.ToLower() == "exit")
                    break;

                try
                {
                    string reversed = anagram.Reverse(input);
                    Console.WriteLine($"Result: {reversed}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}