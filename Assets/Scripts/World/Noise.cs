using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise {
    static float frequecy = 0.01f;
    static float amplitude = 50.0f;
    static float heightOffset = 63.0f;
    static int seed;
    
    public static int Perlin(float x, float z) {        
        x *= frequecy;
        z *= frequecy;
        
        x += seed;
        z += seed;
        
        float y;
        y = Mathf.PerlinNoise(x, z);
        y *= amplitude;

        y += heightOffset;

        return (int)y;
    }

    public static float Perlin3D(float x, float y, float z) {
        float xy = Mathf.PerlinNoise(x, y);
        float xz = Mathf.PerlinNoise(x, z);

        float yx = Mathf.PerlinNoise(y, x);
        float yz = Mathf.PerlinNoise(y, z);

        float zx = Mathf.PerlinNoise(z, x);
        float zy = Mathf.PerlinNoise(z, y);

        float xyz = xy + xz + yx + yz + zx + zy;
        
        return xyz / 6.0f;
    }
}
