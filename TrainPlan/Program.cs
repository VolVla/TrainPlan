using System;
using System.Collections.Generic;

namespace TrainPlan
{
    internal class Program
    {
        static void Main()
        {
            RailStation rail = new RailStation();
            bool isWork = true;

            while (isWork)
            {
                rail.CreateDirection();
                rail.SellTickets();
                rail.FormationTrain(rail.NumberPasseger);
                rail.SaveDirection();
                rail.SendTrain();
                Console.WriteLine($"Вы хотите выйти из программы?Нажмите Enter.\nДля продолжение работы программы нажмите любую другую клавишу");

                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    isWork = false;
                    Console.WriteLine("Вы вышли из программы");
                }

                Console.Clear();
                rail.ShowInfoMarch();
            }
        }
    }

    class RailStation
    {
        private Train _train = new Train();
        private List<Train> _trains = new List<Train>();
        private List<string> _direction = new List<string>();
        private List<int> _numberPasseger = new List<int>();
        private string _firstStation = "";
        private string _secondStation = "";
        public int NumberPasseger = 0;

        public void ShowInfoMarch()
        {
            if (_direction[_direction.Count - 2] != "")
            {
                Console.WriteLine($"Назначение {_direction[_direction.Count - 2]} - {_direction[_direction.Count - 1]}");
                Console.WriteLine($"Количество пассажиров - {_numberPasseger[_numberPasseger.Count - 1]}");

                if (_trains[_trains.Count - 1].GetTrainLenght() != 0)
                {
                    Console.WriteLine("Состав поезда:");
                    _trains[_trains.Count - 1].RenderTrain();
                }
            }
            else
            {
                Console.WriteLine("Направление ещё не создано ");
            }
        }

        public void CreateDirection()
        {
            if (NumberPasseger == 0)
            {
                Console.WriteLine("Введите станцию отправления:");
                _firstStation = Console.ReadLine();
                Console.WriteLine("Введите станцию назначения");
                _secondStation = Console.ReadLine();

                if (_firstStation.ToLower() == _secondStation.ToLower())
                {
                    Console.WriteLine("Станция отправления не должна совпадать со станцией назначение");
                    _firstStation = "";
                    _secondStation = "";
                }
            }
        }

        public void SellTickets()
        {
            if(NumberPasseger == 0)
            {
                if (_firstStation != "") 
                {
                    Random random = new Random();
                    int minimumPassager = 0;
                    int maximumPassager = 101;
                    NumberPasseger = random.Next(minimumPassager, maximumPassager);
                    Console.WriteLine($"Продано {NumberPasseger} билетов");
                }
            }
        }

        public void SaveDirection()
        {
            _direction.AddRange(new string[] { _firstStation, _secondStation });
            _numberPasseger.Add(NumberPasseger);
            _trains.Add(_train);
        }

        public void SendTrain()
        {
            if (_train.GetTrainLenght() != 0)
            {
                _train.DisbandTrain();
                NumberPasseger = 0;
                _firstStation = "";
                _secondStation = "";
                Console.WriteLine("Поезд отправлен\n");
            }
        }

        public void FormationTrain(int value)
        {
            _train.CreateTrain(value);
        }
    }

    class Train
    {
        private List<Wagon> _typeWagons = new List<Wagon> { new Wagon(20), new Wagon(40), new Wagon(15) };
        private List<Wagon> _wagons = new List<Wagon>();

        public void CreateTrain(int valuePassengers)
        {
            int numberPassengers = valuePassengers;

            if (numberPassengers != 0 && _wagons.Count == 0)
            {
                bool isWork = true;

                while (isWork == true)
                {
                    if(numberPassengers > 0)
                    {
                        Console.WriteLine($"Колличество пассажиров : {numberPassengers}\nВыберете Вагон: ");

                        for(int i = 0; i < _typeWagons.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.Вагоон на {_typeWagons[i].NumberPlace} мест");
                        }

                        bool Number =  int.TryParse(Console.ReadLine(), out int input);

                        if(Number == true)
                        {
                            if (input <= _typeWagons.Count && numberPassengers > 0)
                            {
                                _wagons.Add(_typeWagons[input - 1]);
                                numberPassengers -= _typeWagons[input - 1].NumberPlace;
                                Console.WriteLine("Вагон добавлен");
                            }
                            else
                            {
                                Console.WriteLine("Данного вагона нет в списке или нет не распределенных пасссажиров");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Все пассажиры распределены");
                        isWork = false;
                    }
                } 
            }
        }

        public int GetTrainLenght()
        {
            return _wagons.Count;
        }

        public void DisbandTrain()
        {
            _wagons.RemoveRange(0, _wagons.Count);
        }

        public void RenderTrain()
        {
            for(int i = 0; i < _wagons.Count; i++)
            {
                Console.WriteLine($"Вагон {i+1}. Мест:  {_wagons[i].NumberPlace}");
            }
        }
    }

    class Wagon
    {
        public int NumberPlace { get;private set; }

        public Wagon(int lenght)
        {
            NumberPlace = lenght;
        }
    }
}
