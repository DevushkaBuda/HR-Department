using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace HumanResourcesDepartmentApp
{
    /// <summary>
    /// Логика взаимодействия для VacationSchedulePage.xaml
    /// </summary>
    public partial class VacationSchedulePage : System.Windows.Controls.Page
    {
        public VacationSchedulePage()
        {
            InitializeComponent();
            vacation = new List<Vacation_Schedule>();
        }
        List<Vacation_Schedule> vacation;

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                HumanResourcesDepartmentEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGVacationSchedule.ItemsSource = HumanResourcesDepartmentEntities1.GetContext().Vacation_Schedule.ToList();
            }
        }

        private void BtnVacationSchedule_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditVacationSchedulePage((Vacation_Schedule)DGVacationSchedule.Items[DGVacationSchedule.SelectedIndex]));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditVacationSchedulePage(null));
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = TxtSearch.Text;

            var VacationSchedule = HumanResourcesDepartmentEntities1.GetContext().Vacation_Schedule.ToList();
            DGVacationSchedule.ItemsSource = VacationSchedule.Where(c => c.Profile.Full_Name.Contains(search));
            vacation = HumanResourcesDepartmentEntities1.GetContext().Vacation_Schedule.Where(c => c.Profile.Full_Name.Contains(search)).ToList();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            var ScheduleForRemoving = DGVacationSchedule.SelectedItems.Cast<Vacation_Schedule>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить слудующие {ScheduleForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    HumanResourcesDepartmentEntities1.GetContext().Vacation_Schedule.RemoveRange(ScheduleForRemoving);
                    HumanResourcesDepartmentEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGVacationSchedule.ItemsSource = HumanResourcesDepartmentEntities1.GetContext().Vacation_Schedule.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnOtchet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Word._Application wApp = new Word.Application();
                Word._Document wDoc = wApp.Documents.Add();
                wApp.Visible = true;
                wDoc.Activate();
                var ProductParagraph = wDoc.Content.Paragraphs.Add();
                //ProductParagraph.Range.Text = $"День недели:\t{dayOfTheWeek.Name}\n" + $"Статус:\t{shedule.Status}\n" + $"Время работы:\t{shedule.Duration}\n" + $"Цех:\t{shedule.Cabinet}\n";
                Word.Table wTable = wDoc.Tables.Add((Microsoft.Office.Interop.Word.Range)ProductParagraph.Range,
                vacation.Count + 1, 4, Word.WdDefaultTableBehavior.wdWord9TableBehavior);
                wTable.Cell(1, 1).Range.Text = "Специалист";
                wTable.Cell(1, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 2).Range.Text = "Дата начала";
                wTable.Cell(1, 2).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 3).Range.Text = "Продолжительность";
                wTable.Cell(1, 3).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                wTable.Cell(1, 4).Range.Text = "Дата окончания";
                wTable.Cell(1, 4).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                int countRow = 2;
                foreach (var item in vacation)
                {
                    wTable.Cell(countRow, 1).Range.Text = item.Profile.Full_Name.ToString();
                    wTable.Cell(countRow, 1).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 2).Range.Text = item.Start_Date.ToString();
                    wTable.Cell(countRow, 2).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 3).Range.Text = item.Duration.ToString();
                    wTable.Cell(countRow, 3).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wTable.Cell(countRow, 4).Range.Text = item.End_Date.ToString();
                    wTable.Cell(countRow, 4).Range.Paragraphs.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    countRow++;
                }
                /*Word.Chart wChart;
                Word.InlineShape inlineShape;
                inlineShape = wDoc.InlineShapes.AddChart(Microsoft.Office.Core.XlChartType.xlColumnClustered, ProductParagraph.Range);
                wChart = inlineShape.Chart;

                dynamic chartWB = wChart.ChartData.Workbook;
                dynamic chartTable = chartWB.Sheets[1].ListObjects("Таблица1"); chartTable.DataBodyRange.ClearContents();
                dynamic chartRange = chartTable.Range.Resize[2, dayOfTheWeek.Schedule.Count + 1];
                chartTable.Resize(chartRange);
                int countCol = 2;
                foreach (var item in dayOfTheWeek.Schedule)
                {
                    chartRange.Cells[1, countCol] = item.Duration.ToString();
                    chartRange.Cells[2, countCol] = item.Id_Profile.ToString();
                    countCol++;
                }
                */
                wDoc.SaveAs2($@"{Environment.CurrentDirectory}\{DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")}.docx");
            }

            catch
            {
                MessageBox.Show($"Ошибка");
            }
            var process = Process.GetProcessesByName("Excel");
            foreach (var p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }
        }
    }
}
