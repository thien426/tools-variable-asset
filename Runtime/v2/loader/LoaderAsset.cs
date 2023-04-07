using System;
using UnityEngine;


namespace T70.VariableAsset
{
    public enum LoaderState // ---> LoaderStateAsset
    {
        Idle, Start, Progress, Error, Timeout, Cancel, Complete
    }

    public class LoaderStateAsset : AssetT<LoaderState> { }
    
    public class LoaderInfo : Asset
    {
        public FloatAsset progress;
        public LoaderStateAsset status;
        public StringAsset error;
    }
    
    
    
    
    
    
    
    
    
    
    
//    public string id;
//    public Type type;
//    public string webURL;
//    public string localURL;
//    public string resourceURL;
    
    // ------> Load Files
    
    // CheckLocal
    // CheckResources
    // CheckInternet
    
    // LoadWeb
    // LoadLocal
    // LoadResources
    
    // WriteLocal
    
    // LoaderStart
    // LoaderProgress
    // LoaderError
    // LoaderTimeout
    // LoaderComplete

    
    
    
    
    
    
    public class LoaderAsset : Asset 
    {
                   
    }
}

