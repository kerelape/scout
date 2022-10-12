using System;

namespace Game.Scout.Gameplay
{
    public class PropertyNotSetException : ConfigurationException
    {
        public PropertyNotSetException(String name) : base($"Property {name} is unset.", new NullReferenceException())
        {
        }
    }
}
