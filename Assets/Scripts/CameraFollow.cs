using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Virtual Camera
    public Transform player; // Obiekt, kt�ry kamera ma �ledzi�
    public Vector3 offset = new Vector3(0, 5, -10); // Maksymalne przesuni�cie kamery
    public Vector3 firstPersonOffset = new Vector3(0, 1.5f, 0); // Offset dla kamery pierwszoosobowej (np. przy pe�zaniu)
    public LayerMask collisionLayers; // Warstwy przeszk�d
    public float collisionBuffer = 0.2f; // Minimalna odleg�o�� od przeszk�d
    public float smoothSpeed = 0.125f; // P�ynno�� ruchu kamery
    public float rotationSpeed = 100f; // Szybko�� rotacji kamery

    private Transform cameraTransform; // Transform rzeczywistej kamery
    private Vector3 currentOffset; // Dynamiczne przesuni�cie
    private float currentYaw = 0f; // Aktualny k�t rotacji kamery
    private PlayerMove playerMove; // Referencja do skryptu PlayerMove

    void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("Brak przypisanej Virtual Camera!");
            return;
        }

        // Pobieramy rzeczywisty transform kamery
        cameraTransform = virtualCamera.transform;
        currentOffset = offset;

        // Pobieramy komponent PlayerMove z gracza
        playerMove = player.GetComponent<PlayerMove>();
        if (playerMove == null)
        {
            Debug.LogError("Brak skryptu PlayerMove na obiekcie gracza!");
        }
    }

    void LateUpdate()
    {
        if (player == null || cameraTransform == null)
            return;

        // Sprawdzamy, czy gracz jest w trybie pe�zania
        if (playerMove != null && playerMove.isCrawling)
        {
            // Kamera w trybie pierwszoosobowym
            currentOffset = firstPersonOffset;
            // Kamera patrzy na gracza z poziomu pierwszoosobowego
            cameraTransform.position = player.position + currentOffset;
            cameraTransform.rotation = Quaternion.LookRotation(player.forward, Vector3.up);
        }
        else
        {
            // Kamera w trybie trzecioosobowym
            // Odczytujemy ruch myszy dla rotacji kamery
            float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            currentYaw += horizontalInput;

            // Oblicz now� pozycj� kamery z uwzgl�dnieniem rotacji
            Quaternion rotation = Quaternion.Euler(0, currentYaw, 0);
            Vector3 desiredOffset = rotation * offset;
            Vector3 desiredPosition = player.position + desiredOffset;

            // Sprawd� kolizje mi�dzy graczem a docelow� pozycj� kamery
            Vector3 direction = desiredPosition - player.position;
            float maxDistance = direction.magnitude;

            if (Physics.Raycast(player.position, direction.normalized, out RaycastHit hit, maxDistance, collisionLayers))
            {
                // Je�li napotkano przeszkod�, dostosuj odleg�o��
                float adjustedDistance = Mathf.Max(hit.distance - collisionBuffer, 0);
                currentOffset = direction.normalized * adjustedDistance;
            }
            else
            {
                // Brak przeszk�d � powr�t do maksymalnego offsetu
                currentOffset = Vector3.Lerp(currentOffset, desiredOffset, smoothSpeed);
            }

            // Aktualizuj pozycj� kamery
            Vector3 smoothedPosition = Vector3.Lerp(cameraTransform.position, player.position + currentOffset, smoothSpeed);
            cameraTransform.position = smoothedPosition;

            // Kamera zawsze patrzy na gracza
            cameraTransform.LookAt(player.position + Vector3.up * 1.5f);
        }
    }
}
