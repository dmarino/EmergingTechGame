using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [SerializeField]
    private int _width;

    [SerializeField]
    private int _height;

    [SerializeField]
    private float _scale;
    Renderer _renderer;

    [SerializeField]
    private bool _useThreshold;

    [SerializeField]
    [Range(0f, 1f)]
    private float _threshold;

    [SerializeField]
    private GameObject _prefab;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();

        _renderer.material.mainTexture = GenerateTexture();

    }

    private Texture GenerateTexture()
    {
        Texture2D texture = new Texture2D(_width, _height);

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Color color = CalculateColor(x, y);
                
                if(color == Color.white)
                {
                    int randValue = Random.Range(0, 10000);
                    
                    if(randValue < 3)
                    {
                        SpawnPoint(x, y );
                    }
                }

                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    void SpawnPoint(float x , float y)
    {
        Vector2 point = new Vector2( x * ( _width / transform.localScale.x) , y * (_height / transform.localScale.y));
        Vector2 newVec = Camera.main.ScreenToWorldPoint(point);

        GameObject obj = Instantiate(_prefab, newVec,Quaternion.identity);
    }

    private Color CalculateColor(int x, int y)
    {

        float xCoord = (float)x / _width * _scale;
        float yCoord = (float)y / _height * _scale;

        float pixel = Mathf.PerlinNoise(xCoord, yCoord);

        if (_useThreshold)
        {
            if (pixel <= _threshold)
                pixel = 0f;
            else
                pixel = 1f;
        }

        return new Color(pixel, pixel, pixel);
    }
}
