using UnityEditor;

namespace Framework.Editor
{
    /// <summary>
    /// 资源规则的抽象基类
    /// </summary>
    public abstract class AssetRuleBase
    {
        /// <summary>
        /// 获取资源规则的名称、
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 仅在资源导入后调用此方法。
        /// </summary>
        /// <param name="assetImporter">资源导入器</param>
        public void Postprocess(AssetImporterWrapper assetImporter)
        {
            OnPostprocess(assetImporter);
        }

        /// <summary>
        /// 获取一个值，表示某个资源是否需要预处理，用于预校验，省去加载<see cref="AssetImporter"/>的开销。
        /// </summary>
        /// <param name="assetPath"></param>
        public bool Precheck(string assetPath)
        {
            return OnPrecheck(assetPath);
        }

        /// <summary>
        /// 仅在资源导入前调用此方法。
        /// </summary>
        public void Preprocess(AssetImporter assetImporter)
        {
            OnPreprocess(assetImporter);
        }

        /// <summary>
        /// 处理指定资源的导入。
        /// </summary>
        /// <param name="assetImporter">资源导入器</param>
        /// <returns>是否已处理</returns>
        public void Process(AssetImporterWrapper assetImporter)
        {
            OnPreprocess(assetImporter.Target);
            OnPostprocess(assetImporter);
        }

        /// <summary>
        /// 当资源导入后调用此方法。
        /// </summary>
        /// <param name="assetImporter">资源导入器</param>
        protected virtual void OnPostprocess(AssetImporterWrapper assetImporter) { }

        /// <summary>
        /// 当需要检测某个资源是否需要预处理时调用此方法，用于预校验，省去加载<see cref="AssetImporter"/>的开销。
        /// </summary>
        /// <param name="assetPath">资源路径</param>
        /// <returns>是否需要进行进一步处理</returns>
        protected abstract bool OnPrecheck(string assetPath);

        /// <summary>
        /// 仅在资源导入前调用此方法。
        /// </summary>
        /// <param name="assetImporter"></param>
        protected virtual void OnPreprocess(AssetImporter assetImporter) { }
    }
}
