using System.Reflection;
using CBH.Core.Collision;
using CBH.Core.Entity.Input;
using CBH.UI.Game.UI.Game;
using UnityEngine;

namespace CBH.Injection
{
    public class TutorialInstaller : BaseSceneInstaller
    {
        [SerializeField] private RocketController rocketController;
        [SerializeField] private FinishCollisionObject landingPad;

        protected override Assembly UiAssembly => typeof(GameAssemblyPlaceholder).Assembly;

        public override void InstallBindings()
        {
            base.InstallBindings();
        }
    }
}