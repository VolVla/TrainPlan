using System;
using System.Collections.Generic;

namespace TrainPlan
{
    internal class Program
    {
        static void Main()
        {
            RailStation railStation = new RailStation();
            railStation.Work();
        }
    }

    class RailStation
    {
        private List<Train> _trainsList = new List<Train>();
        private TicketOffice _ticketOffice = new TicketOffice();
        private bool _isWork = true;

        public void Work()
        {
            while (_isWork)
            {
                if (_trainsList.Count == 0)
                {
                    Console.WriteLine("Не один поезд ещё не отправлен");
                }
                else if (_trainsList.Count != 0)
                {
                    foreach (Train train in _trainsList)
                    {
                        train.ShowInfoMarch();
                    }
                }

                Console.WriteLine("Нажмите любую кнопку для создания плана поезда");
                Console.ReadKey();
                Console.Clear();
                CreateTrain();
                Console.WriteLine($"Вы хотите выйти из программы?Нажмите Enter.\nДля продолжение работы программы нажмите любую другую клавишу");

                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    _isWork = false;
                    Console.WriteLine("Вы вышли из программы");
                }

                Console.Clear();
            }
        }

        public void CreateTrain()
        {
            Train train = new Train(_ticketOffice.Sell());
            _trainsList.Add(train);
            Console.WriteLine("Поезд отправлен\n");
        }
    }

    class Direction
    {
        public string FirstStation { get; private set; }
        public string SecondStation { get; private set; }

        public Direction()
        {
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("Введите станцию отправления:");
                FirstStation = Console.ReadLine();
                Console.WriteLine("Введите станцию назначения");
                SecondStation = Console.ReadLine();

                if (FirstStation.ToLower() == SecondStation.ToLower())
                {
                    Console.WriteLine("Станция отправления не должна совпадать со станцией назначение");
                }
                else
                {
                    isWork = false;
                }
            }
        }
    }

    class TicketOffice
    {
        private int _numberPassegers = 0;
        private int _minimumPassager = 0;
        private int _maximumPassager = 101;
        private Random _random = new Random();

        public int Sell()
        {
            _numberPassegers = _random.Next(_minimumPassager, _maximumPassager);
            Console.WriteLine($"Продано {_numberPassegers} билетов");
            return _numberPassegers;
        }
    }

    class Train
    {
        private List<int> _typeWagons = new List<int> { 20, 40, 15 };
        private List<Wagon> _wagons = new List<Wagon>();
        private Direction _direction;

        public int NumberPassegers { get; private set; }

        public Train(int numberPassagers)
        {
            _direction = new Direction();
            NumberPassegers = numberPassagers;
            AddWagon(NumberPassegers);
        }

        public void AddWagon(int numberPassagers)
        {
            if (numberPassagers != 0 && _wagons.Count == 0)
            {
                bool isWork = true;

                while (isWork == true && numberPassagers > 0)
                {
                    Console.WriteLine($"Колличество пассажиров : {numberPassagers}\nВыберете Вагон: ");

                    for (int i = 0; i < _typeWagons.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}.Вагоон на {_typeWagons[i]} мест");
                    }

                    if (int.TryParse(Console.ReadLine(), out int input))
                    {
                        if (input <= _typeWagons.Count && numberPassagers > 0)
                        {
                            Wagon wagon = new Wagon(_typeWagons[input - 1]);
                            _wagons.Add(wagon);
                            numberPassagers -= _typeWagons[input - 1];
                            Console.WriteLine("Вагон добавлен");
                        }
                        else
                        {
                            Console.WriteLine("Данного вагона нет в списке или нет не распределенных пасссажиров");
                        }
                    }
                }
            }
        }

        public void ShowWagons()
        {
            for (int i = 0; i < _wagons.Count; i++)
            {
                Console.WriteLine($"Вагон {i + 1}. Мест:  {_wagons[i].NumberPlace}");
            }
        }

        public void ShowInfoMarch()
        {
            Console.WriteLine($"Назначение {_direction.FirstStation} - {_direction.SecondStation}");
            Console.WriteLine($"Количество пассажиров - {NumberPassegers}");

            if (GetLenght() != 0)
            {
                Console.WriteLine("Состав поезда:");
                ShowWagons();
            }
        }

        public int GetLenght()
        {
            return _wagons.Count;
        }
    }

    class Wagon
    {
        public int NumberPlace { get; private set; }

        public Wagon(int lenght)
        {
            NumberPlace = lenght;
        }
    }
}
