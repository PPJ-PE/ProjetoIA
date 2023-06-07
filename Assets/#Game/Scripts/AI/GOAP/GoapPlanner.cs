using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public class GoapPlanner
    {
        private PlanNode[] graph;

        public GoapPlanner(GoapAction[] allActions)
        {
            BuildGraph(allActions);
        }

        // Create all nodes, Calculate all nodes 'solve-by' relations
        private void BuildGraph(GoapAction[] allActions)
        {
            graph = new PlanNode[allActions.Length];

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
            List<PlanNode> goalNodes = new List<PlanNode>();
            foreach (PlanNode node in graph) if (node.Action.GetExpectedEffects() == goal.GetObjective()) goalNodes.Add(node);

            List<PlanNode> inversePlan = Dijkstra(goalNodes, worldKnowledge);

            Queue<GoapAction> planQueue = new Queue<GoapAction>();
            for (int i = inversePlan.Count - 1; i >= 0; i--) planQueue.Enqueue(inversePlan[i].Action);

            return planQueue; 
        }

        private List<PlanNode> AStar(List<PlanNode> goalNodes, IGoapWorldKnowledge worldKnowledge)
        {
            List<PlanNode> plan = new List<PlanNode>();


            return plan;
        }
        private List<PlanNode> Dijkstra(List<PlanNode> goalNodes, IGoapWorldKnowledge worldKnowledge)
        {
            List<PlanNode> plan = new List<PlanNode>();

            PlanNode lastNode = null;
            List<PlanNode> currentNodes = goalNodes;
            List<int> lowestCostNodes = new List<int>();
            int lowestCostNode = 0;
            int testCost; // 
            IGoapWorldKnowledge lastWorldKnowledge;

            int debugCounter = 0; //
            do
            {
                // Calculate nodes costs
                for (int i = 0; i < currentNodes.Count; i++)
                {
                    lowestCostNodes.Add(0);
                    if (currentNodes[i].Action.IsValid(lastNode == null ?  worldKnowledge : lastNode.Action.GetExpectedEffects()))
                    {
                        lowestCostNodes[i] = currentNodes[i].currentComingFromCost + currentNodes[i].Action.GetActionCost();
                        continue;
                    }
                    testCost = 0;
                    foreach (PlanNode test in currentNodes[i].connections)
                    {
                        testCost = currentNodes[i].currentComingFromCost + currentNodes[i].Action.GetActionCost() + test.Action.GetActionCost();
                        if (lowestCostNodes[i] == 0 || lowestCostNodes[i] > testCost)
                        {
                            //
                            lowestCostNodes[i] = testCost;
                        }
                    }
                }
                // Select node with lowest cost
                for(int i = 0; i < lowestCostNodes.Count; i++)
                {
                    if (i == 0) continue;
                    if (lowestCostNodes[lowestCostNode] > lowestCostNodes[i]) lowestCostNode = i;
                }

                // Setup for loop
                lastWorldKnowledge = lastNode.Action.GetExpectedEffects();
                lastNode = currentNodes[lowestCostNode];
                currentNodes = lastNode.connections;
                lowestCostNodes.Clear();
                lowestCostNode = 0;

                plan.Add(lastNode);

                if(debugCounter++ > 1000) //
                {
                    Debug.LogWarning("Failed to build a plan, more than 1000 iterations ocurred");
                    return null;
                }

            } while (!lastNode.Action.IsValid(lastWorldKnowledge));

            return plan; // Inverted
        }

        class PlanNode
        {
            public List<PlanNode> connections; // This node is solved by these other nodes
            public GoapAction Action { get; private set; }

            public PlanNode currentComingFrom;
            public int currentComingFromCost;

            public PlanNode(GoapAction action)
            {
                Action = action;
            }

            //public void SortNodes()
            //{
            //    connections.Sort((x, y) => x.Action.GetActionCost().CompareTo(y.Action.GetActionCost())); // Sort by cost (min -> max)
            //}
        }
    }
}
