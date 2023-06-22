using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public class GoapVision : GoapSenseBase
    {
        [Range(1, 360)]
        [SerializeField] private int fovAngle;
        [Range(2, 100)]
        [SerializeField] private int fovMeshResolution;
        [Range(0.1f, 1000.0f)]
        [SerializeField] private float range;
        [Range(0.0f, 1000.0f)]
        [SerializeField] private float circleRadius;
        [SerializeField] private float meshHeight;

        [SerializeField] private MeshFilter fovMeshFilter;
        [SerializeField] private MeshFilter circleMeshFilter;

        private Vector3[] fovRays;
        private void Awake()
        {
            fovRays = GetFovRays();
            BuildFovMesh();
            BuildCilinderMesh();
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnDrawGizmosSelected()
        {
            
        }
        private void OnDrawGizmos()
        {
            Vector3[] gizmosRays = GetFovRays();
            if (gizmosRays == null) return;
            Gizmos.color = Color.cyan;
            foreach (Vector3 ray in gizmosRays)
            {
                Gizmos.DrawRay(transform.position, transform.TransformVector(ray));
            }
        }
        private Vector3[] GetFovRays()
        {
            Vector3[] rays = new Vector3[fovMeshResolution];
            float angleIncrements = fovAngle / (fovMeshResolution - 1.0f);
            rays[0] = Quaternion.AngleAxis(fovAngle / 2 * -1, Vector3.up) * Vector3.forward * range;
            for (int i = 1; i < fovMeshResolution; i++)
            {
                rays[i] = Quaternion.AngleAxis(angleIncrements, Vector3.up) * rays[i - 1];
            }
            return rays;
        }

        private void BuildFovMesh()
        {
            Mesh fovMesh = new Mesh();
            fovMeshFilter.mesh = fovMesh;

            Vector3[] vertices = new Vector3[(fovMeshResolution + 1) * 2];
            Vector2[] uvs = new Vector2[vertices.Length];
            Vector3 heightVec = new Vector3(0.0f, meshHeight, 0.0f);
            int[] trianglesIndexes = new int[fovMeshResolution * 12];
            int heightVecIndex;
            int i = 0;

            vertices[0] = Vector3.zero;
            vertices[1] = fovRays[0];
            for (i = 1; i < fovRays.Length; i++)
            {
                vertices[i + 1] = fovRays[i];
                trianglesIndexes[(i * 3) - 1] = 0;
                trianglesIndexes[(i * 3) - 2] = i;
                trianglesIndexes[(i * 3) - 3] = i + 1;
            }

            vertices[++i] = heightVec;
            heightVecIndex = i;
            vertices[++i] = heightVec + fovRays[0];

            for (; i < (fovRays.Length * 2) + 1; i++)
            {
                vertices[i + 1] = fovRays[i - fovRays.Length - 1] + heightVec;
                trianglesIndexes[(i * 3) - 9] = heightVecIndex;
                trianglesIndexes[(i * 3) - 8] = i;
                trianglesIndexes[(i * 3) - 7] = i + 1;
            }

            trianglesIndexes[(i * 3) - 9] = 0;
            trianglesIndexes[(i * 3) - 8] = 1;
            trianglesIndexes[(i * 3) - 7] = heightVecIndex;
            i++;
            trianglesIndexes[(i * 3) - 9] = 1;
            trianglesIndexes[(i * 3) - 8] = 1 + heightVecIndex;
            trianglesIndexes[(i * 3) - 7] = heightVecIndex;
            i++;

            trianglesIndexes[(i * 3) - 9] = 0;
            trianglesIndexes[(i * 3) - 8] = heightVecIndex;
            trianglesIndexes[(i * 3) - 7] = 1 + (fovRays.Length * 2);
            i++;
            trianglesIndexes[(i * 3) - 9] = 1 + (fovRays.Length * 2);
            trianglesIndexes[(i * 3) - 8] = fovRays.Length;
            trianglesIndexes[(i * 3) - 7] = 0;

            for (int j = 0; j < fovRays.Length - 1; j++)
            {
                i++;
                trianglesIndexes[(i * 3) - 9] = j + 1;
                trianglesIndexes[(i * 3) - 8] = j + 2;
                trianglesIndexes[(i * 3) - 7] = j + fovRays.Length + 2;
                i++;
                trianglesIndexes[(i * 3) - 9] = j + 2;
                trianglesIndexes[(i * 3) - 8] = j + fovRays.Length + 3;
                trianglesIndexes[(i * 3) - 7] = j + fovRays.Length + 2;
            }


            fovMesh.vertices = vertices;
            fovMesh.triangles = trianglesIndexes;
            fovMesh.uv = uvs;
            fovMesh.RecalculateBounds();
        }
        private void BuildCilinderMesh()
        {

        }
    }
}
