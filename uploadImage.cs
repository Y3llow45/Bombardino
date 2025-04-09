using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ImageUploader : MonoBehaviour
{
    public Material teacherMaterial;

    public void UploadImage()
    {
        Texture2D texture = Resources.Load<Texture2D>("teacher");
        if (texture != null)
        {
            teacherMaterial.mainTexture = texture;
        }
        else
        {
            Debug.LogError("Could not load teacher texture from Resources.");
        }
    }
}