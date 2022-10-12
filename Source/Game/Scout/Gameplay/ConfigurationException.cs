using System;

namespace Game.Scout.Gameplay
{
    public class ConfigurationException : ArgumentException
    {
        public ConfigurationException(String message, Exception cause) : base(message, cause)
        {

        }

        public ConfigurationException(String message) : this(message, null)
        {
            
        }
    }
}
