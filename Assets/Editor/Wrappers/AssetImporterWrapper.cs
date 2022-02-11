using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace Framework.Editor
{
    /// <summary>
    /// 用于包装<see cref="AssetImporter">，合并批次修改其对应的属性。
    /// </summary>
    public class AssetImporterWrapper
    {
        private readonly AssetImporter mImporter;

        /// <summary>
        /// 获取已被包装过的<see cref="AssetImporter"/>原对象。
        /// </summary>
        public AssetImporter Target => mImporter;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
