using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGen : MonoBehaviour{
    public Mesh mesh;
    private MeshFilter meshFilter;
    public int size;
    public float factor;
    public Noise noise;
    private float[] values;
    private float offset;
    private float intensity;
    private float scale;
    private TextHandler data_log;
    private Material mat;
    private MeshCollider collid;
    public Biome biome;
    
    public void ConstructLandscape()
    {
        Vector3[] vertices = new Vector3[(size * size)];
        int[] triangles = new int[((size - 1) * (size - 1) * 6)];
        float max_y = -1f;
        float min_y = 10000f;
        Vector2[] uv = new Vector2[(size * size)];
        int count = 0;
        int triangle_index = 0;
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                float n = 0.0f;
                for (int i = 0; i < biome.size; i++)
                {

                    if (i == 0)
                    {
                        //values[i] = (noise[i].Evaluate(new Vector3(x+offset, 0, y+offset)*0.03f) +1.0f) * 0.5f;
                        float xx = ((float)x / size) * biome.frequency[i];
                        float yy = ((float)y / size) * biome.frequency[i];
                        values[i] = Mathf.PerlinNoise(xx + offset, yy + offset);
                        if (values[i] < biome.threshold[i])
                        {
                            values[i] = 0.0f;
                        }
                        n += values[i]* values[i] * biome.intensity[i];
                    }
                    else
                    {
                        if (values[i - 1] > biome.threshold[i])
                        {
                            values[i] = ((noise.Evaluate(new Vector3(x + offset, 0, y + offset) * biome.frequency[i])) + 1.0f) * 0.5f;
                        }
                        else
                            values[i] = 0.0f;
                        n += values[i] * biome.intensity[i];
                    }
                }
                if (n > max_y)
                {
                    max_y = n;
                }
                if (n < min_y)
                {
                     min_y = n;
                }
                uv[count] = new Vector2((float)x / (size - 1), (float)y / (size - 1));
                vertices[count] = new Vector3(x * factor, n, y * factor);
                
                if (x != size - 1 && y != size - 1)
                {
                    triangles[triangle_index] = count;
                    triangles[triangle_index + 1] = count + size + 1;
                    triangles[triangle_index + 2] = count + size;

                    triangles[triangle_index + 3] = count;
                    triangles[triangle_index + 4] = count + 1;
                    triangles[triangle_index + 5] = count + size + 1;
                    triangle_index += 6;
                }
                count += 1;
            }
            
        }
        
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();
        data_log.WriteString(biome.name);
        data_log.WriteString("max y = " + max_y.ToString());
        data_log.WriteString("min y = " + min_y.ToString());
    }
    public void Start()
    {
        data_log = new TextHandler("Assets/Resources/Save/log.txt");
        meshFilter = this.gameObject.GetComponent<MeshFilter>();
        mesh = new Mesh();
        meshFilter.mesh = mesh;
        noise = new Noise();
        values = new float[2];
        intensity = 50f;
        offset = Random.value * 1000f;
        int type = Random.Range(1, 3);
        biome = new Biome(1);
        
        print(biome.name);
        size = 256;
        factor = 1f;
        scale = 1f;
        ConstructLandscape();
        mat = gameObject.GetComponent<Renderer>().material;
        mat.SetColor("_ColorB", biome.Color1);
        mat.SetColor("_ColorM", biome.Color2);
        mat.SetColor("_ColorT", biome.Color3);
        mat.SetFloat("_Step1", biome.step[0]);
        mat.SetFloat("_Step2", biome.step[1]);
        mat.SetTexture("_Text1", biome.Text1);
        mat.SetTexture("_Text2", biome.Text2);
        mat.SetTexture("_Text3", biome.Text3);
        gameObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        collid = gameObject.GetComponent<MeshCollider>();
        collid.sharedMesh = meshFilter.mesh;

    }
}
