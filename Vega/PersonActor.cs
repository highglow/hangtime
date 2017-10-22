using System;

namespace Vega
{
    public interface IPersonActor
    {
        string SayHello(Person person);
    }

    class PersonActor : IPersonActor
    {
        public string SayHello(Person person)
        {
            var msg = $"Hello {person.Name}";
            Console.WriteLine(msg);
            return msg;
        }
    }
}