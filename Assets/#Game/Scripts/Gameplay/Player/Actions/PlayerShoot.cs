using UnityEngine;
using ProjetoIA.GOAP;

namespace ProjetoIA {

    public class PlayerShoot : PlayerAction {

        [SerializeField] private float shootRange;

        public override Vector3 GetActionLocation() {
            return Player.transform.position;
        }

        public override bool RunAction() {
            // use gun to kill enemy

            return false;
        }

        protected override void BuildExpectedEffects() {
            expectedEffects = new WorldKnowledge { { bWorldInfo.EnemyTargetKill, true } };
        }

        protected override void BuildPreconditions() {
            precondition = (expectedEffects) => {
                if (expectedEffects.GetWorldKnowledge(bWorldInfo.PlayerAlive, out bool playerAlive) &&
                    expectedEffects.GetWorldKnowledge(fWorldInfo.EnemyTargetDistance, out float targetDistance)) {
                    if (playerAlive && targetDistance <= shootRange) return true;
                }
                return false;
            };
        }
    }

}
