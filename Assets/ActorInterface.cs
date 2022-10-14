class ActorInterface
{
    private static ActorInterface actorInterface;

    public bool pause
    {
        get; set;
    }

    public bool dead
    {
        get; set;
    }

    public static ActorInterface Create()
    {
        if(actorInterface == null)
        {
            actorInterface = new ActorInterface();
        }
        return actorInterface;
    }

    private ActorInterface() { }
}
