namespace MiniclipTest.Game.Events
{
    public struct OnTowerHeightChanged
    {
        public string towerOwnerId;
        public float towerHeight;

        public OnTowerHeightChanged(string towerOwnerId, float towerHeight)
        {
            this.towerOwnerId = towerOwnerId;
            this.towerHeight = towerHeight;
        }
    }
}