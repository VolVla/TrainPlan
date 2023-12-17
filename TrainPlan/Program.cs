using System;
using System.Collections.Generic;

namespace TrainPlan
{
    internal class Program
    {
        static void Main()
        {
            RailStation railStation = new RailStation();
            railStation.WorkStation();
        }
    }

    class RailStation
    {
        private List<Train> _trainsList = new List<Train>();
        private TicketOffice _ticketOffice = new TicketOffice();
        private bool _isWork = true;

        public void WorkStation()
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

        private void CreateTrain()
        {
            Train train = new Train(_ticketOffice.SellTicket());
            _trainsList.Add(train);
            Console.WriteLine("Поезд отправлен\n");
        }
    }

    class Direction
    {
        public Direction()
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
                Console.WriteLine("Направление создано");
            }
        }

        public string FirstStation { get; private set; }
        public string SecondStation { get; private set; }
    }

    class TicketOffice
    {
        private int _numberPassegers = 0;
        private int _minimumPassager = 0;
        private int _maximumPassager = 101;
        private Random _random = new Random();

        public int SellTicket()
        {
            _numberPassegers = _random.Next(_minimumPassager, _maximumPassager);
            Console.WriteLine($"Продано {_numberPassegers} билетов");
            return _numberPassegers;
        }
    }

    class Train
    {
        private List<int> _typeWagons;
        private List<Wagon> _wagons;
        private Direction _direction;

        public Train(int numberPassagers)
        {
            _wagons = new List<Wagon>();
            _typeWagons = new List<int> { 20, 40, 15 };
            _direction = new Direction();
            NumberPassegers = numberPassagers;
            AddWagon(NumberPassegers);
        }

        public int NumberPassegers { get; private set; }

        public void ShowInfoMarch()
        {
            Console.WriteLine($"Назначение {_direction.FirstStation} - {_direction.SecondStation}");
            Console.WriteLine($"Количество пассажиров - {NumberPassegers}");

            if (GetWagonsCount() > 0)
            {
                Console.WriteLine("Состав поезда:");
                ShowWagons();
            }
        }

        private void AddWagon(int numberPassagers)
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
                        if ((input > 0 & input <= _typeWagons.Count) && numberPassagers > 0)
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

        private void ShowWagons()
        {
            for (int i = 0; i < _wagons.Count; i++)
            {
                Console.WriteLine($"Вагон {i + 1}. Мест:  {_wagons[i].NumberPlace}");
            }
        }

        private int GetWagonsCount()
        {
            return _wagons.Count;
        }
    }

    class Wagon
    {
        public Wagon(int lenght)
        {
            NumberPlace = lenght;
        }

        public int NumberPlace { get; private set; }
    }
}
