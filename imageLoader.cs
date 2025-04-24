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

    private async void RequestPermissionAsynchronously(bool readPermissionOnly = false)
    {
        NativeFilePicker.Permission permission = await NativeFilePicker.RequestPermissionAsync(readPermissionOnly);
        Debug.Log("Permission result: " + permission);
    }

    public void OpenImageFile()
    {
        string[] fileTypes = new string[] { "image/*" };

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

            if (teacherMaterial != null)
            {
                teacherMaterial.mainTexture = texture;
            }
            else
            {
                Debug.LogError("teacherMaterial is not assigned in the Inspector!");
            }
        }, fileTypes);
    }
}
