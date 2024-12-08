namespace ShijiGroup.Models
{
    public class DataSeeder
    {
        private readonly WordFinderContext wordFinderContext;

        public DataSeeder(WordFinderContext _wordFinderContext
            )
        {
            wordFinderContext = _wordFinderContext;
        }

        public void Seed()
        {
            var matrix = wordFinderContext.Matrixes.ToList();

            //If count is 0 then create the matrix
            if(matrix.Count() == 0)
            {
                var boardMatrix = new List<Matrix>()
                {
                    new Matrix()
                    {
                        Id=1,
                        Name="abcdc"
                    },
                    new Matrix()
                    {
                        Id=2,
                        Name="fgwio"
                    },
                    new Matrix()
                    {
                        Id=3,
                        Name="chill"
                    },
                    new Matrix()
                    {
                        Id=4,
                        Name="pqnsd"
                    },
                    new Matrix()
                    {
                        Id=5,
                        Name="uvdxy"
                    }

                };
                wordFinderContext.Matrixes.AddRange(boardMatrix);
                wordFinderContext.SaveChanges();
            }
        }

    }
}
