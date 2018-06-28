using System;

namespace Stack_m_up
{
    class Audio
    {
        String name;
        public enum typeEnum { sfx, music };
        typeEnum type;
        bool loop;
        string path;

        public Audio(string name, typeEnum type, bool loop, String path)
        {
            this.name = name;
            this.type = type;
            this.loop = loop;
            this.path = path;
        }

        public String getName()
        {
            return this.name;
        }

        public typeEnum getType()
        {
            return this.type;
        }

        public string getPath()
        {
            return this.path;
        }

        public bool isLoop()
        {
            return this.loop;
        }
    }
}
