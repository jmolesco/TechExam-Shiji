using Microsoft.EntityFrameworkCore;
using ShijiGroup.Infrastracture.Interface;
using ShijiGroup.Models;
using System.Linq;
using System.Text;
namespace ShijiGroup.Infrastracture.Service
{
    public class WordFinderService : IWordFinderService
    {
        //public string[] CreateBoard()
        //{
        //    var src = new string[]
        //    {
        //        "abcdc",
        //        "fgwio",
        //        "chill",
        //        "pqnsd",
        //        "uvdxy"
        //    };
        //    return src;
        //}

        private readonly WordFinderContext wordFinderContext;
        public WordFinderService(WordFinderContext _wordFinderContext)
        {
            wordFinderContext = _wordFinderContext;
        }
        public async Task<string> GetMatrices()
        {
            //return wordFinderContext.Matrixes.ToListAsync();

            var matrix = await wordFinderContext.Matrixes.Select(m => m.Name).ToListAsync();

            // Assuming each name in the matrix is a single character
            var words = matrix.SelectMany(m => m.ToCharArray()).Distinct().ToArray();

            var formattedMatrix = FormatMatrix(matrix.ToArray(), words);

           return formattedMatrix;
        }
        public async Task<object> FindWords(List<string> inputDictionary)
        {
            var matrixArray = wordFinderContext.Matrixes
                .OrderBy(m => m.Id) // Optional: ensure consistent order
                .Select(m => m.Name)
                .ToArray();

            var wordSet = new HashSet<string>(inputDictionary);
            var wordsFound = new HashSet<string>();

            // Search horizontally
            foreach (var row in matrixArray)
            {
                FindWordsInLine(row, wordSet, wordsFound);
            }

            // Search vertically
            for (int col = 0; col < matrixArray[0].Length; col++)
            {
                var verticalWord = string.Concat(matrixArray.Select(row => row[col]));
                FindWordsInLine(verticalWord, wordSet, wordsFound);
            }

            var countFoundWords = wordsFound.Count;
            if (countFoundWords>0)
                return  new
                {
                    WordsFound = wordsFound.ToList(),
                    Message = String.Format("Great Job! You've found {0} words.", countFoundWords)

                };
            else
                return new
                {
                    Message ="Sorry, no words found.Try again!"

                };

        }



        #region Helper Methods
        private string FormatMatrix(string[] matrix, char[] words)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string row in matrix)
            {
                foreach (char c in row)
                {
                    if (words.Contains(c))
                    {
                        sb.Append($"|{c}|");
                    }
                    else
                    {
                        sb.Append($" {c} ");
                    }
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private void FindWordsInLine(string line, HashSet<string> wordSet, HashSet<string> wordsFound)
        {
            foreach (var word in wordSet)
            {
                if (line.Contains(word))
                {
                    wordsFound.Add(word);
                }
            }
        }
        #endregion
    }

}

