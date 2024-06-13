using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    [SerializeField] private int m_xWidth, m_zWidth;
    private float m_tileOffset = 0.15f;
    [SerializeField] private GameObject m_tilePrefab;

    void Start()
    {
        for (int i = 0; i < m_xWidth; i++)
        {
            for (int j = 0; j < m_zWidth; j++)
            {
                GameObject tile = Instantiate(m_tilePrefab, new Vector3(i+(m_tileOffset * i), 0, j + (m_tileOffset* j)), m_tilePrefab.transform.rotation, this.transform);
                tile.name = $"tile{i}{j}";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
