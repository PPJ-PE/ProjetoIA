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
            List<PlanNode> goalActions = new List<PlanNode>();
            foreach (PlanNode node in graph) if (node.Action.GetExpectedEffects() == goal.Objective()) goalActions.Add(node);

            Dijkstra(goalActions);

            return new Queue<GoapAction>(); //
        }

        private List<PlanNode> Dijkstra(List<PlanNode> goalNodes)
        {
            List<PlanNode> plan = new List<PlanNode>();

            PlanNode lastNode;
            List<PlanNode> currentNodes = goalNodes;
            List<int> lowestCostNodes = new List<int>();
            int lowestCostNode = 0;
            do
            {
                // Calculate nodes costs
                for (int i = 0; i < currentNodes.Count; i++)
                {
                    lowestCostNodes.Add(0);
                    if (currentNodes[i].Action.IsValid(lastNode.Action.GetExpectedEffects()))
                    {
                        lowestCostNodes[i] = currentNodes[i].currentComingFromCost + currentNodes[i].Action.GetActionCost();
                        continue;
                    }
                    int testCost;
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
                lastNode = currentNodes[lowestCostNode];
                currentNodes = lastNode.connections;
                lowestCostNodes.Clear();
                lowestCostNode = 0;

                plan.Add(lastNode);

            } while (!lastNode.Action.IsValid(currentNodes));

            return new Queue<PlanNode>(plan); // Needs to invert
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
