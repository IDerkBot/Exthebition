using System;
using System.Collections.Generic;

namespace Exhibition.Models
{
    // Классы для считывания и сохранения программ из .xml файла
    [Serializable]
    public class Step
    {
        private int id;
        private string name;
        private int y;
        private int u;
        private int v;
        private float mA;
        private int kV;
        private int timing;

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Y {
            get => y;
            set
            {
                if (value < 0)
                    y = 0;
                else if (value > 2000)
                    y = 2000;
                else
                    y = value; }
        }

        public int U
        {
            get => u;
            set
            {
                if (value < -30)
                    u = -30;
                else if (value > 90)
                    u = 90;
                else
                    u = value;
            }
        }

        public int V
        {
            get => v;
            set
            {
                if (value < 0)
                    v = 0;
                else if (value > 360)
                    v = 360;
                else
                    v = value;
            }
        }

        public float MA
        {
            get => mA;
            set
            {
                if (value < 0.2)
                    mA = 0.2F;
                else if (value > 10)
                    mA = 10;
                else
                    mA = (float)Math.Round(value, 1);
            }
        }

        public int KV
        {
            get => kV;
            set
            {
                if (value < 0)
                    kV = 0;
                else if (value > 160)
                    kV = 160;
                else
                    kV = value;
            }
        }

        public int Timing
        {
            get => timing;
            set
            {
                if (value < 0)
                    timing = 0;
                else if (value > 10000)
                    timing = 10000;
                else
                    timing = value;
            }
        }

        public Step() {
            id = 1;
            name = id.ToString() + ". Шаг";
            y = 0;
            u = 0;
            v = 0;
            mA = 2;
            kV = 20;
            timing = 50;
        }

        public Step(Step s)
        {
            id = s.id;
            name = s.name;
            y = s.y;
            u = s.u;
            v = s.v;
            mA = s.mA;
            kV = s.kV;
            timing = s.timing;
        }
    }

    [Serializable]
    public class ProgramInfoDataSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Step> Steps { get; set; }


        public ProgramInfoDataSource() {
            Id = 1;
            Name = Id + ". Программа";
            Steps = new List<Step>();
        }

        public ProgramInfoDataSource(int id, string name, List<Step> steps)
        {
            Id = id;
            Name = name;
            Steps = steps;
        }

        public ProgramInfoDataSource(ProgramInfoDataSource program)
        {
            Name = program.Name;
            Steps = program.Steps;
        }
    }
}
