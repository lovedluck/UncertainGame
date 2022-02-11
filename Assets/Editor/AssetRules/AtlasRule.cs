using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Editor
{

    public class AtlasRule : AssetRuleBase
    {
        public override string Name => "图集";

        protected override bool OnPrecheck(string assetPath)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnPostprocess(AssetImporterWrapper assetImporter)
        {
            base.OnPostprocess(assetImporter);
        }
    }
}
