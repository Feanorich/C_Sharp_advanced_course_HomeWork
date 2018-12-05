using System;

namespace MyGame
{
    class GameObjectException : Exception
    {
        public GameObjectException(string message) : base(message)
        {
        }
    }
}