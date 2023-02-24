using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField] private Transform cam;
    private float xRotation = 0;

    [Space(20)]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed;
    private float walkingSpeed = 4.317f;
    
    private Vector3 velocity;    
    private float fallSpeed = -78.4f;
    public static bool isGrounded;
    
    [Space(20)]
    [SerializeField] private Transform groundCheck;
    private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundMask;
    
    private float jumpHeight = 1.2522f;

    [Space(20)]
    [SerializeField] private bool running;
    private float sprintingSpeed = 5.612f;

    private float lastClickTime;
    private const float DOUBLE_CLICK_TIME = 0.2f;

    [SerializeField] private InterfaceManager interfaceManager;
    private bool openMenu;
    private bool openGameMenu;

    private void Start() {
        speed = walkingSpeed;
    }

    private void Update() {
        openMenu = interfaceManager.openMenu;
        openGameMenu = interfaceManager.openGameMenu;

        if(!openGameMenu) {    
            if(!openMenu) {
                Cursor.lockState = CursorLockMode.Locked;

                CameraUpdate();
            
                MovementUpdate();
                JumpUpdate();
                SprintUpdate();
            }    

            FallUpdate();

            StepOffsetUpdate();
        }
        else {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void CameraUpdate() {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        cam.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    private void MovementUpdate() {
        float x = Input.GetAxis("HorizontalAD");
        float z = Input.GetAxis("VerticalWS");

        Vector3 moveDirection = transform.TransformDirection(new Vector3(x, 0.0f, z));

        moveDirection *= speed;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void FallUpdate() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        velocity.y += fallSpeed * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        if(isGrounded && velocity.y < 0) {
            velocity.y = -2.0f;
        }
    }

    private void JumpUpdate() {
        if(isGrounded && Input.GetButton("Space")) {
            isGrounded = false;
            running = false;

            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * fallSpeed);
        }
    }

    private void SprintUpdate() {
        if(running == false) {
            speed = walkingSpeed;
        }
        if(running == true) {
            speed = sprintingSpeed;
        }

        // Pressionar LeftControl corre.
        if(Input.GetButton("LCtrl")) {
            running = true;
        }

        // Pressionar W duas vezes, também faz correr.
        if(Input.GetKeyDown(KeyCode.W)) {
            float timeSinceLastClick = Time.time - lastClickTime;

            if(timeSinceLastClick <= DOUBLE_CLICK_TIME) {
                running = true;
            }
            else {
                running = false;
            }

            lastClickTime = Time.time;
        }

        // Soltar W para de corre.
        if(Input.GetKeyUp(KeyCode.W)) {
            running = false;
        }
    }

    private void StepOffsetUpdate() {
        // Ponto de origem do raio
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;

        // Direção do raio
        Vector3 rayDirection = transform.TransformDirection(Vector3.forward);

        // Distância máxima do raio
        float maxDistance = 0.5f;

        // Tamanho da caixa
        Vector3 boxSize = new Vector3(0.5f, 0.5f, 0.5f);

        RaycastHit hit;

        if(isGrounded) {
            //*
            // Verifica se o raio colide com alguma parede de voxel
            if(Physics.Raycast(rayOrigin, rayDirection, out hit, maxDistance, groundMask)) {
                Vector3 rayOriginUp = transform.position + Vector3.up * 1.1f;

                if (Physics.Raycast(rayOriginUp, rayDirection, maxDistance, groundMask)) {
                    return;
                }
                else {
                    // Se colidir, ajusta a posição do personagem para ficar no topo do voxel
                    transform.position = hit.point + Vector3.up * (characterController.height / 2);
                }
            }
            //*/

            /*
            // Verifica se o raio colide com alguma parede de voxel
            if(Physics.BoxCast(rayOrigin, boxSize, rayDirection, out hit, Quaternion.identity, maxDistance, groundMask)) {
                Vector3 rayOriginUp = transform.position + Vector3.up * 1.1f;

                if (Physics.BoxCast(rayOriginUp, boxSize, rayDirection, Quaternion.identity, maxDistance, groundMask)) {
                    return;
                }
                else {
                    // Se colidir, ajusta a posição do personagem para ficar no topo do voxel
                    transform.position = hit.point + Vector3.up * (characterController.height / 2);
                }
            }
            //*/
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Pig"))
        {
            // Aplique a força no objeto com o qual o jogador está colidindo
            hit.gameObject.GetComponent<CharacterController>().Move(transform.forward * 10 * Time.deltaTime);
        }
    }
}
