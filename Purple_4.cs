﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_4
    {
        public struct Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;
            private bool _isNoTime;

            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0;
                _isNoTime = true;
            }

            public void Run(double time)
            {
                if (!_isNoTime) return;
                _time = time;
                _isNoTime = false;
            }

            public void Print()
            {
                Console.WriteLine("Name: " + _name);
                Console.WriteLine("Surname: " + _surname);
                Console.WriteLine("time:" + _time);
            }
        }

        public struct Group
        {
            private string _name;
            private Sportsman[] _sportsmen;

            public string Name => _name;
            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_sportsmen == null) return default(Sportsman[]);
                    return _sportsmen;
                }
            }

            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
            }

            public Group(Group group)
            {
                _name = group.Name;
                if (group.Sportsmen != null)
                {
                    _sportsmen = new Sportsman[group._sportsmen.Length];
                    Array.Copy(group._sportsmen, _sportsmen, group._sportsmen.Length);

                }
                else
                    _sportsmen = null;
            }

            public void Add(Sportsman sportsman)
            {
                if (_sportsmen  == null) return;

                Array.Resize(ref _sportsmen,_sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length - 1] = sportsman;
            }
            public void Add(Sportsman[] sportsmen)
            {
                if (_sportsmen == null || sportsmen == null) return;
                int ind = _sportsmen.Length;
                Array.Resize(ref _sportsmen, _sportsmen.Length + sportsmen.Length);

                for (int i = 0, j = 0; i < _sportsmen.Length; i++)
                {
                    if (i >= ind) _sportsmen[i] = sportsmen[j++];
                }
            }
            public void Add(Group group)
            {
                if (group._sportsmen == null) return;   
                this.Add(group._sportsmen);
            }

            public void Sort()
            {
                if (_sportsmen == null) return;

                for(int i = 1, j = 2; i < _sportsmen.Length;)
                {
                    if (i == 0 || _sportsmen[i - 1].Time <= _sportsmen[i].Time)
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        var temp = _sportsmen[i - 1];
                        _sportsmen[i - 1] = _sportsmen[i];
                        _sportsmen[i] = temp;
                        i--;
                    }
                }
            }
            public static Group Merge(Group group1, Group group2)
            {
                Group MergedGroup = new Group("Финалисты");

                MergedGroup.Add(group1);
                MergedGroup.Add(group2);
                MergedGroup.Sort();
                return MergedGroup;
            }
            public void Print()
            {
                Console.WriteLine("Name: " + _name);
                Console.WriteLine("Sportsmen:");
                
                foreach (Sportsman item in _sportsmen)
                {
                    Console.Write($"{item.Name,4} {item.Surname,4} {item.Time}");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
