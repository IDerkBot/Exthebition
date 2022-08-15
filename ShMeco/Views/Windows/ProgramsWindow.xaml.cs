using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using Exhibition.Models;

namespace Exhibition.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProgramsWindow.xaml
    /// </summary>
    public partial class ProgramsWindow : Window
    {
        private ProgramInfoDataSource selectedItem; 

        public ProgramsWindow()
        {
            InitializeComponent();
            ManagerProgram.ReadPrograms();
            DgPrograms.ItemsSource = ManagerProgram.Programs;
        }

        #region Buttons

        private void BtnCreate_Click(object sender, RoutedEventArgs e) => CreateProgram();
        private void BtnChange_Click(object sender, RoutedEventArgs e) => ChangeProgram();
        private void BtnClose_Click(object sender, RoutedEventArgs e) => CloseWindow();
        private void BtnAddStep_Click(object sender, RoutedEventArgs e) => AddStep();
        private void BtnDeleteStep_Click(object sender, RoutedEventArgs e) => DeleteStep();
        private void BtnSave_Click(object sender, RoutedEventArgs e) => SaveStep();

        #endregion

        #region Methods

        /// <summary>
        /// Создание программы
        /// </summary>
        private void CreateProgram()
        {
            string name = ManagerProgram.Programs[ManagerProgram.Programs.Count - 1].Id + 1 + ". Программа";
            ProgramInfoDataSource prog = new ProgramInfoDataSource(ManagerProgram.Programs[ManagerProgram.Programs.Count - 1].Id + 1, name, new List<Step>());
            ManagerProgram.Programs.Add(prog);
            GrProgramCreate.Visibility = Visibility.Visible;
            LblProgramName.Content = name;

            prog.Steps = prog.Steps.Select(s => new Step(s)).ToList();
            DgSteps.ItemsSource = prog.Steps;
            DgPrograms.Items.Refresh();
            DgPrograms.CurrentItem = DgPrograms.Items[ManagerProgram.Programs.Count - 1];
        }

        /// <summary>
        /// Редактирование программы
        /// </summary>
        private void ChangeProgram()
        {
            int currentRowIdx = DgPrograms.Items.IndexOf(DgPrograms.CurrentItem);
            if (currentRowIdx == -1)
                return;
            GrProgramCreate.Visibility = Visibility.Visible;
            ProgramInfoDataSource pds = DgPrograms.CurrentItem as ProgramInfoDataSource;
            LblProgramName.Tag = currentRowIdx;
            LblProgramName.Content = pds.Name;
            DgSteps.ItemsSource = pds.Steps;
            DgPrograms.CurrentItem = DgPrograms.Items[currentRowIdx];
        }

        /// <summary>
        /// Удаление программы
        /// </summary>
        private void DeleteProgram()
        {

        }

        /// <summary>
        /// Закрытие окна
        /// </summary>
        private void CloseWindow()
        {
            //Button btn = (Button)sender;
            //if (btn.Name == "BtnCloseCreate")
            //{
            //    DgPrograms.CancelEdit();
            //    DgPrograms.CancelEdit();
            //    DgPrograms.Items.Refresh();
            //}
            Close();
        }

        /// <summary>
        /// Добавление шага
        /// </summary>
        private void AddStep()
        {
            if (selectedItem == null) return;

            Step step = new Step
            {
                Id = selectedItem.Steps.Count + 1,
                Name = selectedItem.Steps.Count + 1 + ". Шаг",
                Y = 0,
                U = 0,
                V = 0,
                MA = 0,
                KV = 0,
                Timing = 0
            };

            int selectedStepIndex = GetIndexSelectedStep();

            // Вставляем новый шаг после выбранной строки
            if (selectedStepIndex >= 0 && selectedStepIndex < ManagerProgram.Programs[GetIndexSelectedProgram()].Steps.Count)
            {
                ManagerProgram.Programs[GetIndexSelectedProgram()].Steps.Insert(selectedStepIndex, step);
                for (int i = selectedStepIndex + 1; i < ManagerProgram.Programs[GetIndexSelectedProgram()].Steps.Count(); ++i)
                {
                    ManagerProgram.Programs[GetIndexSelectedProgram()].Steps[i].Id = i;
                    ManagerProgram.Programs[GetIndexSelectedProgram()].Steps[i].Name = i + 1 + ". Шаг";
                }
            }
            // Добавляем новый шаг в конец списка
            else
                ManagerProgram.Programs[GetIndexSelectedProgram()].Steps.Add(step);

            DgSteps.Items.Refresh();
        }

        /// <summary>
        /// Удаление шага
        /// </summary>
        private void DeleteStep()
        {
            int currentStepIdx = DgSteps.Items.IndexOf(DgSteps.CurrentItem);
            if (currentStepIdx > -1 && currentStepIdx < ManagerProgram.Programs[GetIndexSelectedProgram()].Steps.Count())
            {
                for (int i = currentStepIdx + 1; i < ManagerProgram.Programs[GetIndexSelectedProgram()].Steps.Count; ++i)
                {
                    ManagerProgram.Programs[GetIndexSelectedProgram()].Steps[i].Id = i - 1;
                    ManagerProgram.Programs[GetIndexSelectedProgram()].Steps[i].Name = i.ToString() + ". Шаг";
                }
                ManagerProgram.Programs[GetIndexSelectedProgram()].Steps.RemoveAt(currentStepIdx);

                DgPrograms.Items.Refresh();
                DgSteps.Items.Refresh();
            }
        }

        /// <summary>
        /// Сохранение шага
        /// </summary>
        private void SaveStep()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ProgramInfoDataSource>));
            using (TextWriter writer = new StreamWriter("progs.xml"))
            {
                serializer.Serialize(writer, ManagerProgram.Programs);
            }
            DgSteps.Items.Refresh();
        }

        /// <summary>
        /// Получение индекса выбранной программы
        /// </summary>
        /// <returns></returns>
        private int GetIndexSelectedProgram()
        {
            if (!(DgPrograms.SelectedItem is ProgramInfoDataSource item)) return -1;

            return (int)DgPrograms.SelectedIndex;
        }

        /// <summary>
        /// Получение индекса выбранного шага
        /// </summary>
        /// <returns></returns>
        private int GetIndexSelectedStep()
        {
            if (!(DgSteps.SelectedItem is Step item)) return -1;

            return (int)DgSteps.SelectedIndex;
        }

        #endregion

        #region GUI

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void DgPrograms_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(DgPrograms.SelectedItem is ProgramInfoDataSource item)) return;

            selectedItem = item;
            LblProgramName.Content = item.Name;
            DgSteps.ItemsSource = item.Steps;
        }

        #endregion
    }
}
