using System;
using System.Collections.Generic;

namespace TrainPlan
{
    internal class Program
    {
        static void Main()
        {
            RailStation railStation = new RailStation();
            bool isWork = true;

            while (isWork)
            {
                railStation.CreateDirection();
                railStation.CreateTrain();
                railStation.SendTrain();
                Console.WriteLine($"Вы хотите выйти из программы?Нажмите Enter.\nДля продолжение работы программы нажмите любую другую клавишу");

                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    isWork = false;
                    Console.WriteLine("Вы вышли из программы");
                }

                Console.Clear();
                railStation.ShowInfoMarch();
            }
        }
    }

    class RailStation
    {
        private List<Train> _trainsList = new List<Train>();
        private TicketOffice _ticketOffice = new TicketOffice();
        private Direction _direction = new Direction();

        public void CreateDirection()
        {
            _direction.Create();
        }

        public int SellTicket()
        {
            return _ticketOffice.Sell();
        }

        public void CreateTrain()
        {
            Train train = new Train(SellTicket());
            _trainsList.Add(train);
        }

        public void SendTrain()
        {
            Console.WriteLine("Поезд отправлен\n");
        }

        public void ShowInfoMarch()
        {
            Console.WriteLine($"Назначение {_direction.FirstStation} - {_direction.SecondStation}");
            Console.WriteLine($"Количество пассажиров - {_trainsList[_trainsList.Count - 1].numberPasseger}");

            if (_trainsList[_trainsList.Count - 1].GetTrainLenght() != 0)
            {
                Console.WriteLine("Состав поезда:");
                _trainsList[_trainsList.Count - 1].RenderTrain();
            }
        }
    }

    class Direction
    {
        private string _firstStation = "";
        private string _secondStation = "";

        public string FirstStation { get; private set; }
        public string SecondStation { get; private set; }

        public void Create()
        {
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("Введите станцию отправления:");
                _firstStation = Console.ReadLine();
                Console.WriteLine("Введите станцию назначения");
                _secondStation = Console.ReadLine();

                if (_firstStation.ToLower() == _secondStation.ToLower())
                {
                    Console.WriteLine("Станция отправления не должна совпадать со станцией назначение");
                }
                else
                {
                    isWork = false;
                }
            }
            FirstStation = _firstStation;
            SecondStation = _secondStation;
        }
    }

    class TicketOffice
    {
        private int _numberPasseger = 0;
        private int _minimumPassager = 0;
        private int _maximumPassager = 101;

        public int Sell()
        {
            Random random = new Random();
            _numberPasseger = random.Next(_minimumPassager, _maximumPassager);
            Console.WriteLine($"Продано {_numberPasseger} билетов");
            return _numberPasseger;
        }
    }

    class Train
    {
        private List<int> _typeWagons = new List<int> { 20, 40, 15 };
        private List<Wagon> _wagons = new List<Wagon>();

        public int numberPasseger { get; private set; }

        public Train(int numberPassagers)
        {
            numberPasseger = numberPassagers;
            AddWagon(numberPassagers);
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

                    bool number = int.TryParse(Console.ReadLine(), out int input);

                    if (number == true)
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

        public void RenderTrain()
        {
            for (int i = 0; i < _wagons.Count; i++)
            {
                Console.WriteLine($"Вагон {i + 1}. Мест:  {_wagons[i].NumberPlace}");
            }
        }

        public int GetTrainLenght()
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
