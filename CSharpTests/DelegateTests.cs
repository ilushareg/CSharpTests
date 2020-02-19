using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpTests
{
    public class DelegateTests
    {
        public DelegateTests()
        {

        }

        //int arg for Event Handler
        public class InputArgs : EventArgs
        {
            public InputArgs(int value)
            {
                Value = value;
            }
            public int Value { get; set; }
        }

        interface IInput
        {
            //register listener to send the input events to
            void RegisterListener(EventHandler<InputArgs> act);
        }

        //simple input 
        public class InputSimple:IInput
        {
            private event EventHandler<InputArgs> OnInput = delegate { };
            public InputSimple()
            {

            }
            public void RegisterListener(EventHandler<InputArgs> act)
            {
                OnInput += act;
            }
            public void MakeInput(int v)
            {
                OnInput(this, new InputArgs(v));
            }
        }

        //cheater player, listens for input from other player and fires its own
        public class InputCheater:IInput
        {
            private event EventHandler<InputArgs> OnInput = delegate { };
            public InputCheater()
            {

            }
            public void RegisterListener(EventHandler<InputArgs> act)
            {
                OnInput += act;
            }
            public void ReactToInput(int val)
            {
                OnInput(this, new InputArgs(val + 1));
            }
        }

        public void DoTest()
        {

            //someone to receive inputs from both players
            EventHandler<InputArgs> inputReceiver = (sender, e)
                =>
            {
                Console.WriteLine("Input in: {0}", e.Value);
            };

            //simple player
            InputSimple simple = new InputSimple();
            IInput iS = simple;

            //player cheater
            InputCheater cheater = new InputCheater();
            IInput iC = cheater;

            iS.RegisterListener(inputReceiver);
            iS.RegisterListener((sender, e) => { cheater.ReactToInput(e.Value); });
            iC.RegisterListener(inputReceiver);

            simple.MakeInput(11);
            simple.MakeInput(22);

        }
    }
}
