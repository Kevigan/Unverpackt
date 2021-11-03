using TMPro;
using UnityEngine;

public class FloatingScore : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    // Start is called before the first frame update
    private void Update()
    {
        transform.position = transform.position + new Vector3(1,0)*Time.deltaTime*speed;
    }
}
