
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Diagnostics;
/**
 * Whalekit. Тестовое задание (Unity)

Реализуйте игру камень-ножницы-бумага, используя Unity (любой релизной версии не ниже 2018.3.0)

Результатом тестового задания должен быть полный Unity проект, в котором есть сцена Start, запуск которой в редакторе стартует игру.

Игра происходит по раундам, количество раундов не ограничено, конца игры не предусмотрено. 
Текущий счет по раундам (например 3 : 4) должен постоянно отображаться на экране.
Каждый раунд состоит из двух фаз
1)	на экране три кнопки, позволяющие игроку выбрать одну из трех фигур камень-ножницы-бумага. Нажатие на любую из трех кнопок фиксирует выбор фигуры игроком и переходит к фазе 2.
2)	на экране последовательно отображаются
a.	выбор фигуры оппонентом (AI)
b.	результат раунда (победа-поражение)
c.	изменение счета по раундам (например 3 : 4  -> 4 : 4)
d.	по действию пользователя (например клик на специальную кнопку либо в любом месте экрана) начинается новый раунд

Игра должна уметь поддерживать два режима
•	"честный", когда выбор фигуры оппонентом никак не связан с выбором игрока
•	"нечестный", в этом случае оппонент должен побеждать в соответствии с заданной вероятностью P (число [0..1]) - т.е. при P=1 оппонент всегда побеждает, при P=0.5 - примерно в половине раундов и т.д.

Конфигурацию - выбор режима и вероятность выигрыша AI для "нечестного" режима - можно реализовать любым удобным способом. Возможностью динамически поменять конфигурацию в процессе игры можно пренебречь. 




 * */


namespace CSharpTests
{
    public class RPSRules
    {
        public enum Gestures
        {
            R,
            P,
            S
        };

        private static Gestures[][] wincombinations = new Gestures[][] {
            new Gestures[2] { Gestures.P, Gestures.R },
            new Gestures[2] { Gestures.R, Gestures.S },
            new Gestures[2] { Gestures.S, Gestures.P }};

        public static Gestures GetWinGesture(Gestures g)
        {
            for (int i = 0; i < wincombinations.Length; i++)
            {
                if (wincombinations[i][0] == g)
                {
                    return wincombinations[i][1];
                }
            }

            throw new ArgumentException("unreachable code");
        }

        public static Gestures GetRandomGesture()
        {
            Array values = Enum.GetValues(typeof(Gestures));
            Random random = new Random();
            Gestures rand = (Gestures)values.GetValue(random.Next(values.Length));
            return rand;
        }

        public static bool IsDraw(Gestures g1, Gestures g2)
        {

            return g1 == g2;
        }

        public static bool FirstBeatsSecond(Gestures g1, Gestures g2)
        {
            for (int i = 0; i < wincombinations.Length; i++)
            {
                if (wincombinations[i][0] == g1)
                {
                    return wincombinations[i][1] == g2;
                }
            }
            return false;
        }

        [Conditional("TEST_RULES_CODE")]
        public static void testRules()
        {
            Debug.Assert(FirstBeatsSecond(Gestures.P, Gestures.R));
            Debug.Assert(!FirstBeatsSecond(Gestures.P, Gestures.S));
            Debug.Assert(!IsDraw(Gestures.P, Gestures.S));
            Debug.Assert(IsDraw(Gestures.S, Gestures.S));
            Console.WriteLine("RPS tests complete");
        }

    }

    public interface IInputReceiver
    {
        void OnInput(RPSRules.Gestures g);
    }

    public class InputRandom
    {
        private InputRoutertoMatchTurn receiver = null;
        public InputRandom(InputRoutertoMatchTurn _receiver)
        {
            receiver = _receiver;
            SelectResult();
        }

        //TODO:When to call it?
        public void SelectResult()
        {
            receiver.OnInput(RPSRules.GetRandomGesture());
        }
    }

    public class InputWinner : IInputReceiver
    {
        private InputRoutertoMatchTurn receiver = null;
        public InputWinner(InputRoutertoMatchTurn _receiver)
        {
            receiver = _receiver;
        }
        public void OnInput(RPSRules.Gestures g)
        {
            receiver.OnInput(RPSRules.GetWinGesture(g));
        }
    }
    public class InputRoutertoMatchTurn : IInputReceiver
    {
        private int playerIDX = -1;
        private RPSMatchTurn match = null;

        public InputRoutertoMatchTurn(RPSMatchTurn _match, int _playerIDX)
        {
            match = _match;
            playerIDX = _playerIDX;
        }
        public void OnInput(RPSRules.Gestures g)
        {
            match.OnInput(playerIDX, g);
        }
    }

    public class RPSMatchTurn
    {
        public RPSMatchTurn()
        {
        }
        private RPSRules.Gestures[] gesture = { RPSRules.Gestures.S, RPSRules.Gestures.S};
        private bool[] inputReceived = { false, false };

        public delegate void EndTurn(RPSRules.Gestures g1, RPSRules.Gestures g2);
        public EndTurn OnEndTurnSignal;

        public void OnInput(int playerN, RPSRules.Gestures g)
        {
            Debug.Assert(playerN < inputReceived.Length && playerN >= 0);
            Debug.Assert(inputReceived[playerN] == false);
            inputReceived[playerN] = true;
            gesture[playerN] = g;
            if(inputReceived[0] && inputReceived[1])
            {
                OnEndTurnSignal(gesture[0], gesture[1]);
            }
        }

    }

    public class RPSgame
    {
        public static RPSgame Construct()
        {
            RPSgame r = new RPSgame();

            RPSMatchTurn m = new RPSMatchTurn();





            return r;

        }

        public RPSgame()
        {

        }



        public void run()
        {
        }
    }
}
