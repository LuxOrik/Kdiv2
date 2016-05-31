using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdiv2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            for (int limit = 1; limit < 50; limit++)
            {
                for (int page = 0; page < 20; page++)
                {
                    Tuple<int, int> sol = Test1(page, limit);
                    if (!TestSolution(page, limit, sol))
                        throw new Exception("BOOM");
                    Debug.WriteLine($"{page,3} {limit,3} -> {sol.Item1,3} {sol.Item2,3}");
                }
            }
        }

        private static Tuple<int, int> Test1(int userPage, int userLimit)
        {
            var sup = userPage*userLimit;
            var inf = (userPage + 1)*userLimit + 1;
            // must : inf >= retPage * retLim >= sup - retlim

            for (int i = userLimit + 1; ; i++)
            {
                var infI = (inf - i)/(double) i;
                var supI = sup/(double) i;

                var supFloor = Math.Floor(supI);
                if (supFloor >= infI)
                    return new Tuple<int, int>((int) supFloor, i);
                var infCeil = Math.Ceiling(infI);
                if (infCeil <= supI)
                    return new Tuple<int, int>((int) infCeil, i);
            }
        }

        private static bool TestSolution(int userPage, int userLimit, Tuple<int, int> solution)
        {
            return userPage*userLimit >= solution.Item1*solution.Item2
                   && userPage*userLimit + userLimit <= solution.Item1*solution.Item2 + solution.Item2;
        }
    }
}
