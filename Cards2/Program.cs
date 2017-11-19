using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// Логика выборки компьютера примитивна (инфо : строка 210,211)
/// Много лишнего кода........
///  


namespace Cards
{
    class Program
    {
        //-------------------------------------------------------------------------------------------------------------//
        private static int numshufle = 1, gamedatenum, compwin, playwin, compsc, playsc, ret, parety;
        private static byte cvs = 1;
        private static Cards[] cardall = new Cards[36];
        private static Cards[] tempcard = new Cards[1];
        private static Cards[] playhand = new Cards[14];
        private static Cards[] comphand = new Cards[14];
        private static int deskout = 35, plnd = 0, cond = 0;
        private static bool compmind = true, playmind = true, tj = false, cj=false;
        private static double call, pall, dall;
        private static string companswer = "";

        //-------------------------------------------------------------------------------------------------------------//
        struct Cards
        {
            public SuitName Suit;
            public CardName23 Value;
            public int Score;
        }
        public enum SuitName { Clubs, Spases, Hearts, Diamonds }
        public enum CardName23 { Six = 6, Seven, Eight, Nine, Ten, Ace, Jack = 2, Queen, King };

        //-------------------------------------------------------------------------------------------------------------//
        public static void Generator()
        {
            int s = 0;
            Cards tempcard = new Cards();
            foreach (var suitcards in Enum.GetValues(typeof(SuitName)))
            {
                foreach (var intcards in Enum.GetValues(typeof(CardName23)))
                {
                    cardall[s].Suit = (SuitName)suitcards;
                    cardall[s].Value = (CardName23)intcards;
                    cardall[s].Score = (int)intcards;
                    s += 1;
                }
            }

            for (int q = 0; q < numshufle; q++)
            {
                //Console.WriteLine("surflw {0}",q+1);
                for (int i = 0; i < 36; i++)
                {
                    Random rndcards = new Random(DateTime.Now.Millisecond + (i * 333));
                    int numtemp = rndcards.Next(0, 36);
                    tempcard = cardall[i];
                    cardall[i] = cardall[numtemp];
                    cardall[numtemp] = tempcard;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------//
        static void ViewAllResult()
        {
            if (gamedatenum != 0)
            {
                call = (compwin * 100) / gamedatenum;
                pall = (playwin * 100) / gamedatenum;
                dall = (parety * 100) / gamedatenum;
            }
            Console.Clear();
            Console.WriteLine("All games played {0}:", gamedatenum);
            Console.WriteLine("Computer wins in {0} which means in {1} %", compwin, call);
            Console.WriteLine("Player wins in {0} which means in {1} %", playwin, pall);
            Console.WriteLine("All draw games {0} which means in {1} %", parety, dall);
            Console.Write("press any key");
            Console.ReadKey();
            SelectInOut();
        }
        //-------------------------------------------------------------------------------------------------------------//
        static void GamePlayer()
        {
            if (cvs == 1)
            {
                cvs = 0;
                for (int i = 0; i < playhand.Length; i++)
                {
                    for (int f = i + 1; f < playhand.Length; f++)
                    {
                        if ((playhand[i].Value == CardName23.Jack) && (playhand[f].Value == CardName23.Jack))
                        {
                            playwin += 1;
                            tj = true;
                            TwoJack();
                        }
                    }
                }
               
                if (compmind == false && playmind == false) { DeskCalc(); }
                
                if (playsc == 21)
                {
                    playmind = false;
                    GameComputer();
                }

                if (playsc > 21)
                {
                    playmind = false;
                    GameComputer();
                    //DeskCalc();
                }

                Console.Clear();
                Console.WriteLine(companswer);
                Console.WriteLine("you cards:");

                for (int i = 0; i < plnd; i++)
                {
                    Console.WriteLine(":{0}:{1}:{2}", playhand[i].Suit, playhand[i].Value, playhand[i].Score);

                }
                
                Console.WriteLine("you score {0}:", playsc);
                Console.WriteLine("1 - взять:");
                Console.WriteLine("2 - пропуск:");
                Console.Write("Pres:");
                int cout = int.Parse(Console.ReadLine());

                if (cout == 1)
                {
                    cvs = 0;
                    playhand[plnd] = cardall[deskout];
                    playsc += playhand[plnd].Score;
                    deskout -= 1;
                    plnd += 1;
                    Console.WriteLine();
                    GameComputer();
                }

                if (cout == 2)
                {
                    playmind = false;
                    cvs = 0;
                    GameComputer();
                }
            }

            else
            {
                //  GamePlayer();
                GameComputer();
            }
        }
        //-------------------------------------------------------------------------------------------------------------//
        static void Hand()
        {
            ret += 2;

            for (int j = 0; j < ret; j++)
            {
                for (int i = 0; i < 1; i++)
                {
                    playhand[plnd] = cardall[deskout];
                    playsc += playhand[plnd].Score;
                    deskout -= 1;
                    plnd += 1;
                }

                for (int i = 0; i < 1; i++)
                {
                    comphand[cond] = cardall[deskout];
                    compsc += comphand[cond].Score;
                    deskout -= 1;
                    cond += 1;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------//
        static void GameComputer()
        {
            // if (compsc == 21) { compmind = false; GamePlayer(); }

            if (compmind == false && playmind == false)
            {
                DeskCalc();
            }

            if (cvs == 0)
            {
                cvs = 1;
                for (int i = 0; i < comphand.Length; i++)
                {
                    for (int f = i + 1; f < comphand.Length; f++)
                    {
                        if ((comphand[i].Value == CardName23.Jack) && (comphand[f].Value == CardName23.Jack))
                        {
                            compwin += 1;
                            cj = true;
                            TwoJack();
                        }
                    }
                } 
                
                if (compsc == 21) { DeskCalc(); }
                if (compsc > 21) { DeskCalc(); }
                if (compsc < 21 - 8) { compmind = true; companswer = "Computer ++ card"; }
                if (compsc > 21 - 8) { compmind = false; companswer = "Computer no ++ cards"; }
                if (compmind == true)
                {
                    comphand[cond] = cardall[deskout];
                    compsc += comphand[cond].Score;
                    deskout -= 1;
                    cond += 1;
                    GamePlayer();
                }
                if (compmind == false) { GamePlayer(); }

            }
            else
            {
                GamePlayer();
                // GameComputer();
            }
        }
        //-------------------------------------------------------------------------------------------------------------//
        static void SetPlayer()
        {
            Console.WriteLine("кто начинает\n1-Computer\n2-Player");
            int x = int.Parse(Console.ReadLine());
            switch (x)
            {
                case 1: Console.Clear(); cvs = 0; Hand(); GameComputer(); break;
                case 2: Console.Clear(); cvs = 1; Hand(); GamePlayer(); break;
                default: Console.Clear(); break;
            }
        }
        //-------------------------------------------------------------------------------------------------------------//
        static void Main(string[] args)
        {
            SelectInOut();
            Exit();
        }
        //-------------------------------------------------------------------------------------------------------------//
        static void SelectInOut()
        {
            Init();
            Console.Clear();
            Console.WriteLine("Pres:");
            Console.WriteLine("1 - New Game:");
            Console.WriteLine("2 - View Result:");
            Console.WriteLine("3 - Setting:");
            Console.WriteLine("4 - Exit Game:");
            int inout = int.Parse(Console.ReadLine());

            switch (inout)
            {
                case 1: Console.Clear();
                    gamedatenum += 1;
                    Generator();
                    SetPlayer();
                    break;
                case 2: Console.Clear();
                    ViewAllResult();
                    break;
                case 3: Console.Clear();
                    Setting();
                    break;
                case 4: Console.Clear();
                    Exit();
                    break;
                default: Console.Clear();
                    break;
            }
        }
        //-------------------------------------------------------------------------------------------------------------//
        static void Exit()
        {
            Environment.Exit(0);
        }
        //-------------------------------------------------------------------------------------------------------------//
        static void Setting()
        {
            Console.Clear();
            Console.WriteLine("settings now:");
            Console.WriteLine("mixing - {0}:", numshufle);
            Console.WriteLine("\n\n\n");
            Console.WriteLine("1 - Number of cards deck mixing:");
            Console.WriteLine("2 - Out setting:");
            Console.Write("Pres: ");
            int inout = int.Parse(Console.ReadLine());
            switch (inout)
            {
                case 1:
                    Console.Write("new mixing : ");
                    numshufle = int.Parse(Console.ReadLine());
                    Console.Clear();
                    Setting();
                    break;
                case 2:
                    Console.Clear();
                    SelectInOut();
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }
        //-------------------------------------------------------------------------------------------------------------//
        static void DeskCalc()
        {
            Console.Clear();
            Console.WriteLine("you carte: at score {0}", playsc);
              for (int i = 0; i < plnd; i++)
            {
                Console.WriteLine(":{0}:{1}:{2}", playhand[i].Suit, playhand[i].Value, playhand[i].Score);
            }
            Console.WriteLine("comp carte: at score {0}", compsc);
              for (int i = 0; i < cond; i++)
            {
                Console.WriteLine(":{0}:{1}:{2}", comphand[i].Suit, comphand[i].Value, comphand[i].Score);
            }
              if (compsc > 21 && playsc > 21)
            {
                Console.WriteLine("Comp-/Play-\n press key");
                Console.ReadLine();
               
                // SelectInOut();
            }
              if (compsc > 21 && playsc <21)
            {
                playwin += 1;
                Console.WriteLine("Comp-\n press key");
                Console.ReadLine();
                Console.Clear();
            }
              if (playsc > 21 && compsc <21)
            {
                compwin += 1;
                Console.WriteLine("Play-\n press key");
                Console.ReadLine();
                Console.Clear();
            }
            if (compsc < 21 && playsc < 21)
            {
                if (compsc == playsc)
                {
                    parety += 1;
                    Console.WriteLine("Draw\n press key");
                    Console.ReadLine();
                    //  SelectInOut();
                }
                if (compsc < playsc)
                {
                    playwin += 1;
                    Console.WriteLine("Play+\n press key");
                    Console.ReadLine();
                    //    SelectInOut();
                }
                if (compsc > playsc)
                {
                    compwin += 1;
                    Console.WriteLine("Comp+\n press key");
                    Console.ReadLine();
                    //SelectInOut();
                }
            }
            if (compsc == 21) { compwin += 1; Console.WriteLine("Comp+\n press key"); Console.ReadLine(); Console.Clear(); }
            if (playsc == 21) { playwin += 1; Console.WriteLine("Play+\n press key"); Console.ReadLine(); Console.Clear(); }
           
            SelectInOut();
        }
        //-------------------------------------------------------------------------------------------------------------//
        static void TwoJack()
        {
            if (tj == true)
            {
                Console.WriteLine(" youre win 2 Jack" );
                Console.ReadKey();
                ViewAllResult();
            }
            if (cj == true)
            {
                Console.WriteLine(" Comp win 2 Jack");
                Console.ReadKey();
                ViewAllResult();
            }
          
        }
        //-------------------------------------------------------------------------------------------------------------//
        static void Init()
        {
            compmind = true;
            playmind = true;
            tj = false;
            cj = false;
            compsc = 0;
            playsc = 0;
            deskout = 35;
            plnd = 0;
            cond = 0;
            ret = 0;
        }
    }
}

