using Sandbox;

using TerryBros.Settings;

namespace TerryBros.LevelElements
{
    //TODO: Properly add Blockmodels per folder and dont add it by file derived of this abstract class
    public abstract class BlockModelEntity : BlockEntity
    {
        public virtual string ModelName => "models/blocks/block.vmdl";

        /// <summary>
        /// In case the model doesnt contain a Material use the given material as override.
        /// </summary>
        public virtual bool UseMaterial => false;
        [Predicted]
        public override Vector3 Position
        {
            get { return base.Position + GlobalSettings.BlockSize / 2 * GlobalSettings.UpwardDir; }
            set { base.Position = value - GlobalSettings.BlockSize / 2 * GlobalSettings.UpwardDir; }
        }
        public BlockModelEntity() : base()
        {
            //TODO: Check why SceneObject is sometimes not instantiated
            // or do it in a later call to be sure
            if (Host.IsClient && UseMaterial && SceneObject != null)
            {
                SceneObject.SetMaterialOverride(Material.Load(MaterialName));
            }
        }
        public override void Spawn()
        {
            SetModel(ModelName);
            SetupPhysicsFromModel(PhysicsMotionType);

            Scale = GlobalSettings.BlockSize / WorldSpaceBounds.Size.y;

            base.Spawn();
        }
    }
}
