using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGoogleDrive;


public class testImage : MonoBehaviour
{

    [SerializeField]
    public Texture2D image;
    public RawImage finaldownload;
    public byte[] downloadedcontent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            downloadTextures();
        }
    }


    public void downloadTextures()
    {
        print("begin download!");
        var request = GoogleDriveFiles.Download("1gMi9FEi9idpEpCGpAxog-DLamxKEQskN");
        print(request.IsError);
        print(request.ResponseData.Content);

        downloadedcontent = request.ResponseData.Content;
        Texture2D tex = new Texture2D(1024, 1024);
        tex.LoadImage(downloadedcontent);
        tex.Apply();
        finaldownload.texture = tex;
       
    }
    public IEnumerator downloadFromDrive()
    {
        print("begin download!");
        var request = GoogleDriveFiles.Download("1gMi9FEi9idpEpCGpAxog-DLamxKEQskN");
        yield return request.Send();
        print(request.IsError);
        print(request.ResponseData.Content);

        downloadedcontent = request.ResponseData.Content;
        Texture2D tex = new Texture2D(1024, 1024);
        tex.LoadImage(downloadedcontent);
        tex.Apply();
        finaldownload.texture = tex;   

    }
}
