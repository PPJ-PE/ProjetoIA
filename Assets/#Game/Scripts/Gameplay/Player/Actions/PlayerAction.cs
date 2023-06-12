using ProjetoIA.GOAP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA
{
    public abstract class PlayerAction : GoapAction
    {
        public Player.PFields PFields { protected get; set; }
        public Player Player { protected get; set; }
    }
}
