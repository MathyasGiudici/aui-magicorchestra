using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    [Serializable]
    public class ThemeSchema
    {
        public string id;
        public string title;
        public string background_floor;
        public string background_front;
        public string background_sound;
        public string fragrance;
        public string fixed_light;
        public ThemeObjects[] objs;
    }

    [Serializable]
    public class ThemeObjects {
        public string objname;
        public ImageBlock images;
        public string audio;
        public string video;
        public string tags;
        public string tagRFID;
    }

    [Serializable]
    public class ImageBlock {
        public string defaults;
    }

