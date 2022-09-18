using System.Linq;
using UnityEngine;

namespace MiniclipTest.Game.Piece
{
    public class AIPieceController : PieceController
    {
        public Transform[] rayPoints;
        
        private static readonly int PieceLayer = 1 << 6;

        public int RaysWithSameDistance()
        {
            int raysCount = rayPoints.Length;
            float[] distance = new float[raysCount];
            for (int i = 0; i < raysCount; i++)
            {
                RaycastHit2D[] rayResults = new RaycastHit2D[1];
                if (Physics2D.RaycastNonAlloc(rayPoints[i].position, rayPoints[i].up, rayResults, PieceLayer) == 0)
                {
                    distance[i] = float.NaN;
                    continue;
                }

                distance[i] = rayResults[0].distance;
            }
            
            var duplicates = distance.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .ToDictionary(x => x.Key, y => y.Count());

            int sum = 0;
            foreach (var duplicate in duplicates)
            {
                sum += duplicate.Value;
            }

            return sum;
        }
    }
}