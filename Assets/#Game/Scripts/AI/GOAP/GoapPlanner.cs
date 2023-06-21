using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public class GoapPlanner
    {
        /// <summary>
        /// Encapsula a acao com informacoes relevantes para o grafo
        /// </summary>
        private class ActionNode
        {
            public List<ActionNode> Connections; // This node is solved by these other nodes
            public GoapAction Action { get; private set; }

            public ActionNode(GoapAction action)
            {
                Action = action;
            }
        }
        /// <summary>
        /// Encapsula o action node com informacoes relevantes para o plano atual
        /// </summary>
        private class PlanNode
        {
            public int gNCost;
            public int hNCost;

            public List<ActionNode> Connections { get => Node.Connections; } 
            public ActionNode Node { get; private set; }
            public PlanNode Parent { get; private set; }
            public bool IsStartNode { get => Parent == null; }
            public int fNCost { get => gNCost + hNCost; }

            public PlanNode(ActionNode node, GoapGoal goal, PlanNode parent = null)
            {
                if (node == null) { Debug.LogError("PlanNode Nulo!!"); return; }

                Node = node;

                gNCost = node.Action.GetActionCost();
                if (parent != null) gNCost += parent.gNCost;

                hNCost = CalcDiferences(node.Action.GetExpectedEffects(), goal.GetObjective());
            }

            //public void SortNodes()
            //{
            //    connections.Sort((x, y) => x.Action.GetActionCost().CompareTo(y.Action.GetActionCost())); // Sort by cost (min -> max)
            //}
        }
        private ActionNode[] graph;

        public GoapPlanner(GoapAction[] allActions)
        {
            BuildGraph(allActions);
        }

        // Create all nodes, Calculate all nodes 'solve-by' relations
        private void BuildGraph(GoapAction[] allActions)
        {
            graph = new ActionNode[allActions.Length];

            for (int i = 0; i < allActions.Length; i++) graph[i] = new ActionNode(allActions[i]);
            for (int i = 0; i < allActions.Length; i++) // j is solved by i
            {
                for (int j = 0; j < allActions.Length; j++)
                {
                    if (i == j) continue;
                    if (allActions[i].IsValid(allActions[j].GetExpectedEffects())) graph[j].Connections.Add(graph[i]); // j is solved by i
                }
            }
        }

        public Queue<GoapAction> Plan(IGoapWorldKnowledge worldKnowledge, GoapGoal goal)
        {
            return AStar(worldKnowledge, goal); 
        }

        private Queue<GoapAction> AStar(IGoapWorldKnowledge currentWorldState, GoapGoal goal)
        {
            Queue<GoapAction> plan = new Queue<GoapAction>();
            List<PlanNode> frontier = new List<PlanNode>();
            List<PlanNode> visitedNodes = new List<PlanNode>();
            PlanNode expansionNode = null;
            bool planNotFound = true;

            foreach (ActionNode node in graph)
            {
                if (node.Action.IsValid(currentWorldState))
                {
                    if (CalcDiferences(node.Action.GetExpectedEffects(), goal.GetObjective()) == 0)
                    {
                        return new Queue<GoapAction>(new GoapAction[] { node.Action });
                    }
                    else
                    {
                        expansionNode = new PlanNode(node, goal);
                        visitedNodes.Add(expansionNode);
                        foreach (ActionNode connection in expansionNode.Connections)
                        {
                            frontier.Add(new PlanNode(connection, goal, expansionNode));
                        }
                    }
                }
            }

            while (frontier.Count > 0 && planNotFound)
            {
                expansionNode = frontier[0];
                for(int i = 0; i < frontier.Count; i++)
                {
                    if (CalcDiferences(frontier[i].Node.Action.GetExpectedEffects(), goal.GetObjective()) == 0)
                    {
                        if (!planNotFound && frontier[i].fNCost < expansionNode.fNCost) expansionNode = frontier[i];
                        else
                        {
                            planNotFound = false;
                            expansionNode = frontier[i];
                        }
                    }
                    else if (planNotFound && frontier[i].fNCost < expansionNode.fNCost) expansionNode = frontier[i];
                }

                visitedNodes.Add(expansionNode);
                frontier.Remove(expansionNode);

                if (planNotFound)
                {
                    foreach (ActionNode connection in expansionNode.Connections)
                    {
                        frontier.Add(new PlanNode(connection, goal, expansionNode));
                    }
                }
            }

            if (planNotFound) { Debug.LogWarning("Plan not found!"); return null; }

            while (expansionNode != null)
            {
                plan.Enqueue(expansionNode.Node.Action);
                expansionNode = expansionNode.Parent;
            }
            return new Queue<GoapAction>(plan.Reverse());
        }
        //TODO - Wont work for 1 value
        /*
        private List<ActionNode> Dijkstra(List<ActionNode> goalNodes, IGoapWorldKnowledge worldKnowledge)
        {
            List<ActionNode> plan = new List<ActionNode>();

            if (goalNodes.Count == 0) return plan;

            ActionNode lastNode = null;
            List<ActionNode> currentNodes = goalNodes;
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
                        lowestCostNodes[i] = currentNodes[i].gNCost. + currentNodes[i].Action.GetActionCost();
                        continue;
                    }
                    testCost = 0;
                    foreach (ActionNode test in currentNodes[i].Connections)
                    {
                        testCost = currentNodes[i].gNCost + currentNodes[i].Action.GetActionCost() + test.Action.GetActionCost();
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

                lastWorldKnowledge = lastNode.Action.GetExpectedEffects();
                lastNode = currentNodes[lowestCostNode];
                currentNodes = lastNode.Connections;
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
        */
        private static int CalcDiferences(IReadOnlyWorldKnowledge baseDict, IReadOnlyWorldKnowledge goal)
        {
            int remainingStates = 0;
            bool bValue;
            float fValue;
            int eValue;

            foreach (KeyValuePair<bWorldInfo, bool> kvp in (IReadOnlyDictionary<bWorldInfo, bool>)goal)
            {
                if (!(baseDict.TryGetValue(kvp.Key, out bValue) && bValue == kvp.Value))
                {
                    remainingStates++;
                }
            }
            foreach (KeyValuePair<eWorldInfo, int> kvp in (IReadOnlyDictionary<eWorldInfo, int>)goal)
            {
                if (!(baseDict.TryGetValue(kvp.Key, out eValue) && eValue == kvp.Value))
                {
                    remainingStates++;
                }
            }
            foreach (KeyValuePair<fWorldInfo, float> kvp in (IReadOnlyDictionary<fWorldInfo, float>)goal)
            {
                if (!(baseDict.TryGetValue(kvp.Key, out fValue) && fValue == kvp.Value))
                {
                    remainingStates++;
                }
            }

            return remainingStates;
        }
    }
}
