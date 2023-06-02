using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public static class GoapPlanner
    {
        public static Queue<GoapAction> Plan(GoapAgent agent, 
                                                List<GoapAction> knowledge, // change name for availableActions ? Sim!
                                                IGoapWorldKnowledge worldKnowledge, 
                                                GoapGoal goal)
        {
            List<GoapAction> performableAction = UpdatePerformableActions(knowledge, worldKnowledge);

            List<PlanNode> planNodes = new List<PlanNode>();
            PlanNode start = new PlanNode(null, null);

            BuildGraph(start, planNodes, performableAction, worldKnowledge, goal.Objective());

            PlanNode cheapest = null;
            foreach (PlanNode node in planNodes) 
            {
                if (cheapest == null) cheapest = node;
                else
                {
                    // check if cost is lower than current cheapest
                }
            }
            List<GoapAction> result = PlanNode.GetActionQueue(cheapest);
            Queue<GoapAction> queue = new Queue<GoapAction>();
            foreach (GoapAction action in result) queue.Enqueue(action);

            return queue;
        }

        private static List<GoapAction> UpdatePerformableActions(List<GoapAction> knowledge, IGoapWorldKnowledge worldKnowledge) 
        {
            List<GoapAction> performableActions = new List<GoapAction> ();
            foreach(GoapAction action in knowledge) if(action.IsValid(worldKnowledge) /* expected results */) performableActions.Add(action);
            
            //Debug.LogError("Invalid action sent to plan!");
            return performableActions;
        }

        private static void BuildGraph(PlanNode parent, List<PlanNode> planNodes, List<GoapAction> performableActions, IGoapWorldKnowledge worldKnowledge, WorldKnowledge goal) 
        {
            foreach (GoapAction action in performableActions) 
            {
                if (action.GetExpectedEffects() == goal) 
                {
                    PlanNode node = new PlanNode(parent, action);
                    if (!action.IsValid(worldKnowledge)) 
                    {
                        BuildGraph(node, planNodes, worldKnowledge, goal);
                    }
                }
            }
        }

        class PlanNode {
            public PlanNode parent;
            public GoapAction action;
            public IReadOnlyWorldKnowledge knowledgeAfterAction;

            public PlanNode(PlanNode parent, GoapAction action) 
            {
                this.parent = parent;
                this.action = action;
                knowledgeAfterAction = parent.knowledgeAfterAction; // copy world state after parent action
                // change world state to after THIS action
            }

            public static List<GoapAction> GetActionQueue(PlanNode node) 
            {
                List<GoapAction> actionQueue = new List<GoapAction>();
                AddActionToQueue(node, actionQueue);
                return actionQueue;
            }

            private static void AddActionToQueue(PlanNode node, List<GoapAction> queue) 
            {
                if (node.parent != null) {
                    queue.Insert(0, node.action);
                    AddActionToQueue(node.parent, queue);
                }
            }
        }
    }
}
