using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 1f;
    public float teleportPosition = -6.3f;
    public Transform[] backgroundTeleportTargets; // Los tres puntos de teletransporte

    void Update()
    {
        // Mover el fondo
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // Teletransportar el fondo si se sale de la pantalla
        if (transform.position.x <= teleportPosition)
        {
            TeleportBackground();
        }
    }

    void TeleportBackground()
    {
        // Teletransportar el fondo a uno de los puntos de teletransporte aleatoriamente
        int randomIndex = Random.Range(0, backgroundTeleportTargets.Length);
        transform.position = backgroundTeleportTargets[randomIndex].position;
    }
}
