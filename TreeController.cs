using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public Transform TreesArea;
    public ParticleSystem ptTrees;

    private const float distance = 5;
    private const int maxTrees = 25000;

    private Vector3[,] grid2D;    
    private float startX;
    private float startZ;
    private float stepX;
    private float stepZ;
    private bool enable;
    private int gridSize;
    private Camera mainCamera;
    private Vector3 prevCameraPos;
    private List<Vector3> positions = new List<Vector3>();
    private ParticleSystem.Particle[] trees = new ParticleSystem.Particle[maxTrees];

    void Start()
    {
        mainCamera = Camera.main;

        Init();
        Load();
    }

    void Update()
    {
        if (!enable)
            return;

        if ((prevCameraPos - mainCamera.transform.position).sqrMagnitude > 5 )
        {
            prevCameraPos = mainCamera.transform.position;
            GetCameraRect();
        }
    }

    void Init()
    {
        Mesh mesh = TreesArea.GetComponent<MeshFilter>().mesh;
        Bounds box = mesh.bounds;

        float sizeX = box.max.x - box.min.x;
        float sizeZ = box.max.z - box.min.z;

        int countX = (int)(sizeX / distance);
        int countZ = (int)(sizeZ / distance);

        int count = Mathf.Max(countX, countZ);

        stepX = sizeX * 1f / count;
        stepZ = sizeZ * 1f / count;

        startX = box.min.x;
        startZ = box.min.z;
    }

    void GetCameraRect()
    {
        int mask = 1 << 6;

        Ray bottomLeftRay = mainCamera.ViewportPointToRay(Vector3.zero);
        Ray topRightRay = mainCamera.ViewportPointToRay(Vector3.one);
        Ray bottomRightRay = mainCamera.ViewportPointToRay(Vector3.right);
        Ray topLeftRay = mainCamera.ViewportPointToRay(Vector3.up);

        if (Physics.Raycast(topRightRay, out RaycastHit topRightRH, Mathf.Infinity, mask) &&
            Physics.Raycast(bottomLeftRay, out RaycastHit bottomLeftRH, Mathf.Infinity, mask) &&
            Physics.Raycast(bottomRightRay, out RaycastHit bottomRightRH, Mathf.Infinity, mask) &&
            Physics.Raycast(topLeftRay, out RaycastHit topLeftRH, Mathf.Infinity, mask))
        {
            bottomLeftRH.point = new Vector3(topLeftRH.point.x, 0, bottomLeftRH.point.z);
            bottomRightRH.point = new Vector3(topRightRH.point.x, 0, bottomRightRH.point.z);

            float width = Mathf.Abs(bottomRightRH.point.x - bottomLeftRH.point.x);
            float height = Mathf.Abs(bottomRightRH.point.z - topRightRH.point.z);
            int countX = (int)(width / stepX);
            int countZ = (int)(height / stepZ);

            int startI = (int)((bottomLeftRH.point.x - startX) / stepX);
            int startJ = (int)((bottomLeftRH.point.z - startZ) / stepZ);

            positions.Clear();

            Vector3 p = grid2D[startI, startJ];

            for (int i = startI; i < startI + countX; i++)
            {
                for (int j = startJ; j < startJ + countZ; j++)
                {
                    if (i >= gridSize) i = gridSize - 1;
                    if (j >= gridSize) j = gridSize - 1;

                    Vector3 pos = grid2D[i, j];
                    positions.Add(pos);
                }
            }

            SetPoints(positions);
        }
    }

    void Load()
    {
        string treesFile = Application.streamingAssetsPath + "/TreeData.bin";

        byte[] bytes = File.ReadAllBytes(treesFile);

        byte[] data = new byte[4];
        byte[] vector = new byte[4 * 3];

        int length = bytes.Length / 12;

        gridSize = (int)Mathf.Sqrt(length);

        grid2D = new Vector3[gridSize, gridSize];

        for (int i = 0; i < length; i++)
        {
            Array.Copy(bytes, 12 * i, vector, 0, 12);

            Vector3 v3;

            Array.Copy(vector, 0, data, 0, 4);
            v3.x = BitConverter.ToSingle(data, 0);

            Array.Copy(vector, 4, data, 0, 4);
            v3.y = BitConverter.ToSingle(data, 0);

            Array.Copy(vector, 8, data, 0, 4);
            v3.z = BitConverter.ToSingle(data, 0);

            int x = i / gridSize;
            int y = i % gridSize;

            grid2D[x, y] = v3;
        }
    }    

    void SetPoints(List<Vector3> positions)
    {
        int length = Mathf.Min(maxTrees, positions.Count);
        for (int i = 0; i < length; ++i)
        {
            trees[i].startColor = Color.white;
            float treeSize = positions[i].y;
            trees[i].startSize = treeSize;
            trees[i].position = new Vector3(positions[i].x, treeSize * 0.5f, positions[i].z);
        }

        ptTrees.SetParticles(trees, trees.Length);
    }

    public void Show()
    {
        enable = true;
    }

    public void Hide()
    {
        enable = false;
        positions.Clear();
        ptTrees.Clear();
    }
}
