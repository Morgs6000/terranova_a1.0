using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class DebugScreen : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textLeft;
    //[SerializeField] private TextMeshProUGUI textRight;

    private string debugTextLeft;
    //private string debugTextRight;

    private float frameRate;
    private float timer;

    [SerializeField] private Transform player;

    [SerializeField] private Transform cam;
    private float rangeHit = 5.0f;
    [SerializeField] private LayerMask groundMask;

    private void Start() {
        
    }

    private void Update() {
        TextLeft();
        //TextRight();
    }

    private void TextLeft() {
        debugTextLeft = "Minecraft Clone 1.4.7";
        debugTextLeft += "\n";
        debugTextLeft += "by Stradivarius Industries";

        debugTextLeft += "\n\n";

        FrameRate();

        debugTextLeft += "\n\n";

        PlayerPosition();
        debugTextLeft += "\n";
        PlayerDirection();
        debugTextLeft += "\n";
        Biome();
        debugTextLeft += "\n";
        PlayerIsGrounded();

        debugTextLeft += "\n\n";

        TargetVoxel();

        textLeft.text = debugTextLeft;
    }

    private void FrameRate() {
        if(timer > 1.0f) {
            frameRate = (int)(1.0f / Time.unscaledDeltaTime);
            timer = 0;
        }
        else {
            timer += Time.deltaTime;
        }

        debugTextLeft += (
            "FPS" + frameRate + "\n" +
            "minFPS" + "\n" +
            "maxFPS"
        );
    }

    private void PlayerPosition() {
        Vector3 playerPos = new Vector3(
            player.position.x - 0.5f,
            player.position.y - 0.5f,
            player.position.z - 0.5f
        );

        Vector3 chunkPos = new Vector3(
            playerPos.x / Chunk.ChunkSizeInVoxels.x,
            playerPos.y / Chunk.ChunkSizeInVoxels.y,
            playerPos.z / Chunk.ChunkSizeInVoxels.z
        );

        debugTextLeft += (
            "X/Y/Z: " + 
            (playerPos.x).ToString("F0") + ", " +
            (playerPos.y - 1.0f).ToString("F0") + ", " +
            (playerPos.z).ToString("F0") + "\n" +

            "Chunk: " + 
            (chunkPos.x).ToString("F0") + ", " +
            //(chunkPos.y).ToString("F0") + " (0)" + ", " +
            (chunkPos.z).ToString("F0")

            /*
            // Posição x do jogador
            "x: " + playerPos.x + 
            // Posição x inteira do jogador
            " (" + (playerPos.x).ToString("F0") + ") // " + 
            // Chunk que o jogador esta pisando em x
            "c: " + (chunkPos.x).ToString("F0") + 
            " (0)" + 
            "\n" +
            
            // Posição y do jogador
            "y: " + playerPos.y +  " (feet pos, " + 
            // Posição y do jogador + altura dos olhos
            (playerPos.y + 1.62f).ToString() + " eyes pos)" + 
            "\n" +
            
            // Posição z do jogador
            "z: " + playerPos.z + 
            // Posição z inteira do jogador
            " (" + (playerPos.z).ToString("F0") + ") // " + 
            // Chunk que o jogador esta pisando em z
            "c: " + (chunkPos.z).ToString("F0") + 
            " (0)"
            */
        );
    }

    private void PlayerDirection() {
        float aguloInclinacao = player.rotation.eulerAngles.y;

        string direction = "NORTH";

        /*
        if(aguloInclinacao < 45) {
            direction = "NORTH";
        }
        else if(aguloInclinacao < 135) {
            direction = "EAST";
        }
        else if(aguloInclinacao < 225) {
            direction = "SOUTH";
        }
        else if(aguloInclinacao < 315) {
            direction = "WEST";
        }
        //*/

        //*
        if(aguloInclinacao < 22.5) {
            direction = "NORTH";
        }
        else if(aguloInclinacao < 67.5) {
            direction = "NORTHEAST";
        }
        else if(aguloInclinacao < 112.5) {
            direction = "EAST";
        }
        else if(aguloInclinacao < 157.5) {
            direction = "SOUTHEAST";
        }
        else if(aguloInclinacao < 202.5) {
            direction = "SOUTH";
        }
        else if(aguloInclinacao < 247.5) {
            direction = "SOUTHWEST";
        }
        else if(aguloInclinacao < 292.5) {
            direction = "WEST";
        }
        else if(aguloInclinacao < 337.5) {
            direction = "NORTHWEST";
        }
        //*/
        
        debugTextLeft += (
            // Direção cardeal que o jogador esta olhando
            "Face : " + direction + " " +
            aguloInclinacao.ToString("F0") + "º"
        );
    }

    private void Biome() {
        debugTextLeft += (
            "Biome: " + "Eternal WindowsXP Hills"
        );
    }

    private void PlayerIsGrounded() {
        bool isGrounded = Movement.isGrounded;
        
        debugTextLeft += (
            // Se o jogador esta pisando no chão
            "isGround: " + isGrounded.ToString().ToLower()
        );
    }

    private void TargetVoxel() {
        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, rangeHit, groundMask)) {
            Vector3 pointPos = hit.point - hit.normal / 2;

            debugTextLeft += (
                "Target Voxel: " + 
                Mathf.FloorToInt(pointPos.x) + ", " +
                Mathf.FloorToInt(pointPos.y) + ", " +
                Mathf.FloorToInt(pointPos.z)
            );
        }
    }

    private void TextRight() {
        /*
        // Cria um objeto PerformanceCounter para acessar o contador de desempenho "Memória total"
        PerformanceCounter memoriaTotalCounter = new PerformanceCounter("Memory", "Total Physical Memory");

        // Obtém a quantidade total de memória em bytes
        long memoriaTotalBytes = memoriaTotalCounter.RawValue;

        // Converte a quantidade de memória para megabytes
        float memoriaTotalMB = memoriaTotalBytes / (1024.0f * 1024.0f);

        //----------

        // Cria um objeto PerformanceCounter para acessar o contador de desempenho "Memória disponível"
        PerformanceCounter memoriaDisponivelCounter = new PerformanceCounter("Memory", "Available MBytes");

        // Obtém a quantidade de memória disponível em megabytes
        float memoriaDisponivelMB = memoriaDisponivelCounter.NextValue();

        // Calcula a quantidade de memória em uso em megabytes
        float memoriaUsadaMB = memoriaTotalMB - memoriaDisponivelMB;

        //----------

        float porcentagemMemoriaUsada = memoriaUsadaMB / memoriaTotalMB * 100.0f;

        //----------
                
        debugTextRight = (
            "Used memory: " + 
            porcentagemMemoriaUsada.ToString("F2") + "% (" +
            memoriaUsadaMB.ToString("F2") + " MB) of " + 
            memoriaTotalMB.ToString("F2") + " MB" + 
            "\n" +
            "Allocated momory: 0% (0MB)"
        );
        */

        /*
        debugTextRight = (
            "Used memory: 0% (0MB) of 0MB" + "\n" +
            "Allocated momory: 0% (0MB)"
        );
        */

        //textRight.text = debugTextRight;
    }
}
