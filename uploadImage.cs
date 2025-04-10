using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using NativeFilePickerNamespace;

public class ImageUploader : MonoBehaviour
{
    public RawImage img;
    public Material teacherMaterial;
    private string imageName = "teacher.png";

    void Start()
    {
        RequestPermissionAsynchronously(false);
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (img == null) Debug.LogError("RawImage (img) is null in Awake!");
        else Debug.Log("RawImage (img) is assigned in Awake: " + img.name);
    }

    private async void RequestPermissionAsynchronously(bool readPermissionOnly = false)
    {
        NativeFilePicker.Permission permission = await NativeFilePicker.RequestPermissionAsync(readPermissionOnly);
        Debug.Log("Permission result: " + permission);
    }

    public void OpenImageFile()
    {
        string[] fileTypes = new string[] { "image/*" };
        //string[] fileTypes = new string[] { "public.image" }; //That's for ios no need for it for now

        NativeFilePicker.PickFile((path) =>
        {
            if (path == null)
            {
                Debug.Log("Operation cancelled");
                return;
            }

            Debug.Log("Picked file: " + path);
            byte[] bytes = File.ReadAllBytes(path);

            string newPath = Path.Combine(Application.persistentDataPath, imageName);
            File.WriteAllBytes(newPath, bytes);

            Texture2D texture = new Texture2D(1, 1);
            texture.filterMode = FilterMode.Trilinear;
            texture.LoadImage(bytes);

            if (img != null)
            {
                img.texture = texture;
            }
            else
            {
                Debug.Log("raw image is dumb");
            }
            //teacherMaterial.mainTexture = texture;
            if (teacherMaterial != null)
            {
                teacherMaterial.mainTexture = texture;
            }
            else
            {
                Debug.LogError("teacherMaterial is not assigned in the Inspector!");
            }
            /*if (teacherMaterial != null)
            {
                teacherMaterial.mainTexture = texture;
            }
            else
            {
                Debug.LogError("teacherMaterial is not assigned!");
            }*/
        }, fileTypes);
    }
}
