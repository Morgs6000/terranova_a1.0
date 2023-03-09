using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class DebugScreen : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI text1;

    private string debugText;
    private string debugText1;

    private float frameRate;
    private float timer;

    [SerializeField] private Transform player;

    private string direction;

    [SerializeField] private Transform cam;
    private float rangeHit = 5.0f;
    [SerializeField] private LayerMask groundMask;

    private void Start() {
        
    }

    private void Update() {
        Text();
        Text1();
    }

    private void Text() {
        debugText = "Terranova | version a1.0";
        debugText += "\n";
        debugText += "by Stradivarius Industries";

        debugText += "\n\n";

        FrameRate();

        debugText += "\n\n";

        PlayerPosition();
        debugText += "\n";
        PlayerDirection();
        debugText += "\n";
        Biome();
        debugText += "\n";
        PlayerIsGrounded();

        debugText += "\n\n";

        TargetVoxel();

        text.text = debugText;
    }

    private void FrameRate() {
        if(timer > 1.0f) {
            frameRate = (int)(1.0f / Time.unscaledDeltaTime);
            timer = 0;
        }
        else {
            timer += Time.deltaTime;
        }

        debugText += (
            frameRate + " fps" + "\n" +
            "0" + " fpsMin" + "\n" +
            "0" + " fpsMax"
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

        debugText += (
            "XYZ: " + 
            (playerPos.x).ToString("F0") + ", " +
            (playerPos.y - 1.0f).ToString("F0") + ", " +
            (playerPos.z).ToString("F0") + "\n" +

            "Chunk: " + 
            (chunkPos.x).ToString("F0") + ", " +
            //(chunkPos.y).ToString("F0") + ", " +
            (chunkPos.z).ToString("F0")
        );
    }

    private void PlayerDirection() {
        float aguloInclinacao = player.rotation.eulerAngles.y;

        if(aguloInclinacao < 45) {
            direction = "north (positive Z)";
        }
        else if(aguloInclinacao < 135) {
            direction = "east (positive X)";
        }
        else if(aguloInclinacao < 225) {
            direction = "south (negative Z)";
        }
        else if(aguloInclinacao < 315) {
            direction = "west (negative X)";
        }
        
        debugText += (
            // Direção cardeal que o jogador esta olhando
            "Facing : " + direction /*+ " " +
            aguloInclinacao.ToString("F0") + "º"*/
        );
    }

    private void Biome() {
        debugText += (
            "Biome: " + "Eternal WindowsXP Hills"
        );
    }

    private void PlayerIsGrounded() {
        bool isGrounded = Movement.isGrounded;
        
        debugText += (
            // Se o jogador esta pisando no chão
            "isGround: " + isGrounded.ToString().ToLower()
        );
    }

    private void TargetVoxel() {
        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, rangeHit, groundMask)) {
            Vector3 pointPos = hit.point - hit.normal / 2;

            Chunk c = Chunk.GetChunk(new Vector3(
                Mathf.FloorToInt(pointPos.x),
                Mathf.FloorToInt(pointPos.y),
                Mathf.FloorToInt(pointPos.z)
            ));
            
            debugText += (
                "Target Voxel: " + 
                Mathf.FloorToInt(pointPos.x) + ", " +
                Mathf.FloorToInt(pointPos.y) + ", " +
                Mathf.FloorToInt(pointPos.z) + "\n" +
                c.GetBlock(pointPos).ToString()
            );
        }
    }

    private void Text1() {
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

        debugText1 = (
            "Allocated momory: 0% (0MB)" + "\n" +
            "Used memory: 0% (0MB) of 0MB"
        );

        text1.text = debugText1;
    }
}
