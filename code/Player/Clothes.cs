using Sandbox;

namespace ssl.Player
{
    public class Clothes : ModelEntity
    {
        public Clothes() { }

        public Clothes(string clothesModel)
        {
            EnableShadowInFirstPerson = true;
            EnableHideInFirstPerson = true;
            SetupModel(clothesModel);
        }

        private void SetupModel(string clothesModel)
        {
            AddCollisionLayer(CollisionLayer.Debris);
            SetModel(clothesModel);
        }
        
    }
}