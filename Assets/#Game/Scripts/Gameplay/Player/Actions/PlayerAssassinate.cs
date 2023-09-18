using UnityEngine;
using ProjetoIA.GOAP;

namespace ProjetoIA {

    public class PlayerAssassinate : PlayerAction {

        [SerializeField] private float assassinationRange;

        public override Vector3 GetActionLocation() {
            return Player.transform.position;
        }

        public override bool RunAction() {
            // Insta-kill enemy

            return false;
        }

        protected override void BuildExpectedEffects() {
            expectedEffects = new WorldKnowledge { { bWorldInfo.EnemyTargetKill, true } };
        }

        protected override void BuildPreconditions() {
            precondition = (expectedEffects) => {
                if (expectedEffects.GetWorldKnowledge(bWorldInfo.PlayerAlive, out bool playerAlive) &&
                    expectedEffects.GetWorldKnowledge(fWorldInfo.EnemyTargetDistance, out float targetDistance)) {
                    if (playerAlive && targetDistance <= assassinationRange) return true;
                }
                return false;
            };
        }
    }

}
