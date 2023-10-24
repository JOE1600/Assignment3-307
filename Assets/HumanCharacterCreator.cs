using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanCharacterCreator : MonoBehaviour
{


   void Start()
{

    CreateHumanCharacter(); // Create the character's visual representation
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
        head.transform.parent = body.transform;


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

void Update()
    {
    }
}
    