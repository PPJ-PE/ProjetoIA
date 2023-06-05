using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public class GoapPlanner
    {
        // On start, calculate all nodes 'solve-this' relations

        // Plan will have an objective and current world

        private PlanNode[] graph;

        public GoapPlanner(GoapAction[] allActions) {
            BuildGraph(allActions);
        }

        private void BuildGraph(GoapAction[] allActions) 
        { 
            graph = new PlanNode[allActions.Length];

            List<PlanNode> connections = new List<PlanNode>(); // Remember to reset
            for (int i = 0; i < allActions.Length; i++) graph[i] = new PlanNode(allActions[i]);
            for (int i = 0; i < allActions.Length; i++) // i is solved by j
            {
                for (int j = 0; j < allActions.Length; j++) 
                {
                    if (i == j) continue;
                    if (allActions[i].IsValid(allActions[j].GetExpectedEffects())) graph[i].connections.Add(graph[j]);
                }
            }
        }

        public Queue<GoapAction> Plan(IGoapWorldKnowledge worldKnowledge, GoapGoal goal)
        {
            // 1- Construir lista com todas as actions que atingem a goal
            List<PlanNode> GoalActions = new List<PlanNode>();
            foreach(PlanNode node in graph) if(node.Action.GetExpectedEffects() == goal.Objective()) GoalActions.Add(node);

            // 2- Construir lista com todas as actions que são validas no worldknowledge atual ????

            // 3- Para cada ação que atinge a goal, chamar um metodo que calcula um plano de qualquer ação valida com a atual
            foreach (PlanNode node in GoalActions) 
            {

            }

            return new Queue<GoapAction>(); //
        }

        // If an action has a precondition equal to it's expected effect, this method WILL overflow
        private void AStar() 
        {

        }
        
        class PlanNode 
        {
            public List<PlanNode> connections; // This node solves these other nodes
            public GoapAction Action { get; private set; }

            public PlanNode(GoapAction action) 
            {
                Action = action;
            }
        }
    }
}
