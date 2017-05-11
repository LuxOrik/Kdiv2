using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdiv2
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            for (int limit = 1; limit < 500; limit++)
            {
                for (int page = 0; page < 200; page++)
                {
                    (int, int) sol = Proposition1(page, limit);
                    if (!TestSolution(page, limit, sol))
                        throw new Exception("BOOM");
                    Debug.WriteLine($"{page,3} {limit,3} -> {sol.Item1,3} {sol.Item2,3}");
                }
            }
        }

        // pageR, limR => page et limite to submit
        // pageU, limU => page et lim wanted by user
        // 
        //  | pageR * limR <= pageU * limU
        // {
        //  | (pageR + 1) * limR >= (pageU + 1) * limU
        // 
        //  => 
        //
        //  pageU * limU >= pageR * limR >= pageU * limU + limU - limR
        //
        // tous les xxxU sont connus, donc pour page 8 , limite 4 
        //   32 >= pageR * limR >= 37 - limR
        //
        // On choisir un limitR, on test si il convient (équation marche ET le pageR qui en découle peut être entier)
        // sinon on test le limitR suivant (on commence a limitU + 1, on c'est que ce sera notre minimum de requested)

        private static (int, int) Proposition1(int userPage, int userLimit)
        {
            var sup = userPage*userLimit;
            var inf = (userPage + 1)*userLimit + 1;
            // must : inf >= retPage * retLim >= sup - retlim

            for (int i = userLimit + 1; ; i++)
            {
                var infI = (inf - i)/(double) i;
                var supI = sup/(double) i;
                
                // Pas chercher plus ici, peut être opti pitetre
                var supFloor = Math.Floor(supI);
                if (supFloor >= infI)
                    return ((int) supFloor, i);
                var infCeil = Math.Ceiling(infI);
                if (infCeil <= supI)
                    return ((int) infCeil, i);
            }
        }

        private static bool TestSolution(int userPage, int userLimit, (int, int) solution)
        {
            return userPage*userLimit >= solution.Item1*solution.Item2
                   && userPage*userLimit + userLimit <= solution.Item1*solution.Item2 + solution.Item2;
        }
    }
}
