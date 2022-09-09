using UnityEngine;

namespace Blazewing
{
    public class DataTestBehaviour : MonoBehaviour
    {
        void Start()
        {
            x teste1 = new x()
            {
                xx = 1
            };

            DataController.Add(teste1);

            DataController.Add(teste1);

            y teste2 = new y()
            {
                yy = 2
            };

            DataController.Add(teste2);

            var xx= DataController.Get<x>();
            var yy = DataController.Get<y>();
        }

        public struct x
        {
            public int xx;
        }

        public struct y
        {
            public int yy;
        }
    }
}
