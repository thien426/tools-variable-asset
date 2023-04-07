using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace T70.VariableAsset
{
    [CreateAssetMenu(fileName = "AudioClip Asset", menuName = VariableConst.MenuPath.AudioClip, order = VariableConst.Order.AudioClip)]
    public class AudioClipAsset : AssetT<AudioClip>
    {
        protected override void Init()
        {
            _inited = true;
            Value = null;
        }
    }

}