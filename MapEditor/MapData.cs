using Newtonsoft.Json;

using System.Collections.Generic;
using System.Drawing;

namespace MapEditor
{

    //Make a class that contains not only a List<Terrain> but also a LIst<Decor> and serialize that class
    class MapData
    {
        
        [JsonProperty]
        List<Terrain> tTiles = new List<Terrain>();

        [JsonProperty]
        List<Decor> dTiles = new List<Decor>();

        [JsonProperty]
        public Point BoatPosition;

        public MapData(List<Terrain> terrains, List<Decor> decor, Point boatPosition)
        {
            tTiles = terrains;
            dTiles = decor;
            BoatPosition = boatPosition;
        }
    }
}

