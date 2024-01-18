namespace Server.Exceptions
{
    public class PathException : Exception
    {
        public bool state { get; }

        public PathException(string message, bool State) 
            : base(message)
        {
            state = State;
        }
    }
}
