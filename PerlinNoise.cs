using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise{

        public static float GetNoise(int x,
                                     int z,
                                     int chunkLength,
                                     int chunkWidth,
                                     int mapSize,
                                     int octaves,
                                     float seed,
                                     float redistribution,
                                     float frequency){

            float sampleX = (float)(x)/(float)(chunkLength*mapSize) - 0.01f;
            float sampleZ = (float)(z)/(float)(chunkWidth*mapSize) - 0.01f;
            float val = 0.0f;
            float A = 0.0f;
            for(int i = 0; i != octaves; i++){
                float div = Mathf.Pow(2.0f, i);
                A += (1/div);
                val += (1/div)*Mathf.PerlinNoise((frequency*div)*sampleX + seed,
                                                 (frequency*div)*sampleZ + seed);
            }//
            float B = Mathf.Pow(val/A, redistribution);
            return B;
        }//end GetNoise



}//end PerlinNoise
