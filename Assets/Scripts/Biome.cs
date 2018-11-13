using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome {
    public int size;
    public Vector4 Color1;
    public Vector4 Color2;
    public Vector4 Color3;
    public Texture Text1;
    public Texture Text2;
    public Texture Text3;
    public float[] step;
    public float[] intensity;
    public float[] frequency;
    public float[] threshold;
    public string name;

    public Biome(int type)
    {
        switch (type)
        {
            case 1:
                size = 2;
                Color1 = new Color(0.2f, 0.2f, 0.2f, 1.0f);
                Color2 = new Color(0.4f, 0.4f, 0.7f, 1.0f);
                Color3 = new Color(1f, 1f, 1f, 1.0f);
                intensity = new float[] { 100f, 4f };
                step = new float[] { 2f, 8f };
                frequency = new float[] { 5f, 0.15f };
                threshold = new float[] { 0.2f,0.6f };
                Text1 = Resources.Load<Texture>("Textures/Terrain/watersmall");
                Text2 = Resources.Load<Texture>("Textures/Terrain/snowrocksmall");
                Text3 = Resources.Load<Texture>("Textures/Terrain/snowsmall");
                name = "mountain";
            break;

            case 2:
                size = 2;
                Color1 = new Color(0.0f, 0.0f, 0.8f, 1.0f);
                Color2 = new Color(139f/255, 69f/255, 19f/255, 1.0f);
                Color3 = new Color(0f, 1f, 0f, 1.0f);
                intensity = new float[] { 15f, 5f };
                step = new float[] { 2f, 11f };
                frequency = new float[] { 15f, 0.1f };
                threshold = new float[] { 0.15f, 0.5f };
                
                Text1 = Resources.Load<Texture>("Textures/Terrain/watersmall");
                Text2 = Resources.Load<Texture>("Textures/Terrain/grasssmall");
                Text3 = Resources.Load<Texture>("Textures/Terrain/rocksmall");
                name = "hills";
                break;

            case 3:
                size = 2;
                Color1 = new Color(0.0f, 0.0f, 0.8f, 1.0f);
                Color2 = new Color(1f, 1f, 0f, 1.0f);
                Color3 = new Color(1f, 1f, 0f, 1.0f);
                intensity = new float[] { 1f, 1f };
                step = new float[] { 0.01f, 500f };
                frequency = new float[] { 50f, 0.05f };
                threshold = new float[] { 0.01f, 0.65f };
                Text1 = Resources.Load<Texture>("Textures/Terrain/watersmall");
                Text2 = Resources.Load<Texture>("Textures/Terrain/sandsmall");
                Text3 = Resources.Load<Texture>("Textures/Terrain/sandsmall");
                name = "desert";
                break;
        }
    }
}
