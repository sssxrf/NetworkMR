using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGoogleDrive;
using static System.Net.Mime.MediaTypeNames;


public class testImage : MonoBehaviour
{

    [SerializeField]
    public Texture2D image;
    public RawImage finaldownload;
    public byte[] downloadedcontent;

    public Material gunMaterial; // Gun material
    public string imageID = "163jGcabQHcPwzbXIXQkwmkRkLZgedJ8s";
    private string purpleID = "163jGcabQHcPwzbXIXQkwmkRkLZgedJ8s";
    private string pinkID = "1VTPvqAYfpJTvx26W0gIIvvFavHYnhNez";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(downloadFromDrive());
        }
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
