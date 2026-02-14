using System;
using System.Collections.Generic;

namespace ItemUpgradeSystem
{
    public class Item
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Level { get; set; }

        public Item(string name, int price, int level)
        {
            Name = name;
            Price = price;
            Level = level;
        }

        public int CalculatePower()
        {
            return Price * Level;
        }

        public static Item operator +(Item item1, Item item2)
        {
            string newName = $"Редкий {item1.Name} +1";
            int newPrice = item1.Price + item2.Price;
            int newLevel = item1.Level + 1;

            return new Item(newName, newPrice, newLevel);
        }

        public string GetInfo()
        {
            return $"{Name} (Цена: {Price}, Ур. {Level})";
        }
    }

    public class Hero
    {
        public string Name { get; set; }
        public int Power { get; set; }
        public int RequiredPower { get; set; }

        public Hero(string name, int power, int requiredPower)
        {
            Name = name;
            Power = power;
            RequiredPower = requiredPower;
        }

        public bool HasEnoughPower()
        {
            return Power >= RequiredPower;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Улучшение предметов\n");

            Hero hero = new Hero("Герой", 70, 40);

            Console.WriteLine($"Герой: {hero.Name}");
            Console.WriteLine($"Сила героя: {hero.Power}");
            Console.WriteLine($"Требуемая Сила: {hero.RequiredPower}\n");

            List<Item> items = new List<Item>
            {
                new Item("Меч", 25, 1),
                new Item("Топор", 30, 1),
                new Item("Щит", 20, 1)
            };

            Console.WriteLine("Ваши предметы:");
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i].GetInfo()}");
            }

            Console.WriteLine();

            if (hero.HasEnoughPower())
            {

                try
                {
                    Console.Write("Введите название улучшаемого предмета (Меч, Топор или Щит): ");
                    string firstItemName = Console.ReadLine();

                    Console.Write("Введите название используемого предмета (Меч, Топор или Щит): ");
                    string secondItemName = Console.ReadLine();

                    Item firstItem = items.Find(item => item.Name.Equals(firstItemName, StringComparison.OrdinalIgnoreCase));
                    Item secondItem = items.Find(item => item.Name.Equals(secondItemName, StringComparison.OrdinalIgnoreCase));

                    if (firstItem == null || secondItem == null)
                    {
                        Console.WriteLine("\nОшибка: один или оба предмета не найдены!");
                    }
                    else if (firstItem.Name == secondItem.Name)
                    {
                        Console.WriteLine("\nОшибка: нельзя улучшать предмет сам с собой!");
                    }
                    else
                    {
                        Console.WriteLine($"\nУлучшение: {firstItem.Name} с помощью {secondItem.Name}");

                        Item upgradedItem = firstItem + secondItem;

                        Console.WriteLine($"Создан:  {upgradedItem.GetInfo()}");

                        items.Remove(firstItem);
                        items.Remove(secondItem);
                        items.Add(upgradedItem);

                        hero.Power -= hero.RequiredPower;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nПроизошла ошибка: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("✗ Недостаточно Силы для улучшения предметов!");
                Console.WriteLine($"Требуется еще {hero.RequiredPower - hero.Power} Силы.");
            }

        }
    }
}