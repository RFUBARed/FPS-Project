using UnityEngine;

public class UI_Manager : MonoBehaviour
{

    public static UI_Manager Instance;

    public GameObject hitUI;

    private void Awake()
    {
        Instance = this;
    }

    public void InstantiateHitUI() 
    {
        Instantiate(hitUI, transform);
    }

}
