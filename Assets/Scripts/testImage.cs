using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGoogleDrive;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using TMPro;


public class testImage : MonoBehaviour
{

    [SerializeField]
    public Texture2D image;
    public RawImage finaldownload;
    private byte[] downloadedcontent;

    public TMP_Text synchronizeData_text;
    public Material gunMaterial; // Gun material
    public string imageID = "163jGcabQHcPwzbXIXQkwmkRkLZgedJ8s";
    private string purpleID = "163jGcabQHcPwzbXIXQkwmkRkLZgedJ8s";
    private string pinkID = "1VTPvqAYfpJTvx26W0gIIvvFavHYnhNez";
    private string txtID = "105YojQii8c6tjd_VfRu4dveGmhA3tKOx";


    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine that updates the file every 0.5 seconds
        StartCoroutine(UpdateTextFileRoutine(txtID));
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    StartCoroutine(uploadTextDrive());
        //}

        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    StartCoroutine(UpdateTextFile("105YojQii8c6tjd_VfRu4dveGmhA3tKOx"));
        //}


    }


    public void downloadTextures()
    {
        //print("begin download!");
        //var request = GoogleDriveFiles.Download("1gMi9FEi9idpEpCGpAxog-DLamxKEQskN");
        //print(request.IsError);
        //print(request.ResponseData.Content);

        //downloadedcontent = request.ResponseData.Content;
        //Texture2D tex = new Texture2D(1024, 1024);
        //tex.LoadImage(downloadedcontent);
        //tex.Apply();
        //finaldownload.texture = tex;
        StartCoroutine(downloadFromDrive());

    }


    public IEnumerator uploadTextDrive()
    {
        var content = Encoding.ASCII.GetBytes("downloadedcontent");
        var file = new UnityGoogleDrive.Data.File() { Name = "740statusTest.txt", Content = content };
        var request = GoogleDriveFiles.Create(file);
        request.Fields = new List<string> { "id" };
        yield return request.Send();
        print(request.IsError);



    }

    // Coroutine that repeatedly calls UpdateTextFile()
    IEnumerator UpdateTextFileRoutine(string fileId)
    {
        while (true)
        {
            // Call your update coroutine and wait until it completes
            yield return StartCoroutine(UpdateTextFile(fileId));
            // Wait for 0.5 seconds before doing the next update
            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator UpdateTextFile(string fileId)
    {
        // Prepare your new text content and convert it to a byte array.
        var updatedContent = Encoding.ASCII.GetBytes(synchronizeData_text.text);

        // Create a new file instance with the updated content.
        // (Optionally, include other fields such as Name if you want to update metadata as well.)
        var updatedFile = new UnityGoogleDrive.Data.File()
        {
            Content = updatedContent,
            // Name = "740statusTest.txt", // include if you need to change or reaffirm the filename
        };

        // Use the Update method provided by the UnityGoogleDrive API.
        // Pass in the file ID of the file to be updated and the new file object.
        var request = GoogleDriveFiles.Update(fileId, updatedFile);

        // Optional: specify what fields you want returned from the update.
        request.Fields = new List<string> { "id" };

        // Send the request.
        yield return request.Send();

        // Check if there was an error.
        if (request.IsError)
        {
            Debug.LogError("Error updating file: " + request.Error);
        }
        else
        {
            Debug.Log("File updated successfully!");
        }
    }


    public void downloadAndLoadPurple()
    {
        downloadTexturesByID(purpleID);

    }

    public void downloadAndLoadPink()
    {
        downloadTexturesByID(pinkID);

    }

    private void downloadTexturesByID(string id)
    {
        //print("begin download!");
        //var request = GoogleDriveFiles.Download("1gMi9FEi9idpEpCGpAxog-DLamxKEQskN");
        //print(request.IsError);
        //print(request.ResponseData.Content);

        //downloadedcontent = request.ResponseData.Content;
        //Texture2D tex = new Texture2D(1024, 1024);
        //tex.LoadImage(downloadedcontent);
        //tex.Apply();
        //finaldownload.texture = tex;
        StartCoroutine(downloadFromDrivebyID(id));

    }

    

    public IEnumerator downloadFromDrive()
    {
        print("begin download!");
        var request = GoogleDriveFiles.Download(imageID);
        yield return request.Send();
        print(request.IsError);
        print(request.ResponseData.Content);

        downloadedcontent = request.ResponseData.Content;
        Texture2D tex = new Texture2D(1024, 1024);
        tex.LoadImage(downloadedcontent);
        tex.Apply();
        finaldownload.texture = tex;
        changeMainMap(tex);

    }

    public IEnumerator downloadFromDrivebyID(string id)
    {
        print("begin download!");
        var request = GoogleDriveFiles.Download(id);
        yield return request.Send();
        print(request.IsError);
        print(request.ResponseData.Content);

        downloadedcontent = request.ResponseData.Content;
        Texture2D tex = new Texture2D(1024, 1024);
        tex.LoadImage(downloadedcontent);
        tex.Apply();
        finaldownload.texture = tex;
        changeMainMap(tex);

    }

    private void changeMainMap(Texture2D map)
    {

        // Assign to material's Albedo (Main Map)
        if (gunMaterial != null)
        {
            gunMaterial.mainTexture = map; // or gunMaterial.SetTexture("_MainTex", tex);
            Debug.Log("Texture successfully applied to material!");
        }
        else
        {
            Debug.LogWarning("Target material not assigned.");
        }
    }
}
