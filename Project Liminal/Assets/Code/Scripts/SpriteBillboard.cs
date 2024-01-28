using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    public bool freezeXZAxis = true;

    void Update() {

        if (freezeXZAxis) {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        } else {
            transform.rotation = Camera.main.transform.rotation;
        }

    }
}
