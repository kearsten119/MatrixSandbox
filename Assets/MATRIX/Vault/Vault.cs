using UnityEngine;

public class Vault : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] Material VaultHandleSignMaterial;
    [SerializeField] Texture2D pull;
    [SerializeField] Texture2D turn;
    [SerializeField] Texture2D noPull;
    [SerializeField] Texture2D noTurn;
    [SerializeField] Texture2D blank;


    void Start()
    {
        VaultHandleSignMaterial.mainTexture = blank;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) { VaultHandleSignMaterial.mainTexture = blank; }
        if (Input.GetKeyDown(KeyCode.Alpha1)) { VaultHandleSignMaterial.mainTexture = pull; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { VaultHandleSignMaterial.mainTexture = turn; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { VaultHandleSignMaterial.mainTexture = noPull; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { VaultHandleSignMaterial.mainTexture = noTurn; }
    }
}
