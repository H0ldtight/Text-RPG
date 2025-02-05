using System.Numerics;
using System.Reflection.Emit;

namespace Text_RPG
{
    public class Player   // 플레이어 정보
    {
        public int Level { get; set; } = 1;
        public string Name { get; set; }
        public string Job { get; set; } = "초보자";
        public int Att { get; set; } = 10;
        public int Def { get; set; } = 5;
        public int Hp { get; set; } = 100;
        public int Gold { get; set; } = 1500;
        public List<Item> Inventory { get; set; } = new List<Item>();

        public void EquipItem(Item item)
        {
            if (!item.Equipped)
            {
                item.Equipped = true;
                Att += item.Att;
                Def += item.Def;
            }
        }

        public void UnEquipItem(Item item)
        {
            if (item.Equipped)
            {
                item.Equipped = false;
                Att -= item.Att;
                Def -= item.Def;
            }
        }

    }

    public class Item   // 아이템 클래스
    {
        // 생성자 추가 (아이템 생성 시 각 속성 초기화)
        public Item(string name, string info, int att, int def, int gold, bool soldout = false,  bool equipped = false)
        {
            Name = name;
            Info = info;
            Att = att;
            Def = def;
            Gold = gold;
            Soldout = soldout;
            Equipped = equipped;
        }

        public string Name { get; set; }
        public string Info { get; set; }
        public int Att { get; set; }
        public int Def { get; set; }
        public int Gold { get; set; }
        public bool Soldout { get; set; }
        public bool Equipped { get; set; }
    }

    public class Shop
    {
        public List<Item> Items { get; set; }

        public Shop()
        {
            Items = new List<Item>();

            new Item("수련자 갑옷", "| 방어력 +5 | 수련에 도움을 주는 갑옷입니다. |", 0, 5, 1000);
            new Item("무쇠 갑옷", "| 방어력 +9 | 무쇠로 만들어져 튼튼한 갑옷입니다. |", 0, 9, 2000);
            new Item("스파르타의 갑옷", "| 방어력 +15 | 수련에 도움을 주는 갑옷입니다. |", 0, 15, 3500);
            new Item("낡은 검", "| 공격력 +2 | 쉽게 볼 수 있는 낡은 검 입니다. |", 0, 2, 600);
            new Item("청동 도끼", "| 공격력 +5 | 어디선가 사용됐던거 같은 도끼입니다. |", 0, 5, 1500);
            new Item("스파르타의 창", "| 공격력 +7 | 스파르타의 전사들이 사용했다는 전설의 창입니다. |", 0, 5, 3000);
           
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();

            Console.Write("플레이어의 이름을 입력 해주세요. : ");
            player.Name = Console.ReadLine();

            Spartavillage spartavillage = new Spartavillage(player);
            spartavillage.MainSelect();
        }
    }

    public class Spartavillage
    {
        private Player player;
        public Spartavillage(Player player)
        {
            this.player = player;
        }

        public void MainSelect()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"스파르타 마을에 오신 {player.Name}님 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                if (int.TryParse(Console.ReadLine(), out int mainselect))
                {
                    switch (mainselect)
                    {
                        case 1:
                            PlayerInfo();
                            break;
                        case 2:
                            Inventory();
                            break;
                        case 3:
                            Shop();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.Write(">>");
                }
            }
        }
        public void PlayerInfo()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine();
                Console.WriteLine($"LV. {player.Level}");
                Console.WriteLine($"{player.Name} ( {player.Job} )");
                Console.WriteLine($"공격력: {player.Att}");
                Console.WriteLine($"방어력: {player.Def}");
                Console.WriteLine($"체력: {player.Hp}");
                Console.WriteLine($"금액: {player.Gold}G");
                Console.WriteLine();
                Console.WriteLine("1. 이름 변경하기");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");


                if (int.TryParse(Console.ReadLine(), out int PlayerInfoselect))
                {
                    switch (PlayerInfoselect)
                    {
                        case 1:
                            PlayerNameChange();
                            break;
                        case 0:
                            MainSelect();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.Write(">>");
                }
            }

        }
        public void PlayerNameChange()
        {
            Console.WriteLine();
            Console.Write("변경할 이름을 입력해주세요.: ");
            player.Name = Console.ReadLine();
            PlayerInfo();
        }

        public void Inventory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();
                
                    Console.WriteLine("1. 장착관리");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                if (int.TryParse(Console.ReadLine(), out int Inventoryselect))
                {
                    switch (Inventoryselect)
                    {
                        case 1:
                            InventoryEquippedmanagemnt();
                            break;
                        case 0:
                            PlayerInfo();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.Write(">>");
                }
            }
        }

        public void InventoryEquippedmanagemnt()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                if (int.TryParse(Console.ReadLine(), out int Equippedmanagemntselect))
                {
                    switch (Equippedmanagemntselect)
                    {
                        case 0:
                            Inventory();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.Write(">>");
                }
            }
        }

        public void Shop()
        {
            Console.WriteLine("미완성입니다");
        }
    }
}


