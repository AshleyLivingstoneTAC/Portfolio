using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 100;

    public ShapeSettings shape;
    public ColorSettings color;
    [HideInInspector]
    public bool ssFoldout;
    [HideInInspector]
    public bool csFoldout;
    ShapeGenerator shapeGenerator;

    public float mass;
    public float density;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    private void OnValidate()
    {
        GeneratePlanet();
    }

    void Init()
    {
        shapeGenerator = new ShapeGenerator(shape);
        density = Random.Range(0.001f, 1);
        mass = density * shape.planetRadius;
        if(meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        
        terrainFaces = new TerrainFace[6];

        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

        for(int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
                terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }
    public void GeneratePlanet()
    {
        Init();
        GenerateMesh();
        GenerateColors();
    }
    public void OnColorSettingsUpdated()
    {
        Init();
        GenerateColors();
    }

    public void OnShapeSettingsUpdated()
    {
        Init();
        GenerateMesh();
    }

    void GenerateMesh()
    {
        foreach(TerrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }
    }

    void GenerateColors()
    {
        foreach (MeshFilter mf in meshFilters)
        {
            mf.GetComponent<MeshRenderer>().sharedMaterial.color = color.planetColor;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
