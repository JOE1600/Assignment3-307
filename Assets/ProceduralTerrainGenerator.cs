using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTerrainGenerator : MonoBehaviour
{
    public int terrainWidth = 100; // Adjust the width of the terrain
    public int terrainLength = 100; // Adjust the length of the terrain
    public float maxHeight = 10f; // Adjust the maximum height of the terrain
    public Material terrainMaterial; // Assign your terrain material here

    private int treeType;










    void Start()
    {
        // Create a new Terrain GameObject
        Terrain terrain = gameObject.AddComponent<Terrain>();
        TerrainData terrainData = new TerrainData();

        // Set the terrain size
        terrainData.heightmapResolution = terrainWidth + 1;
        terrainData.size = new Vector3(terrainWidth, maxHeight, terrainLength);

        // Generate terrain heights procedurally
        float[,] heights = GenerateTerrainHeights(terrainWidth + 1, terrainLength + 1);
        terrainData.SetHeights(0, 0, heights);

        // Assign the terrain data to the terrain
        terrain.terrainData = terrainData;

        // Assign the terrain material to the terrain
        terrain.materialType = Terrain.MaterialType.Custom;
        terrain.materialTemplate = terrainMaterial; // Assign your terrain material here



        TerrainCollider terrainCollider = GetComponent<TerrainCollider>();
        if (terrainCollider != null)
        {
        // Access and use terrainCollider here
        }
        else
        {
            // Handle the case when TerrainCollider is missing
        }


        
 

        

        CreateTrees();
        CreateMountains();
        

        CreateHumanCharacter();
   






    }

    private float[,] GenerateTerrainHeights(int width, int length)
    {
        // Implement your terrain height generation logic here
        // You can use Perlin noise or other algorithms to create terrain shapes
        float[,] heights = new float[width, length];

        // Example: Set terrain heights using Perlin noise
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                float xCoord = (float)x / width * 10f;
                float zCoord = (float)z / length * 10f;
                heights[x, z] = Mathf.PerlinNoise(xCoord, zCoord);
            }
        }

        return heights;
    }

    void CreateTrees()
    {
        for (int i = 0; i < 50; i++) // Increased the number of trees to 50
        {
            treeType = Random.Range(0, 3); // Randomly select tree type

            // Create the main trunk (cylinder)
            GameObject trunk = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            trunk.transform.position = new Vector3(Random.Range(0, terrainWidth), 0f, Random.Range(0, terrainLength));
            trunk.transform.localScale = new Vector3(0.5f, Random.Range(2f, 5f), 0.5f); // Thinner trunk

            // Set the trunk's material to brown
            Renderer trunkRenderer = trunk.GetComponent<Renderer>();
            if (trunkRenderer != null)
            {
                Material trunkMaterial = new Material(Shader.Find("Standard"));
                trunkMaterial.color = new Color(0.6f, 0.3f, 0.1f); // Brown
                trunkRenderer.material = trunkMaterial;
            }

            // Create branches (smaller cylinders) and attach them to the trunk
            for (int j = 0; j < 3; j++)
            {
                GameObject branch = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                branch.transform.position = trunk.transform.position + new Vector3(0f, Random.Range(1f, 3f), 0f);
                branch.transform.localScale = new Vector3(0.2f, Random.Range(1f, 3f), 0.2f); // Thinner branches

                // Set the branch's material to brown
                Renderer branchRenderer = branch.GetComponent<Renderer>();
                if (branchRenderer != null)
                {
                    Material branchMaterial = new Material(Shader.Find("Standard"));
                    branchMaterial.color = new Color(0.6f, 0.3f, 0.1f); // Brown
                    branchRenderer.material = branchMaterial;
                }

                // Make branches children of the trunk
                branch.transform.parent = trunk.transform;

                // Create leaves (green or yellow spheres) and attach them to the branches
                for (int k = 0; k < 10; k++)
                {
                    GameObject leaf = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    leaf.transform.position = branch.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1f, 3f), Random.Range(-0.5f, 0.5f));
                    leaf.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

                    // Set the leaf's material color based on tree type
                    Renderer leafRenderer = leaf.GetComponent<Renderer>();
                    if (leafRenderer != null)
                    {
                        Material leafMaterial = new Material(Shader.Find("Standard"));
                        Color leafColor = (treeType == 2) ? Color.yellow : Color.green; // Yellow leaves for treeType 2, green for others

                        leafMaterial.color = leafColor;
                        leafRenderer.material = leafMaterial;
                    }

                    // Attach a LineRenderer from leaf to the trunk
                   /* LineRenderer lineRenderer = leaf.AddComponent<LineRenderer>();
                    lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                    lineRenderer.startColor = Color.brown;
                    lineRenderer.endColor = Color.brown;
                    lineRenderer.startWidth = 0.05f;
                    lineRenderer.endWidth = 0.05f;
                    lineRenderer.SetPosition(0, trunk.transform.position);
                    lineRenderer.SetPosition(1, leaf.transform.position);*/



                    LineRenderer lineRenderer = leaf.AddComponent<LineRenderer>();
                    lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                    lineRenderer.startColor = new Color(139f / 255f, 69f / 255f, 19f / 255f); // Brown color
                    lineRenderer.endColor = new Color(139f / 255f, 69f / 255f, 19f / 255f); // Brown color
                    lineRenderer.startWidth = 0.05f;
                    lineRenderer.endWidth = 0.05f;
                    lineRenderer.SetPosition(0, trunk.transform.position);
                    lineRenderer.SetPosition(1, leaf.transform.position);


                    // Make leaves children of the branches
                    leaf.transform.parent = branch.transform;
                }
            }
        }
    }





    void CreateMountains()
{
    Vector3[] mountainPositions = new Vector3[]
    {
        new Vector3(10f, 0f, 20f),
        new Vector3(30f, 0f, 40f),
        new Vector3(50f, 0f, 60f),
        new Vector3(70f, 0f, 80f),
        new Vector3(90f, 0f, 100f)
    };

    for (int i = 0; i < mountainPositions.Length; i++)
    {
        // Create the mountain (cube) at specific positions
        GameObject mountain = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mountain.transform.position = mountainPositions[i];
        mountain.transform.localScale = new Vector3(10f, Random.Range(5f, 15f), 10f);

        // Set the mountain's material to grey
        Renderer mountainRenderer = mountain.GetComponent<Renderer>();
        if (mountainRenderer != null)
        {
            Material greyMaterial = new Material(Shader.Find("Standard"));
            greyMaterial.color = Color.gray; // Grey color
            mountainRenderer.material = greyMaterial;
        }

        // Create trees on the mountain
        CreateTreesOnMountain(mountain.transform);
    }
}



void CreateTreesOnMountain(Transform mountainTransform)
{
    for (int i = 0; i < 10; i++)
    {
        // Create the tree trunk (cylinder)
        GameObject trunk = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        trunk.transform.position = mountainTransform.position + new Vector3(Random.Range(-4f, 4f), 0f, Random.Range(-4f, 4f));
        trunk.transform.localScale = new Vector3(0.5f, Random.Range(2f, 5f), 0.5f); // Thinner trunk

        // Set the trunk's material to brown
        Renderer trunkRenderer = trunk.GetComponent<Renderer>();
        if (trunkRenderer != null)
        {
            Material brownMaterial = new Material(Shader.Find("Standard"));
            brownMaterial.color = new Color(0.6f, 0.3f, 0.1f); // Custom brown color
            trunkRenderer.material = brownMaterial;
        }

        // Create branches (smaller cylinders)
        for (int j = 0; j < 3; j++)
        {
            GameObject branch = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            branch.transform.position = trunk.transform.position + new Vector3(0f, Random.Range(1f, 3f), 0f);
            branch.transform.localScale = new Vector3(0.2f, Random.Range(1f, 3f), 0.2f); // Thinner branches

            // Set the branch's material to brown
            Renderer branchRenderer = branch.GetComponent<Renderer>();
            if (branchRenderer != null)
            {
                Material brownMaterial = new Material(Shader.Find("Standard"));
                brownMaterial.color = new Color(0.6f, 0.3f, 0.1f); // Custom brown color
                branchRenderer.material = brownMaterial;
            }

            // Create leaves (green or yellow spheres)
            for (int k = 0; k < 10; k++)
            {
                GameObject leaf = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                leaf.transform.position = trunk.transform.position + new Vector3(0f, 2f, 0f); // Attach leaves to the trunk
                leaf.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

                // Set the leaf's material color to brown
                Renderer leafRenderer = leaf.GetComponent<Renderer>();
                if (leafRenderer != null)
                {
                    Material leafMaterial = new Material(Shader.Find("Standard"));
                    leafMaterial.color = new Color(0.6f, 0.3f, 0.1f); // Brown color for leaves
                    leafRenderer.material = leafMaterial;
                }
            }
        }
    }
}







void CreateHumanCharacter()
{
    // Create the main body (capsule)
    GameObject body = GameObject.CreatePrimitive(PrimitiveType.Capsule);
    body.transform.position = new Vector3(0f+86.5f, 0.5f+1.1f, 0f+63.9f); // Adjust the body's position
    
    body.transform.localScale = new Vector3(0.5f, 1.0f, 0.5f);
    body.name = "HumanBody"; // Set a name for the body

    // Set the body's material to a flesh color
    Renderer bodyRenderer = body.GetComponent<Renderer>();
    if (bodyRenderer != null)
    {
        Material bodyMaterial = new Material(Shader.Find("Standard"));
        bodyMaterial.color = new Color(1f, 0.75f, 0.6f); // Flesh color
        bodyRenderer.material = bodyMaterial;
    }

 










    // Create a head (sphere) and attach it to the body
    GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    head.transform.position = new Vector3(0f +86.5f , 1.7f+1.1f, 0f+63.9f); // Adjust the head's position
    head.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    head.name = "Head";

    // Set the head's material to a skin color
    Renderer headRenderer = head.GetComponent<Renderer>();
    if (headRenderer != null)
    {
        Material headMaterial = new Material(Shader.Find("Standard"));
        headMaterial.color = new Color(1f, 0.87f, 0.77f); // Skin color
        headRenderer.material = headMaterial;
    }

    // Create eyes (black spheres) and attach them to the head
    CreateEye(new Vector3(-0.1f +86.5f , 1.75f+1.1f, 0.2f+63.9f), head.transform);
    CreateEye(new Vector3(0.1f+86.5f, 1.75f+1.1f, 0.2f+63.9f), head.transform);

    // Create a mouth (curved black cylinder) and attach it to the head
    CreateMouth(new Vector3(0f+86.5f, 1.6f+1.1f, 0.2f+63.9f), head.transform);

    // Create a neck and attach it to the body
    GameObject neck = CreateNeck(new Vector3(0f+86.5f, 1.5f+1.1f, 0f+63.9f), body.transform);

    // Create shoulders (spheres) and attach them to the body
    GameObject leftShoulder = CreateShoulder(new Vector3(-0.2f+86.5f, 1.25f+1.1f, 0f+63.9f), body.transform);
    GameObject rightShoulder = CreateShoulder(new Vector3(0.2f+86.5f, 1.25f+1.1f, 0f+63.9f), body.transform);

    // Create arms (cylinders) with rotations and attach them to the shoulders
    GameObject leftArm = CreateArm(new Vector3(-0.25f+86.5f, 1.236f+1.1f, 0f+63.9f), true, leftShoulder.transform, new Vector3(0f, 0f, 45f));
    GameObject rightArm = CreateArm(new Vector3(0.25f+86.5f, 1.236f+1.1f, 0f+63.9f), false, rightShoulder.transform, new Vector3(0f, 0f, -45f));





    // Create hips (spheres) and attach them to the body
    GameObject leftHip = CreateHip(new Vector3(-0.2f+86.5f, 0.1f+1.1f, 0f+63.9f), body.transform);
    GameObject rightHip = CreateHip(new Vector3(0.2f+86.5f, 0.1f+1.1f, 0f+63.9f), body.transform);

    // Create legs (cylinders) and attach them to the hips
    CreateLeg(new Vector3(-0.25f+86.5f, -0.6f+1.1f, 0f+63.9f), leftHip.transform);
    CreateLeg(new Vector3(0.25f+86.5f, -0.6f+1.1f, 0f+63.9f), rightHip.transform);





}























GameObject CreateEye(Vector3 position, Transform parent)
{
    GameObject eye = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    eye.transform.position = position;
    eye.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
    eye.name = "Eye"; // Set a name for the eye

    // Set the eye's material to black
    Renderer eyeRenderer = eye.GetComponent<Renderer>();
    if (eyeRenderer != null)
    {
        Material eyeMaterial = new Material(Shader.Find("Standard"));
        eyeMaterial.color = Color.black;
        eyeRenderer.material = eyeMaterial;
    }

    // Attach the eye to the parent (head)
    eye.transform.parent = parent;

    return eye;
}

GameObject CreateMouth(Vector3 position, Transform parent)
{
    GameObject mouth = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
    mouth.transform.position = position;
    mouth.transform.localScale = new Vector3(0.05f, 0.05f, 0.15f);
    mouth.name = "Mouth"; // Set a name for the mouth

    // Set the mouth's material to black
    Renderer mouthRenderer = mouth.GetComponent<Renderer>();
    if (mouthRenderer != null)
    {
        Material mouthMaterial = new Material(Shader.Find("Standard"));
        mouthMaterial.color = Color.black;
        mouthRenderer.material = mouthMaterial;
    }

    // Attach the mouth to the parent (head)
    mouth.transform.parent = parent;

    return mouth;
}


GameObject CreateArm(Vector3 position, bool isLeftArm, Transform parent, Vector3 rotation)
{
    GameObject arm = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
    arm.transform.position = position;
    arm.transform.localScale = new Vector3(0.1f, 0.6f, 0.1f);
    arm.name = isLeftArm ? "LeftArm" : "RightArm"; // Set a name for the arm

    // Set the arm's material to a skin color
    Renderer armRenderer = arm.GetComponent<Renderer>();
    if (armRenderer != null)
    {
        Material armMaterial = new Material(Shader.Find("Standard"));
        armMaterial.color = new Color(1f, 0.87f, 0.77f); // Skin color
        armRenderer.material = armMaterial;
    }

    // Attach the arm to the parent (shoulder) and apply rotation
    arm.transform.parent = parent;
    arm.transform.localRotation = Quaternion.Euler(rotation);

    return arm;
}


GameObject CreateNeck(Vector3 position, Transform parent)
{
    GameObject neck = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
    neck.transform.position = position;
    neck.transform.localScale = new Vector3(0.05f, 0.2f, 0.05f);
    neck.name = "Neck"; // Set a name for the neck

    // Set the neck's material to a skin color
    Renderer neckRenderer = neck.GetComponent<Renderer>();
    if (neckRenderer != null)
    {
        Material neckMaterial = new Material(Shader.Find("Standard"));
        neckMaterial.color = new Color(1f, 0.87f, 0.77f); // Skin color
        neckRenderer.material = neckMaterial;
    }

    // Attach the neck to the parent (upper body)
    neck.transform.parent = parent;

    return neck;
}

GameObject CreateShoulder(Vector3 position, Transform parent)
{
    GameObject shoulder = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    shoulder.transform.position = position;
    shoulder.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
    shoulder.name = "Shoulder"; // Set a name for the shoulder

    // Set the shoulder's material to a skin color
    Renderer shoulderRenderer = shoulder.GetComponent<Renderer>();
    if (shoulderRenderer != null)
    {
        Material shoulderMaterial = new Material(Shader.Find("Standard"));
        shoulderMaterial.color = new Color(1f, 0.87f, 0.77f); // Skin color
        shoulderRenderer.material = shoulderMaterial;
    }

    // Attach the shoulder to the parent (upper body)
    shoulder.transform.parent = parent;

    return shoulder;
}

GameObject CreateHip(Vector3 position, Transform parent)
{
    GameObject hip = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    hip.transform.position = position;
    hip.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
    hip.name = "Hips"; // Set a name for the hip

    // Set the hip's material to a skin color
    Renderer hipRenderer = hip.GetComponent<Renderer>();
    if (hipRenderer != null)
    {
        Material hipMaterial = new Material(Shader.Find("Standard"));
        hipMaterial.color = new Color(1f, 0.87f, 0.77f); // Skin color
        hipRenderer.material = hipMaterial;
    }

    // Attach the hip to the parent (lower body)
    hip.transform.parent = parent;

    return hip;
}


void CreateLeg(Vector3 position, Transform parent)
{
    GameObject leg = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
    leg.transform.position = position;
    leg.transform.localScale = new Vector3(0.15f, 0.6f, 0.15f);
    leg.name = "Leg"; // Set a name for the leg

    // Set the leg's material to a skin color
    Renderer legRenderer = leg.GetComponent<Renderer>();
    if (legRenderer != null)
    {
        Material legMaterial = new Material(Shader.Find("Standard"));
        legMaterial.color = new Color(1f, 0.87f, 0.77f); // Skin color
        legRenderer.material = legMaterial;
    }

    // Attach the leg to the parent (lower body)
    leg.transform.parent = parent;
}







}

